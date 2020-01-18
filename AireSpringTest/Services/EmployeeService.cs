using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AireSpringTest.Core.Extensions;
using AireSpringTest.Core.Paging;
using AireSpringTest.Data;
using Microsoft.EntityFrameworkCore;

namespace AireSpringTest.Services
{
    public class EmployeeService: IEmployeeService
    {
        private readonly ApplicationDbContext _dbContext;
        public EmployeeService(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public Task<bool> EmployeeExistAsync(string employeeCode)
        {
            return _dbContext.Employees.AnyAsync(e => e.EmployeeCode == employeeCode);
        }

        public async Task<Employee> CreateEmployeeAsync(string employeeCode, string firstname, string lastname, string phone, string zipcode,
            DateTime hireDate)
        {

            var employee = new Employee
            {
                EmployeeCode = employeeCode,
                LastName = lastname,
                FirstName = firstname,
                ZipCode = zipcode,
                PhoneNumber = phone,
                DateCreated = DateTime.Now,
                HireDate = hireDate
            };
            _dbContext.Entry(employee).State = EntityState.Added;
            await _dbContext.SaveChangesAsync();
            return employee;
        }

        public Task<PaginatedList<Employee>> GetEmployeesAsync(int page = 1, int limit = 30, string searchFilter = default, string? sortColumn = default)
        {
            return  _dbContext.Employees.SearchEmployees(searchFilter).ToPaginatedListAsync(page, limit, sortColumn);
        }
    }
}
