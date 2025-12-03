using System.Data;
using System.Data.SqlClient;

namespace coreproject.Models
{
    public class AdminFacultyDb
    {
        SqlConnection con = new SqlConnection(
            @"server=LAPTOP-TFPFI5HG\SQLEXPRESS;Database=coreproject;integrated security=true");

        // 1️⃣ Get All Faculty
        public List<FacultyModel> GetAllFaculty()
        {
            List<FacultyModel> list = new List<FacultyModel>();

            SqlCommand cmd = new SqlCommand("sp_faculty_getall", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new FacultyModel
                {
                    FacultyId = Convert.ToInt32(dr["facultyid"]),
                    FullName = dr["fullname"].ToString(),
                    Email = dr["email"].ToString(),
                    Phone = dr["phone"].ToString(),
                    Department = dr["department"].ToString()
                });
            }

            con.Close();
            return list;
        }

        // 2️⃣ Insert Faculty
        public string InsertFaculty(FacultyModel obj)
        {
            try
            {
                SqlCommand cmdMax = new SqlCommand("sp_faculty_maxid", con);
                cmdMax.CommandType = CommandType.StoredProcedure;

                con.Open();
                object result = cmdMax.ExecuteScalar();
                con.Close();

                int fid = (result == DBNull.Value ? 0 : Convert.ToInt32(result)) + 1;

                SqlCommand cmd = new SqlCommand("sp_faculty_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@fid", fid);
                cmd.Parameters.AddWithValue("@name", obj.FullName);
                cmd.Parameters.AddWithValue("@email", obj.Email);
                cmd.Parameters.AddWithValue("@phone", obj.Phone);
                cmd.Parameters.AddWithValue("@dept", obj.Department);
                cmd.Parameters.AddWithValue("@password", obj.Password);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return "Faculty Added Successfully!";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        // 3️⃣ Update Faculty
        public string UpdateFaculty(FacultyModel obj)
        {
            SqlCommand cmd = new SqlCommand("sp_faculty_update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@fid", obj.FacultyId);
            cmd.Parameters.AddWithValue("@name", obj.FullName);
            cmd.Parameters.AddWithValue("@email", obj.Email);
            cmd.Parameters.AddWithValue("@phone", obj.Phone);
            cmd.Parameters.AddWithValue("@dept", obj.Department);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return "Faculty Updated Successfully!";
        }

        // 4️⃣ Delete Faculty
        public string DeleteFaculty(int id)
        {
            SqlCommand cmd = new SqlCommand("sp_faculty_delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fid", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return "Faculty Deleted Successfully!";
        }
    }
}
