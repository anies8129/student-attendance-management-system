using System.Data;
using System.Data.SqlClient;

namespace coreproject.Models
{
    public class NewfacultyDb
    {
        SqlConnection con = new SqlConnection(
           @"server=LAPTOP-TFPFI5HG\SQLEXPRESS;Database=coreproject;integrated security=true");

        //public FacultyProfile GetFacultyProfile(int facultyId)
        //{
        //    FacultyProfile obj = new FacultyProfile();

        //    SqlCommand cmd = new SqlCommand("sp_faculty_profile", con);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.AddWithValue("@fid", facultyId);

        //    con.Open();
        //    SqlDataReader dr = cmd.ExecuteReader();

        //    if (dr.Read())
        //    {
        //        obj.FacultyId = Convert.ToInt32(dr["facultyid"]);
        //        obj.FullName = dr["fullname"].ToString();
        //        obj.Email = dr["email"].ToString();
        //        obj.Phone = dr["phone"].ToString();
        //        obj.Department = dr["department"].ToString();
        //    }

        //    con.Close();

        //    return obj;
        //}

    }

}
