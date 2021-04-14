using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Smpl_prjct_mng_mnt_tol.EF
{
    public class ProjectView

    {
        public int id { get; set; }

     
        [Display(Name = "Name")]
        public string name { get; set; }


        [Display(Name = "short Name")]
        public string shortName { get; set; }

        [Display(Name = "Status")]
        public string statusname { get; set; }



        [Display(Name = "Number of Members")]
        public int? memberCount { get; set; }

        [Display(Name = "No of Tasks")]
        public int? taskCount { get; set; }













    }
}