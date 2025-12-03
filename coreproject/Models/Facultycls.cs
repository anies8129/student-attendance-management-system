namespace coreproject.Models
{
    public class Facultycls
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Department { get; set; }

        public string Username { get; set; }   // for login table
        public string Password { get; set; }
    }
}
