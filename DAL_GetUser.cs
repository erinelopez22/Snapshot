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
    public class DAL_GetUser
    {
        private SqlCommand cmd;
        private GlobalSettings gs = new GlobalSettings();
        public List<Model_UpdateUser> GetUser()
        {
            List<Model_UpdateUser> WebUsers = new List<Model_UpdateUser>();
            DataTable dt = new DataTable();
            GlobalSettings gs = new GlobalSettings();
            dt = DT_getUser(new SqlParameter("@mode", "GetAllPendingUser"));
            if (dt != null)
            {
                WebUsers = (from DataRow dr in dt.Rows
                            select new Model_UpdateUser()
                            {
                                EmpID = Convert.ToString(dr["UserID"]),
                                EmpName = Convert.ToString(dr["EmpName"]),
                                V_Rights = Convert.ToString(dr["ViewRights"]),
                                U_Rights = Convert.ToString(dr["UpdateRights"])
                            }).ToList();

            }
            return WebUsers;

        }
        public List<Model_UpdateUser> GetApproveUser()
        {
            List<Model_UpdateUser> WebUsers = new List<Model_UpdateUser>();
            DataTable dt = new DataTable();
            GlobalSettings gs = new GlobalSettings();
            dt = DT_getUser(new SqlParameter("@mode", "GetAllApprovedUser"));
            if (dt != null)
            {
                WebUsers = (from DataRow dr in dt.Rows
                            select new Model_UpdateUser()
                            {
                                EmpID = Convert.ToString(dr["UserID"]),
                                EmpName = Convert.ToString(dr["EmpName"]),
                                V_Rights = Convert.ToString(dr["ViewRights"]),
                                U_Rights = Convert.ToString(dr["UpdateRights"])
                            }).ToList();

            }
            return WebUsers;

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
        public List<Model_GetAllEmployeescs> GetAllEmployee()
        {
            List<Model_GetAllEmployeescs> AllEMp = new List<Model_GetAllEmployeescs>();
            DataTable dt = new DataTable();
            GlobalSettings gs = new GlobalSettings();
            dt = DT_GetAllEmployee(new SqlParameter("@mode", "GetActiveEmployee"));
            if (dt != null)
            {
                AllEMp = (from DataRow dr in dt.Rows
                            select new Model_GetAllEmployeescs()
                            {
                                UserID = Convert.ToString(dr["UserID"]),
                                FName = Convert.ToString(dr["EmpName"]),
                            }).ToList();

            }
            return AllEMp;

        }
        public DataTable DT_GetAllEmployee(params SqlParameter[] parameters)
        {
            string BookkeepingDB = gs.DB_Bookkeeping;
            string StoredProcedureName = gs.GetAllEmp;
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

        public List<Model_GetUserType> GetuserType()
        {
            List<Model_GetUserType> userType = new List<Model_GetUserType>();
            DataTable dt = new DataTable();
            GlobalSettings gs = new GlobalSettings();
            dt = DT_GetuserType(new SqlParameter("@mode", "GetUsersType"));
            if (dt != null)
            {
                userType = (from DataRow dr in dt.Rows
                          select new Model_GetUserType()
                          {
                              UserID = Convert.ToString(dr["UserTypeID"]),
                              UserType = Convert.ToString(dr["UserType"]),
                          }).ToList();

            }
            return userType;

        }
        public DataTable DT_GetuserType(params SqlParameter[] parameters)
        {
            string BookkeepingDB = gs.DB_Bookkeeping;
            string StoredProcedureName = gs.GetAllEmp;
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