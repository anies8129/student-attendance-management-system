namespace coreproject.Models
{
    public class AttendanceViewModel
    {
        public int AttendanceId { get; set; }
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
