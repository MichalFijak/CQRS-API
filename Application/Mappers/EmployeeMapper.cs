
using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                Email = employee.Email,
                Salary = employee.Salary,
                Username = employee.Username
            };
        }

    }
}
