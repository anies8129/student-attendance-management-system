using System.Data;
using System.Data.SqlClient;

namespace coreproject.Models
{
    public class FacultyDb
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-TFPFI5HG\SQLEXPRESS;Database=coreproject;integrated security=true");

        public string InsertFaculty(Facultycls clsobj)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                // 1️⃣ Get max faculty id
                SqlCommand cmd = new SqlCommand("sp_selectmaxfacultyid", con);
                cmd.CommandType = CommandType.StoredProcedure;

                object result = cmd.ExecuteScalar();
                int maxid = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                int facultyid = maxid + 1;

                // 2️⃣ Insert into faculty table
                SqlCommand cmd1 = new SqlCommand("sp_facultyinsert", con);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@fid", facultyid);
                cmd1.Parameters.AddWithValue("@fname", clsobj.FullName);
                cmd1.Parameters.AddWithValue("@femail", clsobj.Email);
                cmd1.Parameters.AddWithValue("@fphone", clsobj.Phone);
                cmd1.Parameters.AddWithValue("@fdept", clsobj.Department);

                cmd1.ExecuteNonQuery();

                // 3️⃣ Insert into login table
                SqlCommand cmd2 = new SqlCommand("sp_logininsert", con);
                cmd2.CommandType = CommandType.StoredProcedure;

                cmd2.Parameters.AddWithValue("@rid", facultyid);
                cmd2.Parameters.AddWithValue("@una", clsobj.Username);
                cmd2.Parameters.AddWithValue("@pswd", clsobj.Password);
                cmd2.Parameters.AddWithValue("@logty", "faculty");

                cmd2.ExecuteNonQuery();

                return "Faculty Inserted Successfully!";
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
        public FacultyProfile GetFacultyProfile(int facultyId)
        {
            FacultyProfile obj = new FacultyProfile();

            SqlCommand cmd = new SqlCommand("sp_faculty_profile", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fid", facultyId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                obj.FacultyId = Convert.ToInt32(dr["facultyid"]);
                obj.FullName = dr["fullname"].ToString();
                obj.Email = dr["email"].ToString();
                obj.Phone = dr["phone"].ToString();
                obj.Department = dr["department"].ToString();
            }

            con.Close();

            return obj;
        }
        public string UpdateFacultyProfile(FacultyProfile obj)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_faculty_update", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@fid", obj.FacultyId);
                cmd.Parameters.AddWithValue("@fullname", obj.FullName);
                cmd.Parameters.AddWithValue("@email", obj.Email);
                cmd.Parameters.AddWithValue("@phone", obj.Phone);
                cmd.Parameters.AddWithValue("@department", obj.Department);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return "Profile updated successfully!";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

    }
}

