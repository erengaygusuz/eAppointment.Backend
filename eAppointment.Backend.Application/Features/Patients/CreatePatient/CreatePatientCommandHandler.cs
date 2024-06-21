using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Patients.CreatePatient
{
    internal sealed class CreatePatientCommandHandler(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IPatientRepository patientRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreatePatientCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
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

            if (request.roleId != Guid.Empty)
            {
                if (await roleManager.Roles.AnyAsync(r => r.Id == request.roleId))
                {
                    Patient patient = new Patient()
                    {
                        UserId = user.Id,
                        CountyId = request.countyId,
                        FullAddress = request.fullAddress,
                        IdentityNumber = request.identityNumber
                    };

                    await patientRepository.AddAsync(patient, cancellationToken);

                    await unitOfWork.SaveChangesAsync(cancellationToken);
                } 
                
                else
                {
                    return Result<string>.Failure("Role does not exist");
                }
            }

            return "Patient created successfully";
        }
    }
}
