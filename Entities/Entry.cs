using System;

namespace TeacherConnect.Entities
{
    public class Entry
    {
        public int Id { get; set; }
        // public int UserId { get; set; }
        // public int SchoolId { get; set; }
        public int CreatedByUserId { get; set; }
        //public int LastEditByUserId { get; set; }
        public DateTime CreatedAtDate { get; set; }
        // public DateTime LastUpdatedDate { get; set; }
        public DateTime? ContactedDate { get; set; }
        public string Note { get; set; }
        public bool Contacted { get; set; }
        // public Student Student { get; set; }
        public int StudentId { get; set; }
    }
}