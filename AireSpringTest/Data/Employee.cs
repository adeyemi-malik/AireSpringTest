using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AireSpringTest.Data
{
    public class Employee: BaseEntity
    {
        //employee unique code
        public string EmployeeCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string ZipCode { get; set; }

        public DateTime HireDate { get; set; }
    }
}
