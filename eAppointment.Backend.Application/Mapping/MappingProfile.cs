using AutoMapper;
using eAppointment.Backend.Application.Features.Departments.CreateDepartment;
using eAppointment.Backend.Application.Features.Departments.UpdateDepartment;
using eAppointment.Backend.Application.Features.Doctors.UpdateDoctor;
using eAppointment.Backend.Application.Features.Patients.UpdatePatient;
using eAppointment.Backend.Application.Features.Users.CreateUser;
using eAppointment.Backend.Application.Features.Users.UpdateUser;
using eAppointment.Backend.Domain.Entities;

namespace eAppointment.Backend.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CreateDepartmentCommand, Department>();
            CreateMap<UpdateDepartmentCommand, Department>();

            CreateMap<CreateUserCommand, User>()
                .ForMember(dest => dest.Doctor, src => src.Ignore())
                .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<UpdateUserCommand, User>();

            CreateMap<UpdateDoctorCommand, Doctor>()
                .ForPath(dest => dest.User.FirstName, src => src.MapFrom(src => src.firstName))
                .ForPath(dest => dest.User.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.DepartmentId, src => src.MapFrom(src => src.departmentId));

            CreateMap<UpdatePatientCommand, Patient>()
                .ForPath(dest => dest.User.FirstName, src => src.MapFrom(src => src.firstName))
                .ForPath(dest => dest.User.LastName, src => src.MapFrom(src => src.lastName))
                .ForPath(dest => dest.County.Id, src => src.MapFrom(src => src.countyId))
                .ForPath(dest => dest.County.City.Id, src => src.MapFrom(src => src.cityId))
                .ForMember(dest => dest.FullAddress, src => src.MapFrom(src => src.fullAddress));
        }
    }
}
