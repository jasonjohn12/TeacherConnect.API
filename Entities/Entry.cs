using System;

namespace TeacherConnect.Entities
{
    public class Entry
    {
        public int Id { get; set; }

        public int CreatedByUserId { get; set; }
        public char Status { get; set; }

        public DateTime CreatedAtDate { get; set; }

        public DateTime? ContactedDate { get; set; }
        public string Note { get; set; }
        public bool Contacted { get; set; }
        public int StudentId { get; set; }
    }
}