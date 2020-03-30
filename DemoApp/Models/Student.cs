using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoApp.Models
{
    public class Student
    {
        public Student()
        {
            Image = new byte[0];
        }
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Roll { get; set; }

        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }


        public Department Department { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }


        public Semester Semester { get; set; }
        [Display(Name = "Semester")]
        public int SemesterId { get; set; }

        [Display(Name = "Profile Picture")]
        [NotMapped]
        public IFormFile ProfileImage { get; set; }

        public byte[] Image { get; set; }
    }
}