using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities.Exceptions;

public sealed class InvalidStudentNameException : DomainException
{
    public InvalidStudentNameException(string fieldName)
        : base($"{fieldName} is required.")
    {
    }
}