using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ArizonaSignCompany.Models
{
    public static class RequestX
    {
        public static IQueryable<Request> sortByColumn(this IQueryable<Request> requests, requestColumnName? columnName, bool? sortDirection)
        {
            if (columnName == requestColumnName.firstname && sortDirection == true)
            {
                requests = requests.OrderByDescending(r => r.first_name);
            }
            else if (columnName == requestColumnName.firstname)
            {
                requests = requests.OrderBy(r => r.first_name);
            }
            else if (columnName == requestColumnName.lastname && sortDirection == true)
            {
                requests = requests.OrderByDescending(r => r.last_name);
            }
            else if (columnName == requestColumnName.lastname)
            {
                requests = requests.OrderBy(r => r.last_name);
            }
            else if (columnName == requestColumnName.company && sortDirection == true)
            {
                requests = requests.OrderByDescending(r => r.company);
            }
            else if (columnName == requestColumnName.company)
            {
                requests = requests.OrderBy(r => r.company);
            }
            else if (columnName == requestColumnName.contact && sortDirection == true)
            {
                requests = requests.OrderByDescending(r => r.contact);
            }
            else if (columnName == requestColumnName.contact)
            {
                requests = requests.OrderBy(r => r.contact);
            }
            return requests;
        }
    }
}