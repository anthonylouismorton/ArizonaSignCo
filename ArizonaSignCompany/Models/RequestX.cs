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
            else if (columnName == requestColumnName.type && sortDirection == true)
            {
                requests = requests.OrderByDescending(r => r.Type);
            }
            else if (columnName == requestColumnName.type)
            {
                requests = requests.OrderBy(r => r.Type);
            }

            return requests;
        }
        public static IQueryable<Customer_Information> sortByColumn(this IQueryable<Customer_Information> customer_Information, customer_informationColumnName? columnName, bool? sortDirection)
        {
            if (columnName == customer_informationColumnName.firstname && sortDirection == true)
            {
                customer_Information = customer_Information.OrderByDescending(r => r.FirstName);
            }
            else if (columnName == customer_informationColumnName.firstname)
            {
                customer_Information = customer_Information.OrderBy(r => r.FirstName);
            }
            else if (columnName == customer_informationColumnName.lastname && sortDirection == true)
            {
                customer_Information = customer_Information.OrderByDescending(r => r.LastName);
            }
            else if (columnName == customer_informationColumnName.lastname)
            {
                customer_Information = customer_Information.OrderBy(r => r.LastName);
            }
            else if (columnName == customer_informationColumnName.company && sortDirection == true)
            {
                customer_Information = customer_Information.OrderByDescending(r => r.Company);
            }
            else if (columnName == customer_informationColumnName.company)
            {
                customer_Information = customer_Information.OrderBy(r => r.Company);
            }
            return customer_Information;
        }
    }
}