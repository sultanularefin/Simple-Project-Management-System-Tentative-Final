using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementSystem.Models
{
    public class Task
    {
        public Task()
        {
            Comments = new HashSet<Comment>();
        }

        public int id { get; set; }

        public int projectId { get; set; }

        [Required]
        [StringLength(100)]
        public string taskHeading { get; set; }

        public int assigneeid { get; set; }

        public int priorityId { get; set; }

        public int assignedbyId { get; set; }

        public DateTime dueDate { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string description { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
