using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities.Exceptions;

public sealed class InvalidStudentEmailException : DomainException
{
    public InvalidStudentEmailException()
        : base("Student email is required.")
    {
    }
}