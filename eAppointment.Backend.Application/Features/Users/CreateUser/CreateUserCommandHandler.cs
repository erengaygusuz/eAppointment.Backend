using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Users.CreateUser
{
    internal sealed class CreateUserCommandHandler(
        UserManager<User> userManager,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        IUserRoleRepository userRoleRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateUserCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await userManager.Users.AnyAsync(p => p.Email == request.email))
            {
                return Result<string>.Failure("Email alraedy exists");
            }

            if (await userManager.Users.AnyAsync(p => p.UserName == request.userName))
            {
                return Result<string>.Failure("User Name alraedy exists");
            }

            User user = mapper.Map<User>(request);

            IdentityResult result = await userManager.CreateAsync(user, request.password);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
            }

            if (request.role != null)
            {
                UserRole userRole = new()
                {
                    RoleId = request.role.Id,
                    UserId = user.Id
                };

                await userRoleRepository.AddAsync(userRole, cancellationToken);

                switch (request.role.Name)
                {
                    case "Doctor":

                        if (request.departmentId == Guid.Empty)
                        {
                            return Result<string>.Failure("Department id is not valid");
                        }

                        Doctor doctor = new()
                        {
                            UserId = user.Id,
                            DepartmentId = request.departmentId
                        };

                        await doctorRepository.AddAsync(doctor, cancellationToken);

                        break;

                    case "Patient":

                        if (string.IsNullOrEmpty(request.identityNumber))
                        {
                            return Result<string>.Failure("Identity number is not valid");
                        }

                        Patient patient = new()
                        {
                            UserId = user.Id,
                            IdentityNumber = request.identityNumber
                        };

                        await patientRepository.AddAsync(patient, cancellationToken);

                        break;
                }

                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return "User created successfully";
        }
    }
}
