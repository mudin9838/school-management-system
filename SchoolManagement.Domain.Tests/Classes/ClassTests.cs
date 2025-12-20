using SchoolManagement.Domain.Entities;
using SchoolManagement.Domain.Entities.Exceptions;

namespace SchoolManagement.Domain.Tests.Classes;

public class ClassTests
{
    [Fact]
    public void ClassConstructor_NegativeMaxStudents_ThrowsInvalidMaxStudentsException()
    {
        //Arrange
        var name = "Class 1";
        var teacherId = Guid.NewGuid();
        var maxStudents = -1;

        //Act & Assert
        Assert.Throws<InvalidMaxStudentsException>(() =>
        {
            var classRepository = new Class(name, teacherId, maxStudents);
        });
    }
}
