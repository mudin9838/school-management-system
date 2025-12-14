using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities;

public class Class : BaseEntity
{
    public string Name { get; private set; }

    private Class() { }

    public Class(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Class name is required");

        Name = name;
    }
}