using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smpl_prjct_mng_mnt_tol.EF
{
    public class ProjectSpecificTaskView
    {

        public int id { get; set; }

        public int projectId { get; set; }

        [StringLength(100)]
        public string taskHeading { get; set; }

        public string assigneeName { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        public string priorityName { get; set; }

        public string assigndbyName { get; set; }

        [Display(Name = "Due Date")]
        [DataType(DataType.Date)]
        public DateTime dueDate { get; set; }

       
    }
}