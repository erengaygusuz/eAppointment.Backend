using AutoMapper;
using eAppointment.Backend.Application.Features.Admins.CreateAdmin;
using eAppointment.Backend.Application.Features.Admins.GetUserById;
using eAppointment.Backend.Application.Features.Admins.UpdateAdminById;
using eAppointment.Backend.Application.Features.Appointments.CreateAppointment;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorIdAndByStatus;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientIdByStatus;
using eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById;
using eAppointment.Backend.Application.Features.Cities.GetAllCities;
using eAppointment.Backend.Application.Features.Counties.GetAllCountiesByCityId;
using eAppointment.Backend.Application.Features.Departments.GetAllDepartments;
using eAppointment.Backend.Application.Features.Doctors.CreateDoctor;
using eAppointment.Backend.Application.Features.Doctors.GetAllDoctorsByDepartmentId;
using eAppointment.Backend.Application.Features.Doctors.GetDoctorById;
using eAppointment.Backend.Application.Features.Doctors.UpdateDoctorById;
using eAppointment.Backend.Application.Features.Doctors.UpdateDoctorProfileById;
using eAppointment.Backend.Application.Features.Patients.CreatePatient;
using eAppointment.Backend.Application.Features.Patients.GetAllPatientsByDoctorId;
using eAppointment.Backend.Application.Features.Patients.GetPatientById;
using eAppointment.Backend.Application.Features.Patients.UpdatePatientById;
using eAppointment.Backend.Application.Features.Patients.UpdatePatientProfileById;
using eAppointment.Backend.Application.Features.Users.GetAllUsers;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Enums;

