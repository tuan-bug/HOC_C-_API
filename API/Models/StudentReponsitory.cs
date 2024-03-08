using API.Models;

namespace API.Models
{
    public class StudentReponsitory
    {
        public static List<Student> Students {  get; set; } = new List<Student>()
            {
                new Student{
                    Id = 2,
                    Name = "Phạm Tuân",
                    Email = "t@gmail.com",
                    Phone = "0977 55 333",
                    Address = "BG"
                },
                new Student{
                    Id = 3,
                    Name = "Phạm Tuân",
                    Email = "t@gmail.com",
                    Phone = "0977 55 333",
                    Address = "BG"
                },
                new Student
                {
                    Id = 5,
                    Name = "Phạm Tuân",
                    Email = "t@gmail.com",
                    Phone = "0977 55 333",
                    Address = "BG"
                }
            };
    }
}
