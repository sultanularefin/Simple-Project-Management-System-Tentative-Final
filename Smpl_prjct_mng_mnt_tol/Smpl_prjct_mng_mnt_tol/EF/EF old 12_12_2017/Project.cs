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
            Comments = new HashSet<Comment>();
            Tasks = new HashSet<Task>();
        }

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
        public DateTime? startDate { get; set; }

        [Display(Name = "Possible End Date")]
        public DateTime? endDate { get; set; }

        [Display(Name = "Duration (in Days)")]
        public int? duration { get; set; }

        [Display(Name = "Upload File")]
        public int? filesId { get; set; }

        [Display(Name = "Status")]
        public int? statusId { get; set; }

        [StringLength(50)]
        public string shortName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AllFile> AllFiles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Assignment> Assignments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ProjectStatus ProjectStatus { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
