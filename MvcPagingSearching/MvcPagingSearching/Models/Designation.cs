namespace MvcPaging.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Designation")]
    public partial class Designation
    {
        public int Id { get; set; }

        [StringLength(10)]
        public string Code { get; set; }

        [StringLength(100)]
        public string Description { get; set; }
    }
}
