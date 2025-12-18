using MediatR;

namespace SchoolManagement.Application.Features.Students.Commands.CreateStudent;

public class CreateStudentCommand : IRequest<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
