using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using API.Models;

namespace API.Controllers
{
    [Route("api/Student")]
    [ApiController]
    public class StudentControler: ControllerBase
    {
        private readonly ILogger<StudentControler> _logger;
        public StudentControler(ILogger<StudentControler> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("All", Name = "getAll")]
        public ActionResult<IEnumerable<Student>> getAll()
        {
            _logger.LogInformation("Get all data");
            var students = new List<StudentDTO>();
            foreach (var item in StudentReponsitory.Students)
            {
                StudentDTO student = new StudentDTO()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Phone = item.Phone,
                    Address = item.Address,
                };
                students.Add(student);
            }
            return Ok(students);
        }

        [HttpGet]
        [Route("{id:int}", Name = "getStudentById")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]

        public IActionResult getStudentById(int id)
        {
            if(id <= 0)
            {
                _logger.LogWarning("Lỗi kh nghiêm trọng");
                return BadRequest();
            }
            var student = StudentReponsitory.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
            {
                _logger.LogWarning("K tìm thấy");
                return NotFound($"Không tìm thấy student có id: {id} như vậy");
            }
            return Ok(student);
        }
        [HttpGet]
        [Route("{name:alpha}", Name = "getStudentByName")]
        public IActionResult getStudentByName(string name)
        {
            var student = StudentReponsitory.Students.Where( n => n.Name == name).FirstOrDefault();
            if(student == null)
            {
                _logger.LogWarning("K tìm thấy");
                return NotFound($"Không tìm thấy student có tên: {name} như vậy");
            }
            return Ok(student);
        }
        [HttpDelete("{id:int}", Name = "deleteStudentById")]  
        public bool deleteStudentById(int id)
        {
            
            var student = StudentReponsitory.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
            {
                _logger.LogWarning("K tìm thấy");
                return false;
            }
            StudentReponsitory.Students.Remove(student);
            return true;
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("Create", Name = "postStudent")]       
        
        public IActionResult postStudent([FromBody]StudentDTO student)
        {
            if (student == null)
            {
                _logger.LogWarning("K tìm thấy");
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("K tìm thấy");
                return BadRequest(ModelState);
            }
            int newId = StudentReponsitory.Students.LastOrDefault().Id + 1;

            Student item = new Student()
            {
                Id = newId,
                Name = student.Name,
                Phone = student.Phone,
                Address = student.Address,
            };

            StudentReponsitory.Students.Add(item);
            return CreatedAtRoute("GetStudentById", new { id = newId }, item);

        }
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("update", Name = "updateStudent")]

        public IActionResult updateStudent([FromBody] StudentDTO student)
        {
            if(!ModelState.IsValid || student == null)
            {
                return BadRequest(ModelState);
            }
            var item = StudentReponsitory.Students.Where(s=>s.Id == student.Id).FirstOrDefault();

            if (item == null)
                return NotFound();

            return NoContent();
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("{id:int}/updatePatch", Name = "updatePartial")]

        public IActionResult updatePartial(int id, [FromBody] JsonPatchDocument<StudentDTO> pStudent)
        {
            if (pStudent == null || id <= 0)
            {
                return BadRequest(ModelState);
            }
            var item = StudentReponsitory.Students.Where(s => s.Id == id).FirstOrDefault();

            if (item == null)
                return NotFound();

            var studentDTO = new StudentDTO
            {
                Id = item.Id,
                Name = item.Name,
                Email = item.Email,
                Phone = item.Phone,
                Address = item.Address,
            };
            pStudent.ApplyTo(studentDTO, ModelState);   
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            item.Name = studentDTO.Name;
            item.Email = studentDTO.Email;
            item.Phone = studentDTO.Phone;
            item.Address = studentDTO.Address;

            

            return NoContent();
        }
    }
}
