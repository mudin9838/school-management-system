using MediatR;
using SchoolManagement.Application.DTOs;

namespace SchoolManagement.Application.Features.Teachers.Queries.GetTeachers;

public record GetAllTeachersQuery : IRequest<List<TeacherDto>>;
