using SchoolManagement.Domain.Common;
using SchoolManagement.Domain.Entities.Exceptions;

namespace SchoolManagement.Domain.Entities;

public class Enrollment : BaseEntity
{
    public Guid StudentId { get; private set; }
    public Guid ClassId { get; private set; }
    private Enrollment() { }
    public Enrollment(Guid studentId, Guid classId)
    {
        if (studentId == Guid.Empty)
            throw new InvalidStudentIdException();

        if (classId == Guid.Empty)
            throw new InvalidClassIdException();

        StudentId = studentId;
        ClassId = classId;
    }
}
