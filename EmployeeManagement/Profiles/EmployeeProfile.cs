using AutoMapper;
using EmployeeManagement.DTOs;
using EmployeeManagement.Models;

namespace EmployeeManagement.Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDto>();

            CreateMap<CreateEmployeeDto, Employee>();

            CreateMap<UpdateEmployeeDto, Employee>();
        }
    }
}
