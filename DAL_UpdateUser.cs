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
    public class DAL_UpdateUser
    {
        public List<Model_getUser> GetUser()
        {
            List<Model_getUser> WebUsers = new List<Model_getUser>();
            DataTable dt = new DataTable();
            GlobalSettings gs = new GlobalSettings();
            dt = DT_getUser(new SqlParameter("@mode", "GetWebUsers"));
            if (dt != null)
            {
                WebUsers = (from DataRow dr in dt.Rows
                            select new Model_getUser()
                            {
                                UserID = Convert.ToString(dr["UserID"]),
                                Empname = Convert.ToString(dr["User Name"]),
                                UserType = Convert.ToString(dr["User Type"]),
                                Active = Convert.ToString(dr["Active"])
                            }).ToList();

            }
            return WebUsers;

        }
        private SqlCommand cmd;
        private GlobalSettings gs = new GlobalSettings();
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