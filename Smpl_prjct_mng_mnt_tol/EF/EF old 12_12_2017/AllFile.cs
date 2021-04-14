namespace Smpl_prjct_mng_mnt_tol.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AllFile
    {
        public int id { get; set; }

        public int? projectId { get; set; }

        [StringLength(200)]
        public string fname { get; set; }

        public virtual Project Project { get; set; }
    }
}
