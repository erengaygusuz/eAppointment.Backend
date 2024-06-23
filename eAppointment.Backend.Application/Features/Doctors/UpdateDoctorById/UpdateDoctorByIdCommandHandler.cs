using AutoMapper;
using eAppointment.Backend.Domain.Entities;
using eAppointment.Backend.Domain.Repositories;
using GenericRepository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.UpdateDoctorById
{
    internal sealed class UpdateDoctorByIdCommandHandler (
        IMapper mapper,
        IDoctorRepository doctorRepository,
        IUnitOfWork unitOfWork,
        UserManager<User> userManager): IRequestHandler<UpdateDoctorByIdCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(UpdateDoctorByIdCommand request, CancellationToken cancellationToken)
        {
            User? user = await userManager.FindByIdAsync(request.id.ToString());

            if (user is null)
            {
                return Result<string>.Failure("Doctor not found");
            }

            if (user.UserName != request.userName)
            {

                if (await userManager.Users.AnyAsync(p => p.UserName == request.userName))
                {
                    return Result<string>.Failure("User Name alraedy exists");
                }
            }

            mapper.Map(request, user);

            IdentityResult result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return Result<string>.Failure(result.Errors.Select(s => s.Description).ToList());
            }

            Doctor doctor = await doctorRepository.GetByExpressionAsync(x => x.UserId == request.id);

            doctor.DepartmentId = request.departmentId;

            doctorRepository.Update(doctor);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return "Doctor updated successfully";
        }
    }
}
