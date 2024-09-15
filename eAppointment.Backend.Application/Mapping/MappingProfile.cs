using AutoMapper;
using eAppointment.Backend.Application.Features.Admins.CreateAdmin;
using eAppointment.Backend.Application.Features.Admins.GetAdminById;
using eAppointment.Backend.Application.Features.Admins.GetAdminProfileById;
using eAppointment.Backend.Application.Features.Admins.UpdateAdminById;
using eAppointment.Backend.Application.Features.Admins.UpdateAdminProfileById;
using eAppointment.Backend.Application.Features.Appointments.CreateAppointment;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorId;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByDoctorIdAndByStatus;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientId;
using eAppointment.Backend.Application.Features.Appointments.GetAllAppointmentsByPatientIdByStatus;
using eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentById;
using eAppointment.Backend.Application.Features.Appointments.UpdateAppointmentStatusById;
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
                .ForMember(dest => dest.Doctor, src => src.Ignore())
                .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<User, GetAdminByIdQueryResponse>()
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.UserName));

            CreateMap<User, GetAdminProfileByIdQueryResponse>()
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.Email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.UserName))
                .ForMember(dest => dest.ProfilePhotoPath, src => src.MapFrom(src => src.ProfilePhotoPath));

            CreateMap<UpdateAdminByIdCommand, User>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.id))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.userName))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForMember(dest => dest.Doctor, src => src.Ignore())
                .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<UpdateAdminProfileByIdCommand, User>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.id))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.ProfilePhotoPath, src => src.MapFrom(src => src.profilePhoto.FileName))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForMember(dest => dest.Doctor, src => src.Ignore())
                .ForMember(dest => dest.Patient, src => src.Ignore());

            #endregion

            #region Appointment Mappings

            CreateMap<CreateAppointmentCommand, Appointment>()
               .ForMember(dest => dest.StartDate, src => src.MapFrom(src => DateTime.ParseExact(src.startDate, "dd.MM.yyyy HH:mm", null)))
               .ForMember(dest => dest.EndDate, src => src.MapFrom(src => DateTime.ParseExact(src.endDate, "dd.MM.yyyy HH:mm", null)))
               .ForMember(dest => dest.PatientId, src => src.MapFrom(src => src.patientId))
               .ForMember(dest => dest.DoctorId, src => src.MapFrom(src => src.doctorId))
               .ForMember(dest => dest.Status, src => src.MapFrom(src => AppointmentStatus.NotCompleted))
               .ForMember(dest => dest.Doctor, src => src.Ignore())
               .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<Appointment, GetAllAppointmentsByDoctorIdQueryResponse>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(src => src.StartDate.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(src => src.EndDate.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(dest => dest.Status, src => src.MapFrom(src => AppointmentStatus.FromValue(src.Status).Name))
                .ForPath(dest => dest.FullName, src => src.MapFrom(src => src.Patient.User!.FirstName + " " + src.Patient.User!.LastName));

            CreateMap<Appointment, GetAllAppointmentsByDoctorIdAndByStatusQueryResponse>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(src => src.StartDate.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(src => src.EndDate.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(dest => dest.Status, src => src.MapFrom(src => AppointmentStatus.FromValue(src.Status).Name))
                .ForPath(dest => dest.FullName, src => src.MapFrom(src => src.Patient.User!.FirstName + " " + src.Patient.User!.LastName));

            CreateMap<Appointment, GetAllAppointmentsByPatientIdAndByStatusQueryResponse>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(src => src.EndDate))
                .ForPath(dest => dest.Title, src => src.MapFrom(src => src.Patient.User!.FirstName + " " + src.Patient.User!.LastName));

            CreateMap<Appointment, GetAllAppointmentsByPatientIdQueryResponse>()
                .ForPath(dest => dest.DepartmentName, src => src.MapFrom(src => src.Doctor.Department!.Name))
                .ForPath(dest => dest.DoctorName, src => src.MapFrom(src => src.Doctor.User!.FirstName + " " + src.Doctor.User!.LastName))
                .ForMember(dest => dest.StartDate, src => src.MapFrom(src => src.StartDate.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(dest => dest.EndDate, src => src.MapFrom(src => src.EndDate.ToString("dd.MM.yyyy HH:mm")))
                .ForMember(dest => dest.Status, src => src.MapFrom(src => AppointmentStatus.FromValue(src.Status).Name));

            CreateMap<UpdateAppointmentByIdCommand, Appointment>()
               .ForMember(dest => dest.StartDate, src => src.MapFrom(src => DateTime.ParseExact(src.startDate, "dd.MM.yyyy HH:mm", null)))
               .ForMember(dest => dest.EndDate, src => src.MapFrom(src => DateTime.ParseExact(src.endDate, "dd.MM.yyyy HH:mm", null)))
               .ForMember(dest => dest.Status, src => src.MapFrom(src => AppointmentStatus.FromValue(src.status)));

            CreateMap<UpdateAppointmentStatusByIdCommand, Appointment>()
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
                .ForMember(dest => dest.Patient, src => src.Ignore())
                .ForMember(dest => dest.Doctor, src => src.Ignore());

            CreateMap<Doctor, GetDoctorByIdQueryResponse>()
                .ForPath(dest => dest.FirstName, src => src.MapFrom(src => src.User!.FirstName))
                .ForPath(dest => dest.LastName, src => src.MapFrom(src => src.User!.LastName))
                .ForPath(dest => dest.PhoneNumber, src => src.MapFrom(src => src.User!.PhoneNumber))
                .ForPath(dest => dest.Email, src => src.MapFrom(src => src.User!.Email))
                .ForPath(dest => dest.UserName, src => src.MapFrom(src => src.User!.UserName))
                .ForMember(dest => dest.DepartmentId, src => src.MapFrom(src => src.DepartmentId));

            CreateMap<Doctor, GetAllDoctorsByDepartmentIdQueryResponse>()
                .ForPath(dest => dest.FullName, src => src.MapFrom(src => src.User!.FirstName + " " + src.User!.LastName));

            CreateMap<UpdateDoctorByIdCommand, User>()
                .ForMember(dest => dest.Id, src => src.MapFrom(src => src.id))
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForMember(dest => dest.Email, src => src.MapFrom(src => src.email))
                .ForMember(dest => dest.UserName, src => src.MapFrom(src => src.userName))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
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
                .ForPath(dest => dest.Patient!.FullAddress, src => src.MapFrom(src => src.fullAddress))
                .ForPath(dest => dest.Patient!.IdentityNumber, src => src.MapFrom(src => src.identityNumber))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForPath(dest => dest.Patient!.CountyId, src => src.MapFrom(src => src.countyId))
                .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<Patient, GetPatientByIdQueryResponse>()
                .ForPath(dest => dest.FirstName, src => src.MapFrom(src => src.User!.FirstName))
                .ForPath(dest => dest.LastName, src => src.MapFrom(src => src.User!.LastName))
                .ForPath(dest => dest.PhoneNumber, src => src.MapFrom(src => src.User!.PhoneNumber))
                .ForPath(dest => dest.Email, src => src.MapFrom(src => src.User!.Email))
                .ForPath(dest => dest.UserName, src => src.MapFrom(src => src.User!.UserName))
                .ForPath(dest => dest.CityId, src => src.MapFrom(src => src.User!.Patient!.County.CityId))
                .ForPath(dest => dest.CountyId, src => src.MapFrom(src => src.User!.Patient!.CountyId));

            CreateMap<Patient, GetAllPatientsByDoctorIdQueryResponse>()
                .ForPath(dest => dest.firstName, src => src.MapFrom(src => src.User!.FirstName))
                .ForPath(dest => dest.lastName, src => src.MapFrom(src => src.User!.LastName))
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
                .ForPath(dest => dest.Patient!.FullAddress, src => src.MapFrom(src => src.fullAddress))
                .ForPath(dest => dest.Patient!.IdentityNumber, src => src.MapFrom(src => src.identityNumber))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForPath(dest => dest.Patient!.CountyId, src => src.MapFrom(src => src.countyId))
                .ForMember(dest => dest.Patient, src => src.Ignore());

            CreateMap<UpdatePatientProfileByIdCommand, User>()
                .ForMember(dest => dest.FirstName, src => src.MapFrom(src => src.firstName))
                .ForMember(dest => dest.LastName, src => src.MapFrom(src => src.lastName))
                .ForMember(dest => dest.PhoneNumber, src => src.MapFrom(src => src.phoneNumber))
                .ForPath(dest => dest.Patient!.FullAddress, src => src.MapFrom(src => src.fullAddress))
                .ForMember(dest => dest.PasswordHash, src => src.Ignore())
                .ForPath(dest => dest.Patient!.CountyId, src => src.MapFrom(src => src.countyId))
                .ForMember(dest => dest.Patient, src => src.Ignore());

            #endregion

            #region User Mappings

            CreateMap<Tuple<User, List<string>>, GetAllUsersQueryResponse>()
                .ForPath(dest => dest.Id, src => src.MapFrom(src => src.Item1.Id))
                .ForPath(dest => dest.FullName, src => src.MapFrom(src => src.Item1.FirstName + " " + src.Item1.LastName))
                .ForPath(dest => dest.Email, src => src.MapFrom(src => src.Item1.Email))
                .ForPath(dest => dest.UserName, src => src.MapFrom(src => src.Item1.UserName))
                .ForPath(dest => dest.RoleNames, src => src.MapFrom(src => src.Item2));

            #endregion
        }
    }
}
