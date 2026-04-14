using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementSystem.Models
{
    public class Priority
    {
        public Priority()
        {
            Tasks = new HashSet<Task>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string priorityName { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
