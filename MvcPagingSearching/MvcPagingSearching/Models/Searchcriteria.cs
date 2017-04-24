using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model
{
    public partial class Searchcriteria
    {
        public Searchcriteria()
        {

        }

        public static  List<SelectListItem> GetSearchCriteria(string searchtype)
        {
            List<SelectListItem> searchcriteria = new List<SelectListItem>();
            switch (searchtype)
            {
                case "Employee":
                    searchcriteria.Add(new SelectListItem { Text = "Employeecode", Value = "Employeecode" });
                    searchcriteria.Add(new SelectListItem { Text = "Employeename", Value = "Employeename" });
                    searchcriteria.Add(new SelectListItem { Text = "Address", Value = "Address" });
                    break;
                case "Designation":
                    searchcriteria.Add(new SelectListItem { Text = "Code", Value = "Code" });
                    searchcriteria.Add(new SelectListItem { Text = "Description", Value = "Description" });
                    break;
            }
            return searchcriteria;
        }
    }
}
