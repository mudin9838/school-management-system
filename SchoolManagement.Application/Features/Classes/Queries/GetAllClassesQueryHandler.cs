using MediatR;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;

namespace SchoolManagement.Application.Features.Classes.Queries;

public class GetAllClassesQueryHandler : IRequestHandler<GetAllClassesQuery, List<ClassDto>>
{
    private readonly IClassRepository _classRepository;

    public GetAllClassesQueryHandler(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public async Task<List<ClassDto>> Handle(GetAllClassesQuery request, CancellationToken cancellationToken)
    {
        var classes = await _classRepository.GetAllAsync();
        return classes.Select(c => new ClassDto
        {
            Id = c.Id,
            Name = c.Name,
            TeacherId = c.TeacherId,
            MaxStudents = c.MaxStudents
        }).ToList();
    }
}
