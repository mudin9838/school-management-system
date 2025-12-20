using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Entities.Exceptions;

namespace SchoolManagement.Domain.Entities;

public class Class : BaseEntity
{
    public string Name { get; private set; }
    public Guid TeacherId { get; private set; }
    public int MaxStudents { get; private set; }
    private Class() { }

    public Class(string name, Guid teacherId, int maxStudents)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new InvalidNameException("Class name is required");

        if (maxStudents <= 0)
            throw new InvalidMaxStudentsException(maxStudents);
        Name = name;
        TeacherId = teacherId;
        MaxStudents = maxStudents;
    }
}