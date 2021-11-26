using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeacherConnect.Dto
{
    public class EntryDto
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public bool Contacted { get; set; }
        public int StudentId { get; set; }
        // public double Grade { get; set; }
        public DateTime CreatedAtDate { get; set; }
        public DateTime? ContactedDate { get; set; }
    }
}