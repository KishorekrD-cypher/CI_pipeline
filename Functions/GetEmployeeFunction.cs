using EmployeeFunctionApp.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using System.Threading.Tasks;

namespace EmployeeFunctionApp.Functions
{
    public class GetEmployeeFunction
    {
        private readonly IEmployeeService _employeeService;

        public GetEmployeeFunction(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Function("GetEmployee")]
        public async Task<HttpResponseData> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "employees/{id}")] HttpRequestData req,
            string id,
            FunctionContext executionContext)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);

            var response = req.CreateResponse();

            if (employee == null)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                await response.WriteStringAsync($"Employee with ID {id} not found.");
                return response;
            }

            await response.WriteStringAsync(System.Text.Json.JsonSerializer.Serialize(employee));
            return response;
        }
    }
}
