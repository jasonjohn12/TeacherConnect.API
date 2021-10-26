using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeacherConnect.Data.Interfaces;
using TeacherConnect.Dto;
using TeacherConnect.Entities;
using TeacherConnect.Extensions;

namespace TeacherConnect.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : BaseApiController
    {
        private readonly IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudents()
        {
            var students = await _studentRepository.GetAllStudentsAsync(User.GetUserId());

            return Ok(students);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await _studentRepository.GetStudentByIdAsync(id);
            // var studentToReturn = new Student
            // {
            //     FirstName = student.FirstName,
            //     LastName = student.LastName,
            //     Grade = student.Grade
            // };

            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<StudentDto>> AddStudent(StudentDto studentDto)
        {
            var userId = User.GetUserId();
            var student = new Student
            {
                UserId = userId,
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Grade = studentDto.Grade,
                CreatedAtDate = DateTime.Now,
                LastUpdatedDate = DateTime.UtcNow,
                CreatedByUserId = userId
            };
            var studentId = await _studentRepository.AddStudent(student);
            var studentToCreate = new StudentDto
            {
                Id = studentId,
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Grade = student.Grade,
                CreatedAtDate = student.CreatedAtDate,
                UserId = userId
            };
            if (studentId > 0) return StatusCode(201, studentToCreate);
            //if (await _studentsRepository.SaveAllAsync()) return Ok(student);
            return BadRequest("Unabled to create student");

        }

        //     // [HttpPatch]
        //     // public async Task<ActionResult<StudentDto>> UpdateStudent(StudentDto studentDto)
        //     // {
        //     //     var userId = User.GetUserId();
        //     //     var student = new Student
        //     //     {

        //     //         Grade = studentDto.Grade

        //     //     };
        //     //     _studentsRepository.AddStudent(student);
        //     //     if (await _studentsRepository.SaveAllAsync()) return Ok(student);
        //     //     return BadRequest("Unabled to create student");

        //     // }
        // }
    }
}