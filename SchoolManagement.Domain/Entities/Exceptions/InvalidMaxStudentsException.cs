using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities.Exceptions;

public class InvalidMaxStudentsException : DomainException
{
    public InvalidMaxStudentsException(int maxStudents)
        : base($"MaxStudents must be greater than {maxStudents}.")
    {
    }
}
