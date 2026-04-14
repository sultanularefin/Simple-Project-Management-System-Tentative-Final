using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementSystem.Models
{
    public class Comment
    {
        public int id { get; set; }

        [NotMapped]
        public int projectId { get; set; }

        public int taskId { get; set; }

        [Column("comment", TypeName = "text")]
        [Required]
        public string comment { get; set; }

        [DataType(DataType.DateTime)]
        [Column(TypeName = "DateTime")]
        public DateTime commentDate { get; set; }

        public int commenterid { get; set; }

        public virtual Task Task { get; set; }
        public virtual User User { get; set; }
    }
}
