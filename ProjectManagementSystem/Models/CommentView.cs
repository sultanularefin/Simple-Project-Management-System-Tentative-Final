using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManagementSystem.Models
{
    public class CommentView
    {
        public int id { get; set; }

        public int projectId { get; set; }

        public int taskId { get; set; }

        [Column("comment", TypeName = "text")]
        public string comment { get; set; }

        [DataType(DataType.Date)]
        public DateTime? commentDate { get; set; }

        public string commenterName { get; set; }
    }
}
