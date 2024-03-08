using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength =1, ErrorMessage ="Tên có ít nhất 1 kí tự")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "Tên không được chứa số")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
       
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}
