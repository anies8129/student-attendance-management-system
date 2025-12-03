namespace coreproject.Models
{
    public class Attendancecls
    {
        public int AttendanceId { get; set; }   // optional, auto-generated (max + 1)

        public int StudentId { get; set; }      // selected student
        public string StudentName { get; set; } // used only for dropdown display

        public int FacultyId { get; set; }      // taken from session, optional in model

        public string Status { get; set; }      // Present / Absent

        public DateTime Date { get; set; } = DateTime.Now.Date; // auto add today's date

    }
}
