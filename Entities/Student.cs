using System;
using System.Collections.Generic;

namespace TeacherConnect.Entities
{
    public class Student
    {
        public int Id { get; set; }
        // public int SchoolId { get; set; }
        public int UserId { get; set; }
        //  public int ClassId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Grade { get; set; }

        public DateTime CreatedAtDate { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public int CreatedByUserId { get; set; }
        // public int LastEditByUserId { get; set; }
        public List<Entry> Entries { get; set; } = new List<Entry>();
    }
}