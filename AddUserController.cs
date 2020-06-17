
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using Snapshot_App.Dal;
using Snapshot_App.Models;

using globalSettings;
using System.Data.SqlClient;

namespace Snapshot_App.Controllers
{
    public class AddUserController : Controller
    {
        // GET: AddUser
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
        public ActionResult AddWebUsers(List<Model_UpdateUser> Tbldata, List<Model_UpdateUser> Additonaldata)
        {
            List<Model_UpdateUser> allUser = new List<Model_UpdateUser>();
            int cnt=0;
            string[] aa = { "", "", "" };
            if (Tbldata == null)
            {
                return Json(JsonConvert.SerializeObject(Additonaldata), JsonRequestBehavior.AllowGet);
            }
            else 
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("EmpID"); dt.Columns.Add("EmpName"); dt.Columns.Add("V_Rights"); dt.Columns.Add("U_Rights");
                int cnt2 = 0;
                foreach (var det in Tbldata)
               {
                   dt.Rows.Add();
                   dt.Rows[cnt2][0] = det.EmpID;
                   dt.Rows[cnt2][1] = det.EmpName;
                   dt.Rows[cnt2][2] = det.V_Rights;
                   dt.Rows[cnt2][3] = det.U_Rights;
                   cnt2 += 1;
                }
                if (Additonaldata != null)
                {
                    dt.Rows.Add();
                    dt.Rows[cnt2][0] = Additonaldata[0].EmpID;
                    dt.Rows[cnt2][1] = Additonaldata[0].EmpName;
                    dt.Rows[cnt2][2] = Additonaldata[0].V_Rights;
                    dt.Rows[cnt2][3] = Additonaldata[0].U_Rights;
                }

                if (dt != null)
                {
                    
                    //allUser = (from DataRow dr in dt.Rows
                               
                    //          select new Model_UpdateUser()
                    //          {
                                 
                    //              EmpID = Convert.ToString(dr["EmpID"]),
                    //              EmpName = Convert.ToString(dr["EmpName"]),
                    //              V_Rights = Convert.ToString(dr["V_Rights"]),
                    //              U_Rights = Convert.ToString(dr["U_Rights"])
                    //           }
                    //          ).ToList();

                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToString(dr["EmpID"]).Trim() == "") { }
                        else
                        {
                            Model_UpdateUser user = new Model_UpdateUser();
                            user.EmpID = Convert.ToString(dr["EmpID"]);
                            user.EmpName = Convert.ToString(dr["EmpName"]);
                            user.V_Rights = Convert.ToString(dr["V_Rights"]);
                            user.U_Rights = Convert.ToString(dr["U_Rights"]);
                            allUser.Add(user);
                        }
                        cnt += 1;
                    }

                    return Json(JsonConvert.SerializeObject(allUser), JsonRequestBehavior.AllowGet);
                }
                
            
            }
            return Json(JsonConvert.SerializeObject(allUser), JsonRequestBehavior.AllowGet);
            //return aa;
        }
        public ActionResult PopulateDropdow()
        {
            DAL_GetUser dal = new DAL_GetUser();
            ModelState.Clear();
            try
            {
                var result = dal.GetAllEmployee();
                return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(410, ex.Message);
            }
        }
        public string SaveUsers(List<Model_UpdateUser> AddTemp)
        {
            
            DAL_AddUser adduser = new DAL_AddUser();
            GlobalSettings gs = new GlobalSettings();
            string result = "";
            int cnt = 0;
            try
            {
                foreach (var det in AddTemp)
                {
                    Model_UpdateUser empDet = new Model_UpdateUser();

                    
                    empDet.EmpID = det.EmpID;
                    empDet.EmpName = det.EmpName;
                    empDet.V_Rights = det.V_Rights;
                    empDet.U_Rights = det.U_Rights;
                    empDet.UserType = det.UserType;
                    result = adduser.AddUser(empDet, Request.Cookies["User"].Values[0]);
                    if (result == "Success") { }
                    else { }
                }
                
            }
            catch (Exception Ex)
            {
                return Ex.Message.ToString();
            }
            return result;
        }
        public ActionResult GetUserType()
        {
            DAL_GetUser dal = new DAL_GetUser();
            ModelState.Clear();
            try
            {
                var result = dal.GetuserType();
                return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(410, ex.Message);
            }
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