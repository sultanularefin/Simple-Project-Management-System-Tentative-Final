using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Smpl_prjct_mng_mnt_tol.EF
{
    public class AssignView
    {
        public int id { get; set; }

        public int projectId{ get; set; }
        public string  projectName { get; set; }

        public int  userId { get; set; }


        public string userName { get; set; }

        public string designation { get; set; }

    }
}