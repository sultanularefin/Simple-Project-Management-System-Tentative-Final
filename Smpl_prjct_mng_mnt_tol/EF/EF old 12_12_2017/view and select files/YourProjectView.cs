using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Smpl_prjct_mng_mnt_tol.EF
{
    public class YourProjectView
    {
        public int id { get; set; }

        // [StringLength(300)]
        public string name { get; set; }

        //[StringLength(200)]
        public string codeName { get; set; }

        //[Column(TypeName = "text")]
        public string description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? startDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? endDate { get; set; }

        public int? duration { get; set; }

        public string status { get; set; }








        public string statusname { get; set; }

        //[StringLength(50)]
        public string shortName { get; set; }
    }
}