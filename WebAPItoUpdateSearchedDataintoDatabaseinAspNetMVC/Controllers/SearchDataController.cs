using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebAPItoUpdateSearchedDataintoDatabaseinAspNetMVC.Controllers
{
    public class SearchDataController : ApiController
    {
        string constr = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        [HttpGet]
        public string GetSearchInfo(string rollno)
        {
            string data = "";
            SqlConnection con = new SqlConnection(constr);
            SqlCommand cmd = new SqlCommand("SpSelstudent_data", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@rollno", rollno);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                data = ds.Tables[0].Rows[0]["sname"].ToString();
                data = data + "||" + ds.Tables[0].Rows[0]["fathername"].ToString();
                data = data + "||" + ds.Tables[0].Rows[0]["mothername"].ToString();
            }
            else
            {
                data = "notfound";

            }
            con.Close();
            return (data);


        }
        [HttpGet]
        public string GetUpdateData(string updaterollno, string sname, string fname, string mname)
        {
            try
            {


                SqlConnection con = new SqlConnection(constr);
                SqlCommand cmd = new SqlCommand("SpUpdatestudent_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@rollno", updaterollno);
                cmd.Parameters.AddWithValue("@sname", sname);
                cmd.Parameters.AddWithValue("@fathername", fname);
                cmd.Parameters.AddWithValue("@mothername", mname);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return "Record with Roll Number " + updaterollno + " Has Been Updated Successfully";
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
