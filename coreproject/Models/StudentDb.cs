using System.Data;
using System.Data.SqlClient;

namespace coreproject.Models
{
    public class StudentDb
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-TFPFI5HG\SQLEXPRESS;Database=coreproject;integrated security=true");

        public string InsertStudent(Studentcls clsobj)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                // 1️⃣ Get max student id
                SqlCommand cmd = new SqlCommand("sp_selectmaxstudentid", con);
                cmd.CommandType = CommandType.StoredProcedure;

                object result = cmd.ExecuteScalar();
                int maxid = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                int studentid = maxid + 1;

                // 2️⃣ Insert into studenttable
                SqlCommand cmd1 = new SqlCommand("sp_studentinsert", con);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@sid", studentid);
                cmd1.Parameters.AddWithValue("@sname", clsobj.FullName);
                cmd1.Parameters.AddWithValue("@semail", clsobj.Email);
                cmd1.Parameters.AddWithValue("@sphone", clsobj.Phone);
                cmd1.Parameters.AddWithValue("@sdept", clsobj.Department);

                cmd1.ExecuteNonQuery();

                return "Student Inserted Successfully!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}

