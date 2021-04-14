using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smpl_prjct_mng_mnt_tol.EF
{
    public class ProjectDetail
    {

        public int id { get; set; }

        [StringLength(300)]
        [Display(Name = "Name")]
        public string name { get; set; }

        [StringLength(200)]
        [Display(Name = "Code Name")]
        public string codeName { get; set; }


        [Column(TypeName = "text")]
        [Display(Name = "Description")]
        public string description { get; set; }


        [Display(Name = "Possible Start Date")]
        [DataType(DataType.Date)]

        public DateTime? startDate { get; set; }


        [Display(Name = "Possible End Date")]
        [DataType(DataType.Date)]

        public DateTime? endDate { get; set; }


        [Display(Name = "Duration (in Days)")]
        public int? duration { get; set; }

        [Display(Name = "Status")]
        public string statusName { get; set; }

        [StringLength(50)]
        public string shortName { get; set; }

       
    }
}

