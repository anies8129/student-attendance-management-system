namespace coreproject.Models
{
    public class Studentcls
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }
        public int StudentId { get; internal set; }
    }
}
