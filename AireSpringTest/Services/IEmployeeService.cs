using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AireSpringTest.Core.Paging;
using AireSpringTest.Data;

namespace AireSpringTest.Services
{
    public interface IEmployeeService
    {
        Task<bool> EmployeeExistAsync(string employeeCode);
        Task<Employee> CreateEmployeeAsync(string employeeCode,string firstname, string lastname, string phone, string zipcode,
            DateTime hireDate);
        Task<PaginatedList<Employee>> GetEmployeesAsync(int page = 1, int limit = 30, string searchFilter = default,
            string? sortColumn = default);
    }
}
