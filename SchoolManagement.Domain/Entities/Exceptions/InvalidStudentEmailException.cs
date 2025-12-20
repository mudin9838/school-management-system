using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities.Exceptions;

/// <summary>
/// 
/// </summary>
public sealed class InvalidEmailException : DomainException
{
    public InvalidEmailException()
        : base("Email is required.")
    {
    }
}