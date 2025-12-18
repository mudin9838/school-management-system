using MediatR;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Students.Commands.CreateStudent;


public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Guid>
{

    private readonly IStudentRepository _studentRepository;

    public CreateStudentCommandHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }
    public async Task<Guid> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = new Student
        (
           request.FirstName,
           request.LastName,
           request.Email
        );
        await _studentRepository.AddAsync(student);
        return student.Id;
    }
}
