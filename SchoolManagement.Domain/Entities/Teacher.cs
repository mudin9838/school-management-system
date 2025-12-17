using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities;

public class Teacher : BaseEntity
{
    public string FullName { get; private set; }
    private Teacher() { }
    public Teacher(string fullName)
    {
        SetName(fullName);
    }
    public void SetName(string fullName)
    {
        if (string.IsNullOrEmpty(fullName))
        {
            throw new ArgumentException("Teacher name is required");
        }
        FullName = fullName;
    }
}
