namespace Smpl_prjct_mng_mnt_tol.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Project
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Project()
        {
            AllFiles = new HashSet<AllFile>();
            Assignments = new HashSet<Assignment>();
            Tasks = new HashSet<Task>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(300)]
        public string name { get; set; }

        [Required]
        [StringLength(200)]
        public string codeName { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime startDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime endDate { get; set; }

        public int duration { get; set; }

        public int? filesId { get; set; }

        public int? statusId { get; set; }

        [StringLength(50)]
        public string shortName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllFile> AllFiles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assignment> Assignments { get; set; }

        public virtual ProjectStatus ProjectStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
