using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Data;
using Snapshot_App.Dal;
using Snapshot_App.Models;


namespace Snapshot_App.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
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
        public ActionResult GetUserName()
        {
            string UserID = @Request.Cookies["User"]["Username"];
            return Json(JsonConvert.SerializeObject(UserID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUserID()
        {
            string UserID = @Request.Cookies["User"]["UserID"];
            return Json(JsonConvert.SerializeObject(UserID), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetBranch()
        {
            DAL_GetBranch Dal = new DAL_GetBranch();
            ModelState.Clear();
            var result = Dal.GetBranch();
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetMonths()
        {

            DAL_GetMonth_Year Dal = new DAL_GetMonth_Year();
            ModelState.Clear();
            var result = Dal.GetMonths();
            return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
        }
    }
}