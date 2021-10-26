using System;
using System.Collections.Generic;

namespace TeacherConnect.Entities
{
    public class Entities_Student
    {
        public int student_id { get; set; }
        public int user_id { get; set; }
        public int school_id { get; set; }
        public bool is_active { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public double grade { get; set; }
        public DateTime created_at_date { get; set; }
        public DateTime last_update_date { get; set; }
        public DateTime last_successful_contact_date { get; set; }
        public DateTime last_attempted_contact_date { get; set; }
        public List<Entities_Entries> Entries { get; set; }
    }
}