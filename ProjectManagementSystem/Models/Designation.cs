using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementSystem.Models
{
    public class Designation
    {
        public Designation()
        {
            Users = new HashSet<User>();
        }

        public int id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
