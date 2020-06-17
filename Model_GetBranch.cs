using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Snapshot_App.Models
{
    public class Model_GetBranch
    {
        
            public string Whscode { get; set; }
            public string whsName { get; set; }
            public IEnumerable<Model_GetBranch> Branches { get; set; }
        
    }
}