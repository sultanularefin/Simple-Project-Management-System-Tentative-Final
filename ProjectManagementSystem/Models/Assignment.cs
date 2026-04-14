using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementSystem.Models
{
    public class Assignment
    {
        public int id { get; set; }

        public int projectId { get; set; }

        public int userId { get; set; }

        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