namespace eAppointment.Backend.Application.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Admin Mappings

            CreateMap<CreateAdminCommand, User>()
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.userName))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForMember(dest => dest.RoleId, src => src.MapFrom(src => src.roleId))
                .ForMember(dest => dest.Doctor, src => src.Ignore())
                .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<User, GetAdminByIdQueryResponse>()
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.UserName))
                .ForMember(dest => dest.RoleId, src => src.MapFrom(src => src.RoleId));

            CreateMap<UpdateAdminByIdCommand, User>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.id))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.userName))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForMember(dest => dest.RoleId, src => src.MapFrom(src => src.roleId))
                .ForMember(dest => dest.Doctor, src => src.Ignore())
                .ForMember(dest => dest.Patient, src => src.Ignore());

            #endregion

            #region Appointment Mappings

            CreateMap<CreateAppointmentCommand, Appointment>()
               .ForMember(dest => dest.StartDate, src => src.MapFrom(src => src.startDate))
               .ForMember(dest => dest.EndDate, src => src.MapFrom(src => src.endDate))
               .ForMember(dest => dest.PatientId, src => src.MapFrom(src => src.patientId))
               .ForMember(dest => dest.DoctorId, src => src.MapFrom(src => src.doctorId))
               .ForMember(dest => dest.Status, src => src.MapFrom(src => AppointmentStatus.NotCompleted))
               .ForMember(dest => dest.Doctor, src => src.Ignore())
               .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<Appointment, GetAllAppointmentsByDoctorIdAndByStatusQueryResponse>()
                .ForMember(dest => dest.id, src => src.MapFrom(src => src.Id))
                .ForMember(dest => dest.startDate, src => src.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.endDate, src => src.MapFrom(src => src.EndDate))
                .ForPath(dest => dest.title, src => src.MapFrom(src => src.Patient.User.FirstName + " " + src.Patient.User.LastName));

            CreateMap<Appointment, GetAllAppointmentsByPatientIdAndByStatusQueryResponse>()
                .ForMember(dest => dest.id, src => src.MapFrom(src => src.Id))
                .ForMember(dest => dest.startDate, src => src.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.endDate, src => src.MapFrom(src => src.EndDate))
                .ForPath(dest => dest.title, src => src.MapFrom(src => src.Patient.User.FirstName + " " + src.Patient.User.LastName));

            CreateMap<UpdateAppointmentByIdCommand, Appointment>()
               .ForMember(dest => dest.StartDate, src => src.MapFrom(src => src.startDate))
               .ForMember(dest => dest.EndDate, src => src.MapFrom(src => src.endDate))
               .ForMember(dest => dest.Status, src => src.MapFrom(src => AppointmentStatus.FromValue(src.status)));

            #endregion

            #region City Mappings

            CreateMap<City, GetAllCitiesQueryResponse>()
                .ForPath(dest => dest.id, src => src.MapFrom(src => src.Id))
                .ForPath(dest => dest.name, src => src.MapFrom(src => src.Name));

            #endregion

            #region County Mappings

            CreateMap<County, GetAllCountiesByCityIdQueryResponse>()
                .ForPath(dest => dest.id, src => src.MapFrom(src => src.Id))
                .ForPath(dest => dest.name, src => src.MapFrom(src => src.Name));

            #endregion

            #region Department Mappings

            CreateMap<Department, GetAllDepartmentsQueryResponse>()
                .ForPath(dest => dest.id, src => src.MapFrom(src => src.Id))
                .ForPath(dest => dest.name, src => src.MapFrom(src => src.Name));

            #endregion

            #region Doctor Mappings

            CreateMap<CreateDoctorCommand, User>()
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.userName))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForMember(dest => dest.RoleId, src => src.MapFrom(src => src.roleId))
                .ForPath(dest => dest.Doctor!.DepartmentId, src => src.MapFrom(src => src.departmentId))
                .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<Doctor, GetDoctorByIdQueryResponse>()
                .ForPath(dest => dest.firstName, src => src.MapFrom(src => src.User.FirstName))
                .ForPath(dest => dest.lastName, src => src.MapFrom(src => src.User.LastName))
                .ForPath(dest => dest.phoneNumber, src => src.MapFrom(src => src.User.PhoneNumber))
                .ForPath(dest => dest.email, src => src.MapFrom(src => src.User.Email))
                .ForPath(dest => dest.userName, src => src.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.departmentId, src => src.MapFrom(src => src.DepartmentId));

            CreateMap<Doctor, GetAllDoctorsByDepartmentIdQueryResponse>()
                .ForPath(dest => dest.firstName, src => src.MapFrom(src => src.User.FirstName))
                .ForPath(dest => dest.lastName, src => src.MapFrom(src => src.User.LastName))
                .ForPath(dest => dest.departmentName, src => src.MapFrom(src => src.Department.Name));

            CreateMap<UpdateDoctorByIdCommand, User>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.id))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.userName))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForPath(dest => dest.Doctor!.DepartmentId, src => src.MapFrom(src => src.departmentId))
                .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<UpdateDoctorProfileByIdCommand, User>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.id))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForMember(dest => dest.Patient, src => src.Ignore());

            #endregion

            #region Patient Mappings

            CreateMap<CreatePatientCommand, User>()
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.userName))
                .ForMember(dest => dest.Patient!.FullAddress, src => src.MapFrom(src => src.fullAddress))
                .ForMember(dest => dest.Patient!.IdentityNumber, src => src.MapFrom(src => src.identityNumber))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForMember(dest => dest.RoleId, src => src.MapFrom(src => src.roleId))
                .ForPath(dest => dest.Patient!.CountyId, src => src.MapFrom(src => src.countyId))
                .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<Patient, GetPatientByIdQueryResponse>()
                .ForPath(dest => dest.firstName, src => src.MapFrom(src => src.User.FirstName))
                .ForPath(dest => dest.lastName, src => src.MapFrom(src => src.User.LastName))
                .ForPath(dest => dest.phoneNumber, src => src.MapFrom(src => src.User.PhoneNumber))
                .ForPath(dest => dest.email, src => src.MapFrom(src => src.User.Email))
                .ForPath(dest => dest.userName, src => src.MapFrom(src => src.User.UserName))
                .ForPath(dest => dest.cityId, src => src.MapFrom(src => src.User.Patient!.County.CityId))
                .ForPath(dest => dest.countyId, src => src.MapFrom(src => src.User.Patient!.CountyId));

            CreateMap<Patient, GetAllPatientsByDoctorIdQueryResponse>()
                .ForPath(dest => dest.firstName, src => src.MapFrom(src => src.User.FirstName))
                .ForPath(dest => dest.lastName, src => src.MapFrom(src => src.User.LastName))
                .ForPath(dest => dest.identityNumber, src => src.MapFrom(src => src.IdentityNumber))
                .ForMember(dest => dest.patientAppointments, src => src.MapFrom(src => src.Appointments!.Select(x => new PatientAppointment()
                {
                    Id = x.Id,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Status = x.Status
                })));

            CreateMap<UpdatePatientByIdCommand, User>()
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.userName))
                .ForMember(dest => dest.Patient!.FullAddress, src => src.MapFrom(src => src.fullAddress))
                .ForMember(dest => dest.Patient!.IdentityNumber, src => src.MapFrom(src => src.identityNumber))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForPath(dest => dest.Patient!.CountyId, src => src.MapFrom(src => src.countyId))
                .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<UpdatePatientProfileByIdCommand, User>()
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.Patient!.FullAddress, src => src.MapFrom(src => src.fullAddress))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForPath(dest => dest.Patient!.CountyId, src => src.MapFrom(src => src.countyId))
                .ForMember(dest => dest.Patient, src => src.Ignore());

            #endregion

            #region User Mappings

            CreateMap<User, GetAllUsersQueryResponse>()
                .ForMember(dest => dest.FullName, src => src.MapFrom(src => src.FirstName + " " + src.LastName))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.UserName))
                .ForPath(dest => dest.RoleName, src => src.MapFrom(src => src.Role!.Name));

            #endregion
        }
    }
}
