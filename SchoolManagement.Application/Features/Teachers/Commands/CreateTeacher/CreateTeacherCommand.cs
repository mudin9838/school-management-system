using MediatR;

namespace SchoolManagement.Application.Features.Teachers.Commands.CreateTeacher;

public record CreateTeacherCommand(
    string FullName,
    string Email
) : IRequest<Guid>;
