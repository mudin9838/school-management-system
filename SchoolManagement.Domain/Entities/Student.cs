using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities;


public class Student : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }

    private Student() { } // For ORM
    public Student(string firstName, string lastName, string email)
    {
        SetName(firstName, lastName);
        SetEmail(email);
    }

    public void SetName(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(FirstName))
        {
            throw new ArgumentException("First name is required");
        }
        if (string.IsNullOrEmpty(LastName))
        {
            throw new ArgumentException("Last name is required");
        }
        FirstName = firstName; LastName = lastName;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Email is required");
        }
        Email = email;
    }
}
