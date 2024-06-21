﻿namespace eAppointment.Backend.Application.Features.Users.GetAllUsers
{
    public sealed record GetAllUsersQueryResponse()
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? RoleName { get; set; }
    };
}
