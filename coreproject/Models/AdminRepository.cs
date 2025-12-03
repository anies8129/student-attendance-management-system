using coreproject.Models;
using System.Data;
using System.Data.SqlClient;


namespace coreproject.Models
{
    public class AdminRepository
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-TFPFI5HG\SQLEXPRESS;Database=coreproject;integrated security=true");


        public string InsertAdmin(Admin clsobj)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

                con.Open();

                // 1️⃣ Get max admin id
                SqlCommand cmd = new SqlCommand("sp_selectmaxadminid", con);
                cmd.CommandType = CommandType.StoredProcedure;

                object result = cmd.ExecuteScalar();
                int maxid = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                int adminid = maxid + 1;

                // 2️⃣ Insert into admin table
                SqlCommand cmd1 = new SqlCommand("sp_admininsert", con);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@aid", adminid);
                cmd1.Parameters.AddWithValue("@name", clsobj.name);
                cmd1.Parameters.AddWithValue("@email", clsobj.email);
                cmd1.Parameters.AddWithValue("@phone", clsobj.phone);

                cmd1.ExecuteNonQuery();


                // 3️⃣ Insert into login table
                SqlCommand cmd2 = new SqlCommand("sp_logininsert", con);
                cmd2.CommandType = CommandType.StoredProcedure;

                cmd2.Parameters.AddWithValue("@rid", adminid);
                cmd2.Parameters.AddWithValue("@una", clsobj.username);
                cmd2.Parameters.AddWithValue("@pswd", clsobj.password);
                cmd2.Parameters.AddWithValue("@logty", "admin");

                cmd2.ExecuteNonQuery();

                return "Admin inserted successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (con.State != ConnectionState.Closed)
                    con.Close();
            }
        }
    }
}

