using MediatR;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Classes.Commands.CreateClass;

public class CreateClassCommandHandler : IRequestHandler<CreateClassCommand, Guid>
{
    private readonly IClassRepository _repository;

    public CreateClassCommandHandler(IClassRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateClassCommand request, CancellationToken cancellationToken)
    {
        var classEntity = new Class(
             request.Name,
             request.TeacherId,
             request.MaxStudents);

        await _repository.AddAsync(classEntity);
        return classEntity.Id;
    }
}
