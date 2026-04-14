using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementSystem.Models
{
    [Table("ProjectStatuses")]
    public class ProjectStatus
    {
        public ProjectStatus()
        {
            Projects = new HashSet<Project>();
        }

        public int id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
