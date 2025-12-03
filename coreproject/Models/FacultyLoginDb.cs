using System.Data;
using System.Data.SqlClient;

namespace coreproject.Models
{
    public class FacultyLoginDb
    {
        SqlConnection con = new SqlConnection(
            @"server=LAPTOP-TFPFI5HG\SQLEXPRESS;Database=coreproject;integrated security=true");

        public string LoginDb(Logincls clsobj)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                // 1️⃣ CHECK IF FACULTY EXISTS
                SqlCommand cmd = new SqlCommand("sp_facultylogin_count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@una", clsobj.Username);
                cmd.Parameters.AddWithValue("@pswd", clsobj.Password);

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (count != 1)
                {
                    return "invalid";   // wrong username or password
                }

                // 2️⃣ GET FACULTY ID
                SqlCommand cmd1 = new SqlCommand("sp_faculty_getid", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@una", clsobj.Username);
                cmd1.Parameters.AddWithValue("@pswd", clsobj.Password);

                con.Open();
                object idObj = cmd1.ExecuteScalar();
                con.Close();

                string facultyId = idObj.ToString();

                // 3️⃣ GET USER TYPE (faculty)
                SqlCommand cmd2 = new SqlCommand("sp_faculty_getusertype", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@una", clsobj.Username);
                cmd2.Parameters.AddWithValue("@pswd", clsobj.Password);

                con.Open();
                object typeObj = cmd2.ExecuteScalar();
                con.Close();

                string logtype = typeObj.ToString();

                // 4️⃣ GET FACULTY NAME
                SqlCommand cmd3 = new SqlCommand("sp_get_faculty_name", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@fid", facultyId);

                con.Open();
                object nameObj = cmd3.ExecuteScalar();
                con.Close();

                string facultyName = nameObj.ToString();

                // ⭐ RETURN FORMAT : logtype|facultyId|facultyName
                return $"{logtype}|{facultyId}|{facultyName}";
            }
            catch (Exception ex)
            {
                return "error:" + ex.Message;
            }
        }
    }
}
