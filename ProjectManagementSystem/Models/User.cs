using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementSystem.Models
{
    public class User
    {
        public User()
        {
            Assignments = new HashSet<Assignment>();
            Comments = new HashSet<Comment>();
            Roles = new HashSet<Role>();
            Tasks = new HashSet<Task>();
        }

        public int id { get; set; }

        [Required]
        [StringLength(100)]
        public string name { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [StringLength(100)]
        public string password { get; set; }

        public bool status { get; set; }

        public int designationId { get; set; }

        public virtual ICollection<Assignment> Assignments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual Designation Designation { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Task> Tasks { get; set; }
    }
}
