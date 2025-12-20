using FluentValidation;

namespace SchoolManagement.Application.Features.Classes.Commands.CreateClass;

public class CreateClassCommandValidator : AbstractValidator<CreateClassCommand>
{
    public CreateClassCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.TeacherId).NotEmpty();
        RuleFor(x => x.MaxStudents).GreaterThan(0);
    }
}