using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeeFunctionApp.Models;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeFunctionApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private static readonly string FilePath = Environment.GetEnvironmentVariable("EmployeeDataPath")
                              ?? Path.Combine(Directory.GetCurrentDirectory(), "Data", "employees.json");

        public async Task<Employee> GetEmployeeByIdAsync(string id)
        {
            var employees = await ReadEmployeesAsync();
            return employees.FirstOrDefault(e => e.Id == id);
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            var employees = await ReadEmployeesAsync();

            if (employees.Any(e => e.Id == employee.Id)) //check if employee already exists
            {
                Console.WriteLine("Employee already exists!");
                return;
            }

            employees.Add(employee);
            await WriteEmployeesAsync(employees);
            Console.WriteLine($"File will be created at: {FilePath}");

        }

        private async Task<List<Employee>> ReadEmployeesAsync()
        {
            if (!File.Exists(FilePath))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(FilePath));
                await File.WriteAllTextAsync(FilePath, "[]");
            }

            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<Employee>>(json) ?? new List<Employee>();
        }

        private async Task WriteEmployeesAsync(List<Employee> employees)
        {
            var json = JsonSerializer.Serialize(employees, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }
    }
}
