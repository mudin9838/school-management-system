using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities.Exceptions;

/// <summary>
/// 
/// </summary>
public sealed class InvalidStudentEmailException : DomainException
{
    public InvalidStudentEmailException()
        : base("Student email is required.")
    {
    }
}