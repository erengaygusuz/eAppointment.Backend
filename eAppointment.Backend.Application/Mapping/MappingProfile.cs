using AutoMapper;
using eAppointment.Backend.Application.Features.Admins.CreateAdmin;
using eAppointment.Backend.Application.Features.Admins.UpdateAdminById;
using eAppointment.Backend.Application.Features.Departments.CreateDepartment;
using eAppointment.Backend.Application.Features.Departments.UpdateDepartment;
using eAppointment.Backend.Application.Features.Doctors.UpdateDoctor;
using eAppointment.Backend.Application.Features.Patients.UpdatePatient;
using eAppointment.Backend.Domain.Entities;

namespace eAppointment.Backend.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CreateDepartmentCommand, Department>();
            CreateMap<UpdateDepartmentCommand, Department>();

            CreateMap<CreateAdminCommand, User>()
                .ForMember(dest => dest.Doctor, src => src.Ignore())
                .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<UpdateAdminByIdCommand, User>();

            CreateMap<UpdateDoctorCommand, Doctor>()
                .ForPath(dest => dest.User.FirstName, src => src.MapFrom(src => src.firstName))
                .ForPath(dest => dest.User.LastName, src => src.MapFrom(src => src.lastName))
                .ForPath(dest => dest.User.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForPath(dest => dest.User.UserName, src => src.MapFrom(src => src.userName))
                .ForPath(dest => dest.User.Email, src => src.MapFrom(src => src.email))
                .ForPath(dest => dest.User.RoleId, src => src.MapFrom(src => src.roleId));

            CreateMap<UpdatePatientCommand, Patient>()
                .ForPath(dest => dest.User.FirstName, src => src.MapFrom(src => src.firstName))
                .ForPath(dest => dest.User.LastName, src => src.MapFrom(src => src.lastName))
                .ForPath(dest => dest.User.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForPath(dest => dest.User.UserName, src => src.MapFrom(src => src.userName))
                .ForPath(dest => dest.County.Id, src => src.MapFrom(src => src.countyId))
                .ForPath(dest => dest.County.City.Id, src => src.MapFrom(src => src.cityId))
                .ForMember(dest => dest.FullAddress, src => src.MapFrom(src => src.fullAddress));
        }
    }
}
