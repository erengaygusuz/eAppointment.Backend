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

            var addedUser = await userManager.FindByEmailAsync(user.Email!);

            var roleResult = await userManager.AddToRoleAsync(addedUser!, "Doctor");

            if (!roleResult.Succeeded)
            {
                return Result<string>.Failure("Role could not add to user");
            }

            Doctor doctor = new Doctor()
            {
                UserId = addedUser!.Id,
                DepartmentId = request.departmentId
            };

            await doctorRepository.AddAsync(doctor, cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Doctor created successfully";
        }
    }
}
