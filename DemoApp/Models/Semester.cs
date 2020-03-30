using System.ComponentModel.DataAnnotations;

namespace DemoApp.Models
{
    public class Semester
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(10)]
        public string ShortName { get; set; }



        public Department Department { get; set; }
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
    }
}