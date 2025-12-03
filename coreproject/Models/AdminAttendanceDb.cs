using System.Data;
using System.Data.SqlClient;

namespace coreproject.Models
{
    public class AdminAttendanceDb
    {
        SqlConnection con = new SqlConnection(
            @"server=LAPTOP-TFPFI5HG\SQLEXPRESS;Database=coreproject;integrated security=true");

        public List<AdminAttendanceModel> FilterAttendance(
            int? studentid, int? facultyid, string department, DateTime? date)
        {
            List<AdminAttendanceModel> list = new List<AdminAttendanceModel>();

            SqlCommand cmd = new SqlCommand("sp_admin_attendance_filter", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@studentid", (object)studentid ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@facultyid", (object)facultyid ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@department", (object)department ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@date", (object)date ?? DBNull.Value);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new AdminAttendanceModel
                {
                    AttendanceId = Convert.ToInt32(dr["attendanceid"]),
                    FacultyId = Convert.ToInt32(dr["facultyid"]),
                    FacultyName = dr["facultyname"].ToString(),
                    StudentId = Convert.ToInt32(dr["studentid"]),
                    StudentName = dr["studentname"].ToString(),
                    Department = dr["department"].ToString(),
                    Status = dr["status"].ToString(),
                    Date = Convert.ToDateTime(dr["date"]).ToString("dd-MM-yyyy")
                });
            }

            con.Close();
            return list;
        }
    }
}
