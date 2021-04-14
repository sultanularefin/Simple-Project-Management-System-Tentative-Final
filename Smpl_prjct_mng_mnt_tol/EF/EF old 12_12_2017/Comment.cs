namespace Smpl_prjct_mng_mnt_tol.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int id { get; set; }

        public int projectId { get; set; }

        public int taskId { get; set; }

        [Column("comment", TypeName = "text")]
        public string comment { get; set; }


        //public DateTime? startDate { get; set; }
        public DateTime? commentDate { get; set; }

        public int commenterid { get; set; }

        //public virtual Project Project { get; set; }

        public virtual Task Task { get; set; }

        public virtual User User { get; set; }
    }
}
