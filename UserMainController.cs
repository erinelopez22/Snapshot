using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using Snapshot_App.Dal;
using Snapshot_App.Models;
using System.Data;
using globalSettings;
using System.Data.SqlClient;


namespace Snapshot_App.Controllers
{
    
    public class UserMainController : Controller
    {
        // GET: UserMain
        GlobalSettings gs = new GlobalSettings();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserAccess()
        {
            string UserID = Request.Cookies["User"].Values[0];
            string FName = Request.Cookies["User"].Values[1];
            try
            {

                Model_getUser result = new Model_getUser();
                result.UserID = Request.Cookies["User"].Values[0];
                result.UserName = Request.Cookies["User"].Values[1];
                result.Empname = Request.Cookies["User"].Values[2];
                result.UserType = Request.Cookies["User"].Values[3];
                return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(410, ex.Message);
            }
        }
        public ActionResult GetWebUsers()
        {
            DAL_GetUser dal = new DAL_GetUser();
            ModelState.Clear();
            try
            {
                var result = dal.GetUser();
                return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(410, ex.Message);
            }
        }
       
        public void UpdateWebUsers(List<Model_UpdateUser> erine)
        {
            string[] aa = {"","",""};
            List<Model_UpdateUser> Branch = new List<Model_UpdateUser>();
            DataTable dt = new DataTable();
            GlobalSettings gs = new GlobalSettings();
            dt = DT_getUser(new SqlParameter("@mode", "GetUserbyUserID"));
            if (dt != null)
            {
                Branch = (from DataRow dr in dt.Rows
                          select new Model_UpdateUser()
                          {
                              EmpID = Convert.ToString(dr["UserID"]),
                              EmpName = Convert.ToString(dr["empName"]),
                              V_Rights = Convert.ToString(dr["ViewRights"]),
                              U_Rights = Convert.ToString(dr["UpdateRights"])
                          }).ToList();



            }
            //return aa;
        }
        public DataTable DT_getUser(params SqlParameter[] parameters)
        {
            string BookkeepingDB = gs.DB_Bookkeeping;
            string StoredProcedureName = gs.GetUser;
            string CmdStr = "";


            DataTable dt = new DataTable();
            using (SqlConnection cn = new SqlConnection(BookkeepingDB))
            {
                cn.Open();
                using (SqlCommand cmd = new SqlCommand(StoredProcedureName, cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    foreach (SqlParameter item in parameters)
                    {
                        cmd.Parameters.Add(item);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        da.Dispose();
                    }
                    cn.Close();
                }

                return dt;
            }
        }
    }
}