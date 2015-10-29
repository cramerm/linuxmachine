namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Achievement")]
    public partial class Achievement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int achievementID { get; set; }

        [StringLength(50)]
        public string achievementName { get; set; }

        public string achievementDescription { get; set; }

        public bool? completed { get; set; }

        public int? userID { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}
