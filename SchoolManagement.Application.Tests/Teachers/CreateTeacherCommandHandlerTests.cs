using Moq;
using SchoolManagement.Application.Features.Teachers.Commands.CreateTeacher;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Tests.Teachers;

public class CreateTeacherCommandHandlerTests
{
    private readonly Mock<ITeacherRepository> _mockRepository;
    private readonly CreateTeacherCommandHandler _handler;
    public CreateTeacherCommandHandlerTests()
    {
        _mockRepository = new Mock<ITeacherRepository>();
        _handler = new CreateTeacherCommandHandler(_mockRepository.Object);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task Handle_CreateTeacherCommand_ReturnsTeacherId()
    {
        //  It tests the Handle method of the CreateTeacherCommandHandler class.

        //In the Arrange section, it sets up the necessary variables for the test, including a fullName and email for a teacher, and configures the mockRepository to return a completed task when the AddAsync method is called with any Teacher object.

        //In the Act section, it calls the Handle method of the _handler object with a new CreateTeacherCommand object containing the fullName and email variables.

        //In the Assert section, it verifies that the AddAsync method of the mockRepository was called exactly once with any Teacher object.This ensures that the Handle method correctly adds a new teacher to the repository.The async keyword indicates that the method is asynchronous and can use await to wait for the completion of asynchronous operations.


        // Arrange 
        var fullName = "John Doe";
        var email = "I7s0j@example.com";
        _mockRepository.Setup(repo => repo.AddAsync(It.IsAny<Teacher>())).Returns(Task.CompletedTask);
        var command = new CreateTeacherCommand(fullName, email);

        // Act
        await _handler.Handle(new CreateTeacherCommand(fullName, email), CancellationToken.None);

        // Assert

        //In the Assert section, it verifies that the AddAsync method of the mockRepository was called exactly once with any Teacher object.This ensures that the Handle method correctly adds a new teacher to the repository.The async keyword indicates that the method is asynchronous and can use await to wait for the completion of asynchronous operations.
        _mockRepository.Verify(repo => repo.AddAsync(It.IsAny<Teacher>()), Times.Once);
    }
}
