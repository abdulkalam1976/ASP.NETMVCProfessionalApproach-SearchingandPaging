namespace MvcPaging.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string Employeecode { get; set; }

        [StringLength(100)]
        public string Employeename { get; set; }

        [StringLength(300)]
        public string Address { get; set; }
    }
}
