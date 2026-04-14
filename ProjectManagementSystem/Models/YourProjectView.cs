using System.ComponentModel.DataAnnotations;

namespace ProjectManagementSystem.Models
{
    public class YourProjectView
    {
        public int id { get; set; }

        public string name { get; set; }

        public string codeName { get; set; }

        public string description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? startDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? endDate { get; set; }

        public int? duration { get; set; }

        public string status { get; set; }

        public string statusname { get; set; }

        public string shortName { get; set; }
    }
}
