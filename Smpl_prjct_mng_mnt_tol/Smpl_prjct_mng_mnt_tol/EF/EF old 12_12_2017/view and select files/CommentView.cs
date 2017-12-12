using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Smpl_prjct_mng_mnt_tol.EF
{
    public class CommentView
    {
        public int id { get; set; }

        public int projectId { get; set; }

        public int taskId { get; set; }

        [Column("comment", TypeName = "text")]
        public string comment { get; set; }


        //public DateTime? startDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? commentDate { get; set; }

        public string commenterName { get; set; }

       
    }
}