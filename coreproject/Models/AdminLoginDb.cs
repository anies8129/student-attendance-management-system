using System.Data;
using System.Data.SqlClient;

namespace coreproject.Models
{
    public class AdminLoginDb
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-TFPFI5HG\SQLEXPRESS;Database=coreproject;integrated security=true");

        public string LoginDb(Logincls clsobj)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                // 1️⃣ Check login record exists
                SqlCommand cmd = new SqlCommand("sp_adminlogin_count", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@una", clsobj.Username);
                cmd.Parameters.AddWithValue("@pswd", clsobj.Password);

                con.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                con.Close();

                if (count != 1)
                {
                    return "invalid";
                }

                // 2️⃣ Get admin ID
                SqlCommand cmd1 = new SqlCommand("sp_admin_getid", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@una", clsobj.Username);
                cmd1.Parameters.AddWithValue("@pswd", clsobj.Password);

                con.Open();
                object uidObj = cmd1.ExecuteScalar();
                con.Close();

                if (uidObj == null || uidObj == DBNull.Value)
                {
                    return "invalid";
                }

                string uid = uidObj.ToString();

                // 3️⃣ Get role (admin)
                SqlCommand cmd2 = new SqlCommand("sp_admin_getusertype", con);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@una", clsobj.Username);
                cmd2.Parameters.AddWithValue("@pswd", clsobj.Password);

                con.Open();
                object roleObj = cmd2.ExecuteScalar();
                con.Close();

                if (roleObj == null || roleObj == DBNull.Value)
                {
                    return "invalid";
                }

                string role = roleObj.ToString();

                // 4️⃣ Get Admin Name (NEW)
                SqlCommand cmd3 = new SqlCommand("sp_admin_getname", con);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@aid", Convert.ToInt32(uid));

                con.Open();
                object nameObj = cmd3.ExecuteScalar();
                con.Close();

                if (nameObj == null || nameObj == DBNull.Value)
                {
                    return $"{role}|{uid}|Unknown";
                }

                string adminName = nameObj.ToString();

                // ⭐ Return role | id | name
                return $"{role}|{uid}|{adminName}";
            }
            catch (Exception ex)
            {
                return "error:" + ex.Message;
            }
        }

    }
}
