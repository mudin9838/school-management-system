using FluentAssertions;
using Moq;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Features.Enrollments.Commands.EnrollStudent;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Tests.Enrollments;

public class EnrollStudentCommandHandlerTests
{
    private readonly Mock<IEnrollmentRepository> _repoMock;
    private readonly EnrollStudentCommandHandler _handler;

    public EnrollStudentCommandHandlerTests()
    {
        _repoMock = new Mock<IEnrollmentRepository>();
        _handler = new EnrollStudentCommandHandler(_repoMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldThrow_WhenStudentAlreadyEnrolled()
    {
        //Arrange
        var studentId = Guid.NewGuid();
        var classId = Guid.NewGuid();

        _repoMock.Setup(r => r.ExistsAsync(studentId, classId))
            .ReturnsAsync(true);
        var command = new EnrollStudentCommand(studentId, classId);

        //Act
        var act = async () => await _handler.Handle(command, default);

        //Assert
        await act.Should()
            .ThrowAsync<BusinessRuleViolationException>()
            .WithMessage("Student is already enrolled in this class");
    }


    [Fact]
    public async Task Handle_ShouldEnroll_WhenNotAlreadyEnrolled()
    {
        //Arrange
        var studentId = Guid.NewGuid();
        var classId = Guid.NewGuid();

        _repoMock.Setup(r => r.ExistsAsync(studentId, classId))
            .ReturnsAsync(false);
        var command = new EnrollStudentCommand(studentId, classId);

        //Act
        await _handler.Handle(command, default);

        //Assert
        _repoMock.Verify(
            r => r.AddAsync(It.IsAny<Enrollment>()),
            Times.Once);//  AddAsync method of the _repoMock object is called exactly once with any Enrollment object.
    }
}