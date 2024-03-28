using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using DEMO11.Models;
using System.IO;

namespace DEMO11.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Registration r)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-6IF5N4H5\\SQLEXPRESS;Initial Catalog=store;Integrated security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("S_regis", con);
            string Pic = Path.Combine(Server.MapPath("~/Content/Image"), r.pic.FileName);
            r.pic.SaveAs(Pic);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", r.name);
            cmd.Parameters.AddWithValue("@email", r.email);
            cmd.Parameters.AddWithValue("@password", r.password);
            cmd.Parameters.AddWithValue("@cpassword", r.Cpassowrd);
            cmd.Parameters.AddWithValue("@mobile", r.phone);
            cmd.Parameters.AddWithValue("@adhar_no", r.adhar_No);
            cmd.Parameters.AddWithValue("@pic", r.pic.FileName);
            int n=cmd.ExecuteNonQuery();
            if(n>0)
            {
                Response.Write("save");
            }
            else
            {
                Response.Write
                    ("not save");
            }
            return View();
        }
        public ActionResult showregis()
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-6IF5N4H5\\SQLEXPRESS;Initial Catalog=store;Integrated security=true");
           DataSet ds = new DataSet();
            SqlDataAdapter sa = new SqlDataAdapter();
            
            SqlCommand cmd = new SqlCommand("S_sel", con);
            cmd.CommandType= CommandType.StoredProcedure;
            List<Registration> lst=new List<Registration> ();
            sa.SelectCommand = cmd;
            sa.Fill(ds);
            for (int i=0; i < ds.Tables[0].Rows.Count;i++)
            {
                Registration r = new Registration();
                r.name = ds.Tables[0].Rows[i]["Name"].ToString();
                r.email = ds.Tables[0].Rows[i]["email"].ToString();
                r.password = ds.Tables[0].Rows[i]["password"].ToString();
                r.Cpassowrd = ds.Tables[0].Rows[i]["cpassword"].ToString();
                r.phone = ds.Tables[0].Rows[i]["mobile"].ToString();
                r.adhar_No = ds.Tables[0].Rows[i]["adhar_no"].ToString();
                ViewBag.pic = ds.Tables[0].Rows[i]["Pic"].ToString();
           lst.Add(r);
            }
            return View(lst);

        }
        public ActionResult Delres(string del)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-6IF5N4H5\\SQLEXPRESS;Initial Catalog=store;Integrated security=true");
            con.Open();
            SqlCommand cmd = new SqlCommand("delres", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("email", del);
            int n =cmd.ExecuteNonQuery();
            if(n > 0)
            {
                Response.Redirect("/User/showregis");
            }
            else
            {
                Response.Write("not deleted");
            }

            return View();
        }
        [HttpGet]
        public ActionResult Updetres(string up)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-6IF5N4H5\\SQLEXPRESS;Initial Catalog=store;Integrated security=true");
            DataTable dt=new DataTable();
            string query = "select * from regis where email='" + up + "'";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter sa=new SqlDataAdapter(cmd);
            sa.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                ViewBag.name = dt.Rows[0]["name"];
                ViewBag.email = dt.Rows[0]["email"];
                ViewBag.password = dt.Rows[0]["password"];
                ViewBag.cpassword = dt.Rows[0]["cpassword"];
                ViewBag.number = dt.Rows[0]["mobile"];
                ViewBag.adhar = dt.Rows[0]["adhar_no"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Updatres(string txtname,string txtemail,string txtpass,string txtcpass,string txtmo,string txtnum)
        {
            SqlConnection con = new SqlConnection("Data Source=LAPTOP-6IF5N4H5\\SQLEXPRESS;Initial Catalog=store;Integrated security=true");
            con.Open();
            SqlCommand cmd=new SqlCommand("S_up", con);
            cmd.CommandType=CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", txtname);
            cmd.Parameters.AddWithValue("@email", txtemail);
            cmd.Parameters.AddWithValue("@password", txtpass);
            cmd.Parameters.AddWithValue("@cpassword", txtcpass);
            cmd.Parameters.AddWithValue("@mobile", txtmo);
            cmd.Parameters.AddWithValue("@adhar_no", txtnum);
            int n=cmd.ExecuteNonQuery();
            if (n > 0) {
                Response.Write("update success");

            }
            else
            {
                Response.Write("not update");
            }
            return View();
        }
    }
}