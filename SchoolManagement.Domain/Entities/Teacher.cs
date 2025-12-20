using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Entities.Exceptions;

namespace SchoolManagement.Domain.Entities;

public class Teacher : BaseEntity
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    private Teacher() { }
    public Teacher(string FullName, string email)
    {
        SetName(FullName);
        SetEmail(email);
    }

    public void SetName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new InvalidStudentNameException("first name");

        FullName = fullName;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidEmailException();

        Email = email;
    }
}
