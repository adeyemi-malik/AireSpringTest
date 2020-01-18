using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AireSpringTest.Models
{
    public class EmployeeViewModel
    {
        [Required]
        public string EmployeeCode { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required,Phone]
        public string PhoneNumber { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime HireDate { get; set; }
    }

    public class SearchModel<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int Page { get; set; }

        public int Limit { get; set; }

        public int TotalItems { get; set; }

        public string Filter { get; set; }
    }
}
