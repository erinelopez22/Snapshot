using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Snapshot_App.Dal;
using Snapshot_App.Models;
using System.Data;
using globalSettings;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Security;


namespace Snapshot_App.Dal
{
    public class DAL_AddUser
    {
        private SqlCommand cmd;
        private GlobalSettings gs = new GlobalSettings();
        public string AddUser(Model_UpdateUser userDet, string CreatedBy)
        {
            List<Model_UpdateUser> Branch = new List<Model_UpdateUser>();
            string result = "";
            GlobalSettings gs = new GlobalSettings();
            try {
                result = DB_AddUser(new SqlParameter("@mode", "InsertNewUser"), new SqlParameter("@UserID", userDet.EmpID),
                            new SqlParameter("@UserTypeID", Convert.ToInt32(userDet.UserType)), new SqlParameter("@ViewRights", Convert.ToInt32(userDet.V_Rights)),
                            new SqlParameter("@UpdateRights", Convert.ToInt32(userDet.U_Rights)), new SqlParameter("@Createdby", CreatedBy));
            if (result == "Success")
            {
                return "Success";

            }
            else
            {
                return result;
            }
                }
            catch(Exception Ex)
            {
                return Ex.Message.ToString(); ;
            }
            

        }

        public string DB_AddUser(params SqlParameter[] parameters)
        {
            string BookkeepingDB = gs.DB_Bookkeeping;
            string StoredProcedureName = gs.GetAllEmp;
            string CmdStr = "";


            
            try { 
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
                    }
                    cmd.ExecuteNonQuery();
                   
                }

                 }
            }
            catch(Exception Ex)
            {
                return Ex.Message.ToString();
            }
            return "Success";
        }
    }
}