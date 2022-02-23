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
            var students = await _studentRepository.GetAllStudentsAsync(4);

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

        [HttpPut]
        public async Task<ActionResult<StudentDto>> UpdateStudent(StudentDto studentDto)
        {
            var userId = User.GetUserId();
            var student = await _studentRepository.GetStudentByIdAsync(studentDto.Id);
            if (student == null) return NotFound();
            if (userId != student.CreatedByUserId) return BadRequest("You are not authorized to edit this user");
            var newStudent = new Student
            {
                Id = student.Id,
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Grade = studentDto.Grade,
                CreatedAtDate = student.CreatedAtDate,
                LastUpdatedDate = DateTime.Now,

            };
            var editedStudent = await _studentRepository.EditStudent(newStudent);
            if (editedStudent > 0) return StatusCode(204, editedStudent);
            return BadRequest("Unable to edit student");
            // _studentsRepository.AddStudent(student);
            // if (await _studentsRepository.SaveAllAsync()) return Ok(student);
            // return BadRequest("Unabled to create student");

        }
        // }
    }
}