using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArizonaSignCompany.Models
{
    public class SortDescriptor
    {
        public requestColumnName? quoteColumn { get; set; }
        //for sorting by descending
        public bool? quoteSort { get; set; }
        public requestColumnName? installationColumn { get; set; }
      
        public bool? installationSort { get; set; }

        public requestColumnName? rfiColumn { get; set; }

        public bool? rfiSort { get; set; }

        public requestColumnName? repairColumn { get; set; }

        public bool? repairSort { get; set; }

        public requestColumnName? serviceColumn { get; set; }

        public bool? serviceSort { get; set; }

        public requestColumnName? liftColumn { get; set; }

        public bool? liftSort { get; set; }

        public customer_informationColumnName? customer_informationColumn { get; set; }

        public bool? customer_informationSort { get; set; }
    }
}