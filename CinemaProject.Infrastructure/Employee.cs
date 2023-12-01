namespace CinemaProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal IdEmployee { get; set; }

        [Required]
        [StringLength(50)]
        public string LastNameEmployee { get; set; }

        [Required]
        [StringLength(50)]
        public string NameEmployee { get; set; }

        [Required]
        [StringLength(50)]
        public string MiddleNameEmployee { get; set; }

        public DateTime DateOfBirth { get; set; }

        [StringLength(50)]
        public string Floor { get; set; }

        public decimal IDPost { get; set; }

        [Required]
        [StringLength(50)]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public virtual Post Post { get; set; }
    }
}
