using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities.Exceptions;

public sealed class InvalidNameException : DomainException
{
    public InvalidNameException(string fieldName)
        : base($"{fieldName} is required.")
    {
    }
}