using EmployeeFunctionApp.Models;
using EmployeeFunctionApp.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeFunctionApp.Functions
{
    public class AddEmployeeFunction
    {
        private readonly IEmployeeService _employeeService;

        public AddEmployeeFunction(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Function("AddEmployee")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "employees")] HttpRequestData req,
            FunctionContext executionContext)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var employee = JsonSerializer.Deserialize<Employee>(requestBody);

            var response = req.CreateResponse();

            if (employee == null || string.IsNullOrEmpty(employee.Id))
            {
                response.StatusCode = System.Net.HttpStatusCode.BadRequest;
                await response.WriteStringAsync("Invalid employee data.");
                return response;
            }

            await _employeeService.AddEmployeeAsync(employee);

            response.StatusCode = System.Net.HttpStatusCode.OK;
            await response.WriteStringAsync($"Employee with ID {employee.Id} added successfully.");
            return response;
        }
    }
}
