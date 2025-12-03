using System.Data;
using System.Data.SqlClient;

namespace coreproject.Models
{
    public class AttendanceDb
    {
        SqlConnection con = new SqlConnection(
            @"server=LAPTOP-TFPFI5HG\SQLEXPRESS;Database=coreproject;integrated security=true");

        // ⭐ GET STUDENTS BY FACULTY (department based)
        public List<Studentcls> GetStudentsByFaculty(int facultyid)
        {
            List<Studentcls> list = new List<Studentcls>();

            // 1️⃣ Get faculty department
            SqlCommand cmdDept = new SqlCommand("sp_get_faculty_department", con);
            cmdDept.CommandType = CommandType.StoredProcedure;
            cmdDept.Parameters.AddWithValue("@fid", facultyid);

            con.Open();
            string dept = cmdDept.ExecuteScalar()?.ToString();
            con.Close();

            // 2️⃣ Get students under that department
            SqlCommand cmd = new SqlCommand("sp_students_by_department", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@dept", dept);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new Studentcls
                {
                    StudentId = Convert.ToInt32(dr["studentid"]),
                    FullName = dr["fullname"].ToString()
                });
            }
            con.Close();

            return list;
        }


        // ⭐ QUICK MARK ATTENDANCE METHOD (❗ THIS FIXES YOUR ERROR)
        public string QuickMarkAttendance(int studentId, int facultyId, string status)
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                // 1️⃣ Get Max Attendance Id
                SqlCommand cmd = new SqlCommand("sp_selectmaxattendanceid", con);
                cmd.CommandType = CommandType.StoredProcedure;

                object result = cmd.ExecuteScalar();
                int maxid = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                int attendanceid = maxid + 1;

                // 2️⃣ Insert Attendance
                SqlCommand cmd1 = new SqlCommand("sp_attendanceinsert", con);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@aid", attendanceid);
                cmd1.Parameters.AddWithValue("@sid", studentId);
                cmd1.Parameters.AddWithValue("@fid", facultyId);
                cmd1.Parameters.AddWithValue("@date", DateTime.Now.Date);
                cmd1.Parameters.AddWithValue("@status", status);

                cmd1.ExecuteNonQuery();

                return "Attendance marked successfully!";
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
        public List<AttendanceViewModel> GetAttendanceByFaculty(int facultyId)
        {
            List<AttendanceViewModel> list = new List<AttendanceViewModel>();

            SqlCommand cmd = new SqlCommand("sp_attendance_by_faculty", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fid", facultyId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                list.Add(new AttendanceViewModel
                {
                    AttendanceId = Convert.ToInt32(dr["attendanceid"]),
                    StudentId = Convert.ToInt32(dr["studentid"]),
                    StudentName = dr["fullname"].ToString(),
                    Date = Convert.ToDateTime(dr["date"]),
                    Status = dr["status"].ToString()
                });
            }

            con.Close();

            return list;
        }
        public FacultyProfile GetFacultyProfile(int facultyId)
        {
            FacultyProfile obj = null;

            SqlCommand cmd = new SqlCommand("sp_faculty_profile", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@fid", facultyId);

            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                obj = new FacultyProfile
                {
                    FacultyId = Convert.ToInt32(dr["facultyid"]),
                    FullName = dr["fullname"].ToString(),
                    Email = dr["email"].ToString(),
                    Phone = dr["phone"].ToString(),
                    Department = dr["department"].ToString()
                };
            }

            con.Close();
            return obj;
        }

    }
}
