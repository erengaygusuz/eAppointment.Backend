using AutoMapper;
using eAppointment.Backend.Application.Features.Doctors.CreateDoctor;
using eAppointment.Backend.Application.Features.Doctors.UpdateDoctor;
using eAppointment.Backend.Application.Features.Patients.CreatePatient;
using eAppointment.Backend.Application.Features.Patients.UpdatePatient;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;

namespace eAppointment.Backend.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<CreateDoctorCommand, Doctor>()
                .ForMember(member => member.Department, options => options.MapFrom(map => DepartmentEnum.FromValue(map.departmentValue)));

            CreateMap<UpdateDoctorCommand, Doctor>()
                .ForMember(member => member.Department, options => options.MapFrom(map => DepartmentEnum.FromValue(map.departmentValue)));

            CreateMap<CreatePatientCommand, Patient>();
            CreateMap<UpdatePatientCommand, Patient>();
        }
    }
}
