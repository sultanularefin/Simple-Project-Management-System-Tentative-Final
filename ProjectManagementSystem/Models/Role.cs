using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementSystem.Models
{
    public class Role
    {
        public int id { get; set; }

        [Column("Role")]
        [StringLength(100)]
        public string Role1 { get; set; }

        public int? userId { get; set; }

        public virtual User User { get; set; }
    }
}
