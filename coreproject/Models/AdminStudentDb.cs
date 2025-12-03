using System.Data;
using System.Data.SqlClient;

namespace coreproject.Models
{
    public class AdminStudentDb
    {
        SqlConnection con = new SqlConnection(
            @"server=LAPTOP-TFPFI5HG\SQLEXPRESS;Database=coreproject;integrated security=true");

        // 1️⃣ Get All Students
        public List<Studentcls> GetAllStudents()
        {
            List<Studentcls> list = new List<Studentcls>();

            SqlCommand cmd = new SqlCommand("sp_student_getall", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new Studentcls
                {
                    StudentId = Convert.ToInt32(dr["studentid"]),
                    FullName = dr["fullname"].ToString(),
                    Email = dr["email"].ToString(),
                    Phone = dr["phone"].ToString(),
                    Department = dr["department"].ToString()
                });
            }

            con.Close();
            return list;
        }

        // 2️⃣ Insert Student
        public string InsertStudent(Studentcls obj)
        {
            try
            {
                SqlCommand cmdMax = new SqlCommand("sp_student_maxid", con);
                cmdMax.CommandType = CommandType.StoredProcedure;

                con.Open();
                object result = cmdMax.ExecuteScalar();
                con.Close();

                int sid = (result == DBNull.Value ? 0 : Convert.ToInt32(result)) + 1;

                SqlCommand cmd = new SqlCommand("sp_student_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@sid", sid);
                cmd.Parameters.AddWithValue("@name", obj.FullName);
                cmd.Parameters.AddWithValue("@email", obj.Email);
                cmd.Parameters.AddWithValue("@phone", obj.Phone);
                cmd.Parameters.AddWithValue("@dept", obj.Department);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                return "Student Added Successfully!";
            }
            catch (Exception ex)
            {
                return "error: " + ex.Message;
            }
        }

        // 3️⃣ Update Student
        public string UpdateStudent(Studentcls obj)
        {
            SqlCommand cmd = new SqlCommand("sp_student_update", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sid", obj.StudentId);
            cmd.Parameters.AddWithValue("@name", obj.FullName);
            cmd.Parameters.AddWithValue("@email", obj.Email);
            cmd.Parameters.AddWithValue("@phone", obj.Phone);
            cmd.Parameters.AddWithValue("@dept", obj.Department);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return "Student Updated Successfully!";
        }

        // 4️⃣ Delete Student
        public string DeleteStudent(int id)
        {
            SqlCommand cmd = new SqlCommand("sp_student_delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sid", id);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            return "Student Deleted Successfully!";
        }
    }
}
