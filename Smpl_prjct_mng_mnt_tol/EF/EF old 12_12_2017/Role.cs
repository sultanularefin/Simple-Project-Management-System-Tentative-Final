namespace Smpl_prjct_mng_mnt_tol.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Role
    {
        public int id { get; set; }

        [Column("Role")]
        [StringLength(100)]
        public string Role1 { get; set; }

        public int? userId { get; set; }

        public virtual User User { get; set; }
    }
}
