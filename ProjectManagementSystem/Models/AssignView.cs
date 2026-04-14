using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class AssignView
    {
        public int id { get; set; }

        public int projectId { get; set; }
        public string projectName { get; set; }

        public int userId { get; set; }

        public string userName { get; set; }

        public string designation { get; set; }
    }
}
