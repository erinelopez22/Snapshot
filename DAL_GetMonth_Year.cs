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
    public class DAL_GetMonth_Year
    {
        public List<Model_GetMonth> GetMonths()
        {
            List<Model_GetMonth> Months = new List<Model_GetMonth>();
            DataTable dt = new DataTable();
            GlobalSettings gs = new GlobalSettings();
            dt = DT_getMOnth( new SqlParameter("@mode", "GetMonths"));
            if (dt != null)
            {
                Months = (from DataRow dr in dt.Rows
                          select new Model_GetMonth()
                          {
                              MonthNum = Convert.ToInt32(dr["Monthnum"]),
                              MonthName = Convert.ToString(dr["MonthName"])
                          }).ToList();
            }
            return Months;

        }
        private SqlCommand cmd;
        private GlobalSettings gs = new GlobalSettings();
        public DataTable DT_getMOnth(params SqlParameter[] parameters)
        {
            string BookkeepingDB = gs.DB_Bookkeeping;
            string StoredProcedureName = gs.GetCalendar;
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
       
        //-------------------------------------------
    //    public List<TrialBalance_Mod> GenerateTB(string Branch, int month, int year, bool posted)
    //    {
    //        List<TrialBalance_Mod> TBList = new List<TrialBalance_Mod>();
    //        try
    //        {
    //            DataTable dt = new DataTable();
    //            ClsDataLayer clsdl = new ClsDataLayer();
    //            GlobalSettings gs = new GlobalSettings();
    //            dt = clsdl.GetDt(gs.Bookkeeping, "sp_TrialBalanceWorksheet", new SqlParameter("@mode", "GenerateTB"),
    //                new SqlParameter("@month", month), new SqlParameter("@year", year), new SqlParameter("@WhsCode", Branch), new SqlParameter("@Posted", posted));

    //            if (dt != null)
    //            {
    //                TBList = (from DataRow dr in dt.Rows
    //                          select new TrialBalance_Mod()
    //                          {
    //                              // TransID = Convert.ToInt32(dr["TransID"]),
    //                              FormatCode = Convert.ToString(dr["FormatCode"]),
    //                              AcctCode = Convert.ToString(dr["AcctCode"]),
    //                              AcctName = Convert.ToString(dr["AcctName"]),
    //                              BegDr = Convert.ToDecimal(dr["BegDr"]),
    //                              BegCr = Convert.ToDecimal(dr["BegCr"]),
    //                              NetDr = Convert.ToDecimal(dr["NetDr"]),
    //                              NetCr = Convert.ToDecimal(dr["NetCr"]),
    //                              EndDr = Convert.ToDecimal(dr["EndDr"]),
    //                              EndCr = Convert.ToDecimal(dr["EndCr"]),
    //                              RunningDr = Convert.ToDecimal(dr["RunningDr"]),
    //                              RunningCr = Convert.ToDecimal(dr["RunningCr"]),
    //                              VarianceDr = Convert.ToDecimal(dr["VarianceDr"]),
    //                              VarianceCr = Convert.ToDecimal(dr["VarianceCr"]),
    //                              TransID = Convert.ToInt32(dr["TransID"])
    //                          }).ToList();
    //            }
    //            else
    //            {

    //                if (posted == true)
    //                {
    //                    throw new ApplicationException("No posted Trial Balance for previous period. Please check.");
    //                }

    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ApplicationException(ex.Message);
    //        }

    //        return TBList;



    //    }
    }
}