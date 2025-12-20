using MediatR;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Application.Features.Students.Queries.GetStudents;

/// <summary>
/// This code snippet defines a class named GetAllStudentsQueryHandler that implements the IRequestHandler<GetAllStudentsQuery, List<StudentDto>> interface. This means that the class is responsible for handling requests of type GetAllStudentsQuery and returning a response of type List<StudentDto>. The Handle method is the entry point for handling the request. In this case, it throws a NotImplementedException, indicating that the implementation is not yet provided.
/// </summary>
public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, List<StudentDto>>
{
    private readonly IStudentRepository _studentRepository;

    public GetAllStudentsQueryHandler(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<List<StudentDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
    {
        var students = await _studentRepository.GetAllAsync();
        return students.Select(s => new StudentDto
        {
            Id = s.Id,
            FullName = $"{s.FirstName} {s.LastName}",
            Email = s.Email
        }).ToList();
    }
}
