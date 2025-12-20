using MediatR;
using SchoolManagement.Application.DTOs;

namespace SchoolManagement.Application.Features.Students.Queries.GetStudents;

/// <summary>
/// This code snippet defines a C# record called GetAllStudentsQuery that inherits from the IRequest<List<StudentDto>> interface. The GetAllStudentsQuery record represents a request for retrieving a list of StudentDto objects. The IRequest interface is commonly used in the MediatR library for handling requests in a clean architecture pattern.
/// </summary>
public record GetAllStudentsQuery : IRequest<List<StudentDto>>;