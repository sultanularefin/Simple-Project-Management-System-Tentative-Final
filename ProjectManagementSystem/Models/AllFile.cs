using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementSystem.Models
{
    public class AllFile
    {
        public int id { get; set; }

        public int projectId { get; set; }

        [StringLength(200)]
        public string fname { get; set; }

        public virtual Project Project { get; set; }
    }
}
