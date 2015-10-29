namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Dinner")]
    public partial class Dinner
    {
        public Dinner()
        {
            Courses = new HashSet<Course>();
            Participants = new HashSet<ApplicationUser>();
        }

        public int dinnerID { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public string description { get; set; }

        public int? userID { get; set; }

        public int? dinnerMonth { get; set; }

        public int? dinnerDay { get; set; }

        public int? dinnerYear { get; set; }

        public int? NumberOfCourses { get; set; }

        public int? NumberAttending { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ApplicationUser> Participants { get; set; }
    }
}
