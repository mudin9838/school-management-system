namespace SchoolManagement.Application.DTOs;

public class ClassDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid TeacherId { get; set; }
    public int MaxStudents { get; set; }
}

