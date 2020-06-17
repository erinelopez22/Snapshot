using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using globalSettings;
using Snapshot_App.Dal;
using System.Web.Security;

using Newtonsoft.Json;


namespace Snapshot_App.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string Username, string Password)
        {

            GlobalSettings gs = new GlobalSettings();
            DAL_Login DalLogin = new DAL_Login();
            try
            {
                
                ModelState.Clear();
                DataTable dt = DalLogin.GetEmployees( new SqlParameter("@UserName", Username),
                                new SqlParameter("@Password", Password),
                                new SqlParameter("@ProgramName", gs.Appname1),
                                new SqlParameter("@IP", gs.GetLocalIPAddress()));

                if (dt.Rows.Count > 0)
                {
                    HttpCookie cookie = new HttpCookie("User");
                    cookie.Values.Add("UserID", dt.Rows[0][0].ToString());
                    cookie.Values.Add("Username", Username);
                    cookie.Values.Add("EmpName", dt.Rows[0][2].ToString());
                    cookie.Values.Add("UserType", dt.Rows[0][1].ToString());
                    

                    cookie.HttpOnly = true;
                    Response.Cookies.Add(cookie);
                    FormsAuthentication.SetAuthCookie(dt.Rows[0][0].ToString(), false);
                    string a = Request.Cookies["User"].Values[0];
                    return Json(new { success = true, message = "Login Successful.", JsonRequestBehavior = JsonRequestBehavior.AllowGet });
                }
                else { throw new ApplicationException("Invalid Username or Password"); }

                
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(410, ex.Message);
            }
    }

    }
}