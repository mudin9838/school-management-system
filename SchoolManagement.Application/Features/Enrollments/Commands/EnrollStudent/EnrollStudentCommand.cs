using MediatR;

namespace SchoolManagement.Application.Features.Enrollments.Commands.EnrollStudent;
/// <summary>
/// By default, properties in a record are immutable (read-only), which is a key principle for commands and events in CQRS. It ensures that once a command is created with specific data (the StudentId and ClassId), that data cannot be changed accidentally, making the system more predictable and thread-safe.
/// </summary>
/// <param name="StudentId"></param>
/// <param name="classId"></param>
public record EnrollStudentCommand(Guid StudentId, Guid ClassId) : IRequest; //IRequest interface from the MediatR library, indicating that it can be used as a request object in a MediatR pipeline.
