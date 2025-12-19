using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities.Exceptions;

public sealed class InvalidStudentIdException : DomainException
{
    public InvalidStudentIdException()
        : base("StudentId is required.")
    {
    }
}

