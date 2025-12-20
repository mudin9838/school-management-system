using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Entities.Exceptions;

namespace SchoolManagement.Domain.Entities;


public class Student : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }

    private Student() { }

    public Student(string firstName, string lastName, string email)
    {
        SetName(firstName, lastName);
        SetEmail(email);
    }

    public void SetName(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new InvalidStudentNameException("first name");

        if (string.IsNullOrWhiteSpace(lastName))
            throw new InvalidStudentNameException("last name");

        FirstName = firstName;
        LastName = lastName;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new InvalidEmailException();

        Email = email;
    }
}
