using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArizonaSignCompany.Models
{
    public partial class Customer_Information
    {
        public string fullCustomerDetails => FirstName + " " + LastName +(string.IsNullOrWhiteSpace(Company) ? "" : (" (" + Company + ")"));
    }
}