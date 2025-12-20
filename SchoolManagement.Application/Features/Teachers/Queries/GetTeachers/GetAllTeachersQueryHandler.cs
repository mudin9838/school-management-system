using MediatR;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Application.Features.Teachers.Queries.GetTeachers;


public class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersQuery, List<TeacherDto>>
{
    private readonly ITeacherRepository _repository;

    public GetAllTeachersQueryHandler(ITeacherRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<TeacherDto>> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
    {
        var teachers = await _repository.GetAllAsync();
        return teachers.Select(s => new TeacherDto
        {
            Id = s.Id,
            FullName = s.FullName,
            Email = s.Email
        }).ToList();
    }
}
