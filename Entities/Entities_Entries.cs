using System;

namespace TeacherConnect.Entities
{
    public class Entities_Entries
    {
        public int entry_id { get; set; }
        public int student_id { get; set; }
        public int user_id { get; set; }
        public int school_id { get; set; }
        public bool contacted { get; set; }
        public int created_by_user_id { get; set; }
        public DateTime created_at_date { get; set; }
        public DateTime last_update_date { get; set; }
        public DateTime last_attempted_contact_date { get; set; }
        public string note { get; set; }
    }
}