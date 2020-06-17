using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Snapshot_App.Dal;
using Snapshot_App.Models;
using System.Data;
using globalSettings;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using globalSettings;
using System.Web.Security;  

namespace Snapshot_App.Dal
{
    public class DAL_Login
    {
        private SqlCommand cmd;
        private GlobalSettings gs = new GlobalSettings();
        public DataTable GetEmployees(params SqlParameter[] parameters)
        {
            string BookkeepingDB = gs.DB_Bookkeeping;
            string StoredProcedureName = gs.BK_SP;
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

                        
                    }
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                    cn.Close();
                }

                return dt;
            }
        }
       
    }
}