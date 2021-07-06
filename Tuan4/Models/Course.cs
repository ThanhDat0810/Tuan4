namespace Tuan4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Course")]
    public partial class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string LecturerId { get; set; }

        [Required(ErrorMessage = "The Place is requited")]
        [StringLength(255)]
        public string Place { get; set; }

        [Required(ErrorMessage = "Date Time is not correct format")]
        public DateTime? DateTime { get; set; }

        public int? CategoryId { get; set; }

        [Required(ErrorMessage ="Category is requited")]
        public virtual Category Category { get; set; }
        

        //add list category
        public List<Category> ListCategory = new List<Category>();
    }
}
