using Api.Endpoints.Employee;

namespace Api.Endpoints.Modules
{
    public static class EmployeesModule
    {
        public static void Map(WebApplication app)
        {
            var group = app.MapGroup("/api/employees")
                           .WithTags("Employees")
                           .RequireAuthorization()
                           .RequireRateLimiting("employeePolicy");

            GetEmployees.Map(group);
            GetEmployee.Map(group);
            CreateEmployee.Map(group);
            DeleteEmployee.Map(group);
            UpdateEmployee.Map(group);
            GetDeletedEmployee.Map(group);
            GetAllEmployees.Map(group);
        }
    }
}
