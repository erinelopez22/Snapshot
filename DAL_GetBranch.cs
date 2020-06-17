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
    public class DAL_GetBranch
    {
        private SqlCommand cmd;
        private GlobalSettings gs = new GlobalSettings();
        public List<Model_GetBranch> GetBranch()
        {
            List<Model_GetBranch> Branch = new List<Model_GetBranch>();
            DataTable dt = new DataTable();
            GlobalSettings gs = new GlobalSettings();
            dt = DT_getBranch(new SqlParameter("@mode", "GetBranch"));
            if (dt != null)
            {
                Branch = (from DataRow dr in dt.Rows
                          select new Model_GetBranch()
                          {
                              Whscode = Convert.ToString(dr["WhsCode"]),
                              whsName = Convert.ToString(dr["WhsName"])
                          }).ToList();

            }
            return Branch;

        }
       
        public DataTable DT_getBranch(params SqlParameter[] parameters)
        {
            string BookkeepingDB = gs.DB_Bookkeeping;
            string StoredProcedureName = gs.GetBranch;
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