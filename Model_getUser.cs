using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snapshot_App.Models
{
    public class Model_getUser
    {
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Empname { get; set; }
        public string UserType { get; set; }
        public string Add { get; set; }
        public string Edit { get; set; }
        public string Delete { get; set; }
        public string View { get; set; }
        public string Print { get; set; }
        public string Import { get; set; }
        public string Export { get; set; }
        public string Post { get; set; }
        public string Unpost { get; set; }
        public string Approve { get; set; }
        public string ViewNewVer { get; set; }


        public int UserTypeID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime DateMoodified { get; set; }
        public string Active { get; set; }
        public string DeletedBy { get; set; }
        public DateTime DateDeleted { get; set; }

        public string Version { get; set; }
    }
}