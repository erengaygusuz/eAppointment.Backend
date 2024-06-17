﻿using eAppointment.Backend.Application.Features.Doctors.GetAllDoctors;
using MediatR;
using TS.Result;

namespace eAppointment.Backend.Application.Features.Doctors.GetAllDoctor
{
    public sealed record GetAllDoctorsQuery() : IRequest<Result<List<GetAllDoctorsQueryResponse>>>;
}
