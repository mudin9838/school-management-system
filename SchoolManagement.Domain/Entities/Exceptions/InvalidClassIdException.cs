using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities.Exceptions;

public sealed class InvalidClassIdException : DomainException
{
    public InvalidClassIdException()
        : base("ClassId is required.")
    {
    }
}