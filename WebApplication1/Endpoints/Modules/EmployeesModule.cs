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

            GetEmployeesEndpoint.Map(group);
            GetEmployeeEndpoint.Map(group);
            GetEmployeeInfoEndpoint.Map(group);
            AddEmployeeEndpoint.Map(group);
            RemoveEmployeeEndpoint.Map(group);
        }
    }
}
