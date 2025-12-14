using SchoolManagement.Domain.Common;

namespace SchoolManagement.Domain.Entities;

public class Enrollment : BaseEntity
{
    public Guid StudentId { get; private set; }
    public Guid ClassId { get; private set; }
    private Enrollment() { }
    public Enrollment(Guid studentId, Guid classId)
    {
        if (studentId == Guid.Empty)
            throw new ArgumentException("StudentId is required");

        if (classId == Guid.Empty)
            throw new ArgumentException("ClassId is required");
        StudentId = studentId;
        ClassId = classId;
    }
}
