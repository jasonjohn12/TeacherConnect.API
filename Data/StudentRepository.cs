using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TeacherConnect.Data;
using TeacherConnect.Data.Interfaces;
using TeacherConnect.Dto;
using TeacherConnect.Entities;


namespace TeacherConnectAPI.Data
{
    public class StudentRepository : BaseRepository, IStudentRepository
    {

        private readonly IConfiguration _configuration;
        public StudentRepository(IConfiguration configuration) : base(configuration, "TeacherConnectDB")
        {
            _configuration = configuration;

        }


        public async Task<int> AddStudent(Student studentDto)
        {
            string command = @"INSERT INTO dbo.student(firstName, lastName, grade,createdAtDate,lastupdateddate, createdbyuserid, status)
              VALUES(@firstname, @lastname, @grade, @createdatdate, @lastupdateddate, @createdbyuserid,@status) RETURNING id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.ReturnValue);
            parameters.Add("@firstname", studentDto.FirstName);
            parameters.Add("@lastname", studentDto.LastName);
            parameters.Add("@grade", studentDto.Grade);
            parameters.Add("@createdatdate", studentDto.CreatedAtDate);
            parameters.Add("@lastupdateddate", studentDto.LastUpdatedDate);
            parameters.Add("@createdbyuserid", studentDto.CreatedByUserId);
            parameters.Add("@status", 'A');

            var result = await ExecuteScalarAsync<int>(sql: command, param: parameters);
            return result;
        }

        public async Task<List<Student>> GetAllStudentsAsync(int userId)
        {
            string command = "SELECT  s.id, s.firstname, s.lastname, s.grade," +
             "s.createdatdate, s.lastupdateddate, s.createdbyuserid, e.id, e.note, e.studentid," +
             "e.createdatdate, e.createdbyuserid, e.lastupdatedate,e.contacted, e.contacteddate" +
             " FROM dbo.student as s LEFT JOIN dbo.entry as e ON s.id = e.studentid  WHERE s.createdbyuserid = @userid" +
             " AND s.status = 'A' ORDER BY s.id ASC, e.id ASC";
            var studentDict = new Dictionary<int, Student>();
            var students = await QueryAsync<Student, Entry, Student>(
                             command,
                         (student, entry) =>
                         {
                             Student currentStudent;

                             if (!studentDict.TryGetValue(student.Id, out currentStudent))
                             {
                                 currentStudent = student;
                                 currentStudent.Entries = new List<Entry>();
                                 studentDict.Add(currentStudent.Id, currentStudent);
                             }
                             if (entry?.Id > 0)
                             {
                                 currentStudent.Entries.Add(entry);
                             }

                             return currentStudent;

                         }, param: new { userid = userId });

            return students.Distinct().ToList();


            // using (var connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=Test;User Id=postgres;Password=Reality17;"))
            // {
            //     var students = await connection.QueryAsync<Entities_Student, Entities_Entries, Entities_Student>(
            //         command,
            //     (student, entry) =>
            //     {
            //         Entities_Student currentStudent;
            //         if (!studentDict.TryGetValue(student.student_id, out currentStudent))
            //         {
            //             currentStudent = student;
            //             currentStudent.Entries = new List<Entities_Entries>();
            //             studentDict.Add(currentStudent.student_id, currentStudent);
            //         }
            //         currentStudent.Entries.Add(entry);
            //         return currentStudent;

            //     }, splitOn: "entry_id");

            //     return students.Distinct().ToList();
            // }





            // string command = @"SELECT * FROM students where user_id = @user_Id";
            // var students = await QueryAsync<Entities_Student>(sql: command, param: new
            // {
            //     user_id = userId
            // });

            // var listOfStudents = new List<Student>();
            // foreach (var student in students)
            // {
            //     var studentsToReturn = new Student
            //     {
            //         StudentId = student.student_id,
            //         UserId = student.user_id,
            //         SchoolId = student.school_id,
            //         Grade = student.grade,
            //         FirstName = student.first_name,
            //         LastName = student.last_name,
            //         CreatedAtUTC = student.created_at_date,
            //         LastEditDateUTC = student.last_update_date,

            //     };
            //     listOfStudents.Add(studentsToReturn);
            // }
            // return listOfStudents;
        }

        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            string command = "select s.id, s.firstname, s.lastname, s.grade, s.createdbyuserid, s.createdatdate, s.lastupdateddate from dbo.student as s where id = @id";
            var student = await QueryFirstOrDefaultAsync<Student>(sql: command, param: new
            {
                id = studentId
            });

            return student;
        }

        // public async Task<Student> GetStudentById(int studentId)
        // {
        //     string command = @"SELECT * FROM students where student_id = @student_id";
        //     var student = await QueryFirstOrDefaultAsync<Entities_Student>(sql: command, param: new
        //     {
        //         student_id = studentId
        //     });
        //     var studentsToReturn = new Student
        //     {
        //         StudentId = student.student_id,
        //         UserId = student.user_id,
        //         SchoolId = student.school_id,
        //         Grade = student.grade,
        //         FirstName = student.first_name,
        //         LastName = student.last_name,
        //         CreatedAtUTC = student.created_at_date,
        //         LastEditDateUTC = student.last_update_date,

        //     };
        //     return studentsToReturn;
        // }

        // public Task<bool> SaveAllAsync()
        // {
        //     throw new System.NotImplementedException();
        // }
        //     private readonly DataContext _context;
        //     public StudentsRepository(DataContext context)
        //     {
        //         _context = context;

        //     }
        //     public void AddStudent(Student studentDto)
        //     {
        //         _context.Students.Add(studentDto);
        //     }

        //     public async Task<IEnumerable<Student>> GetAllStudentsAsync(int userId)
        //     {
        //        return await _context.Students.Include(e => e.Entries).Where(x => x.UserId == userId).ToListAsync();
        //     }

        //     public async Task<Student> GetStudentById(int userId)
        //     {
        //        return await _context.Students.FindAsync(userId);
        //     }

        //     public async Task<bool> SaveAllAsync()
        //     {
        //         return await _context.SaveChangesAsync() > 0;
        //     }
        // }
    }
}