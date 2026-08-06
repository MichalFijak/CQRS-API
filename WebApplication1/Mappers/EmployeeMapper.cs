using Api.Response;
using Application.Dtos;

namespace Api.Mappers
{
    public static class EmployeeMapper
    {

        public static EmployeeResponse MapToResponse(this EmployeeDto dto)
        {
            return new EmployeeResponse(EmployeeId: dto.EmployeeId, Username: dto.Username, Salary: dto.Salary, Email: dto.Email);
        }

    }
}
