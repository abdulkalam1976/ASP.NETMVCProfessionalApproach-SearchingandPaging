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
    public class DesignationsController : Controller
    {
        private Model1 db = new Model1();

        public IPagedList DisplayFilterData(string sortOrder, string currentFilter, string searchString, string searchField, int? page, int? pagesize)
        {
            ViewBag.CurrentFilter = searchString;
            ViewBag.CurrentSearchField = searchField;
            ViewBag.SearchResult = new SelectList(Searchcriteria.GetSearchCriteria("Designation"), "Text", "Value", searchField);

            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Code" : "";

            if (searchString != null) { page = 1; } else { searchString = currentFilter; }

            ViewBag.CurrentFilter = searchString;

            IQueryable<Designation> desig = db.Designations;

            if (!String.IsNullOrEmpty(searchString))
            {
                switch (searchField)
                {
                    case "Code":
                        desig = desig.Where(r => r.Code.Contains(searchString));
                        break;
                    case "Description":
                        desig = desig.Where(r => r.Description.Contains(searchString));
                        break;
                }
            }

            if (searchField == null)
            {
                desig = desig.OrderByDescending(r => r.Id);
            }
            else
            {
                switch (searchField)
                {
                    case "Code":
                        desig = desig.OrderBy(r => r.Code);
                        break;
                    case "Description":
                        desig = desig.OrderBy(r => r.Description);
                        break;
                }
            }

            int pageSize = (pagesize ?? 3);
            int pageNumber = (page ?? 1);

            return desig.ToPagedList(pageNumber, pageSize);
        }

        // GET: Employees
        public ActionResult Designations(string sortOrder, string currentFilter, string searchString, string searchField, int? page, int? pagesize)
        {
            TempData["CurrentAction"] = "Designations";
            return View(DisplayFilterData(sortOrder, currentFilter, searchString, searchField, page, pagesize));
        }

        // GET: Employees
        public ActionResult Index(int? page, int? pagesize)
        {
            return View();
        }


        // GET: Designations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = await db.Designations.FindAsync(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // GET: Designations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Designations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Code,Description")] Designation designation)
        {
            if (ModelState.IsValid)
            {
                db.Designations.Add(designation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(designation);
        }

        // GET: Designations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = await db.Designations.FindAsync(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Code,Description")] Designation designation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(designation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(designation);
        }

        // GET: Designations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Designation designation = await db.Designations.FindAsync(id);
            if (designation == null)
            {
                return HttpNotFound();
            }
            return View(designation);
        }

        // POST: Designations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Designation designation = await db.Designations.FindAsync(id);
            db.Designations.Remove(designation);
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
