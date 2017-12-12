namespace Smpl_prjct_mng_mnt_tol.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Assignment
    {
        public int id { get; set; }

        public int projectId { get; set; }

        public int userId { get; set; }

        public virtual Project Project { get; set; }

        public virtual User User { get; set; }
    }
}
