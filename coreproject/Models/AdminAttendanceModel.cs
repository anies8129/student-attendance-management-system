namespace coreproject.Models
{
    public class AdminAttendanceModel
    {
        public int AttendanceId { get; set; }
        public int FacultyId { get; set; }
        public string FacultyName { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Department { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
}
