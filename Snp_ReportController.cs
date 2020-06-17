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
    public class Snp_ReportController : Controller
    {
        // GET: Snp_Report
        public ActionResult Index()
        {
            
            
            return View();
        }
        public ActionResult GetWebUsers()
        {
            string UserID = Request.Cookies["User"].Values[0];
            string FName = Request.Cookies["User"].Values[1];
            try
            {
                var result = FName;
                return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(410, ex.Message);
            }
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
    }
}