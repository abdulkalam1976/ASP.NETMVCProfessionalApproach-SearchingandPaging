using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using MvcPaging.Models;
using PagedList;
using Model;
using System;

namespace MvcPaging.Controllers
{
    public class EmployeesController : Controller
    {
        private Model1 db = new Model1();

        public IPagedList DisplayFilterData(string sortOrder, string currentFilter, string searchString, string searchField, int? page, int? pagesize)
        {
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentSearchField = searchField;
            ViewBag.SearchResult = new SelectList(Searchcriteria.GetSearchCriteria("Employee"), "Text", "Value", searchField);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Employeecode" : "";

            if (searchString != null) { page = 1; } else { searchString = currentFilter; }

            ViewBag.CurrentFilter = searchString;

            IQueryable<Employee> emp = db.Employees;

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchField)
                {
                    case "Employeecode":
                        emp = emp.Where(r => r.Employeecode.Contains(searchString));
                        break;
                    case "Employeename":
                        emp = emp.Where(r => r.Employeename.Contains(searchString));
                        break;
                    case "Address":
                        emp = emp.Where(r => r.Address.Contains(searchString));
                        break;
                }
            }

            if (searchField == null)
            {
                emp = emp.OrderByDescending(r => r.Id);
            }
            else
            {
                switch (searchField)
                {
                    case "Employeecode":
                        emp = emp.OrderBy(r => r.Employeecode);
                        break;
                    case "Employeename":
                        emp = emp.OrderBy(r => r.Employeename);
                        break;
                    case "Address":
                        emp = emp.OrderBy(r => r.Address);
                        break;
                }
            }

            int pageSize = (pagesize ?? 3);
            int pageNumber = (page ?? 1);

            return emp.ToPagedList(pageNumber, pageSize);
        }


        // GET: Employees
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, string searchField, int? page, int? pagesize)
        {
            return View(DisplayFilterData(sortOrder, currentFilter, searchString, searchField, page, pagesize));
        }
        
        // GET: Employees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Employeecode,Employeename,Address")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Employeecode,Employeename,Address")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = await db.Employees.FindAsync(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employee employee = await db.Employees.FindAsync(id);
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
