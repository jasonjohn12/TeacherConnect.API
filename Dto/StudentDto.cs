using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeacherConnect.Entities;

namespace TeacherConnect.Dto
{
    public class StudentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Grade { get; set; }
        public DateTime CreatedAtDate { get; set; }
        public List<Entry> Entries { get; set; } = new List<Entry>();
    }
}