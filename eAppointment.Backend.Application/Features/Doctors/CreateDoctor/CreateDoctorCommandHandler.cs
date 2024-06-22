using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.CreateDoctor
{
    internal sealed class CreateDoctorCommandHandler(
        UserManager<User> userManager,
        RoleManager<Role> roleManager,
        IDoctorRepository doctorRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<CreateDoctorCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
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

                if (await roleManager.Roles.AnyAsync(r => r.Id == request.roleId))
                {
                    Doctor doctor = new Doctor()
                    {
                        UserId = user.Id,
                        DepartmentId = request.departmentId
                    };

                    await doctorRepository.AddAsync(doctor, cancellationToken);

                    await unitOfWork.SaveChangesAsync(cancellationToken);
                } 
                
                else
                {
                    return Result<string>.Failure("Role does not exist");
                }

            return "Doctor created successfully";
        }
    }
}
