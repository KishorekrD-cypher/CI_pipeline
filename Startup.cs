using EmployeeFunctionApp.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(EmployeeFunctionApp.Startup))]
namespace EmployeeFunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
        }
    }
}
