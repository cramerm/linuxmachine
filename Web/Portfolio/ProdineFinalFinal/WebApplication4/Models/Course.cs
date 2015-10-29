namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        public int courseID { get; set; }

        public int? userID { get; set; }

        public int? dinnerID { get; set; }

        [StringLength(50)]
        public string courseName { get; set; }

        public string courseDescription { get; set; }

        public int? time { get; set; }

        public string recipe { get; set; }

        [StringLength(50)]
        public string address { get; set; }

        [StringLength(50)]
        public string state { get; set; }

        public int? zipcode { get; set; }

        public virtual Dinner Dinner { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
