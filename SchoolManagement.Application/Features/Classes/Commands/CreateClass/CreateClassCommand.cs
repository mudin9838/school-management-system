using MediatR;

namespace SchoolManagement.Application.Features.Classes.Commands.CreateClass;

public record CreateClassCommand(
    string Name,
    Guid TeacherId,
    int MaxStudents
) : IRequest<Guid>;