using System.ComponentModel.DataAnnotations;

namespace DemoApp.Models
{
    public class Department
    {
        public int Id { get; set; }

        [StringLength(10)]
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
