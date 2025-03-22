using EmployeeFunctionApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeFunctionApp.Services
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeByIdAsync(string id);
        Task AddEmployeeAsync(Employee employee);
    }
}
