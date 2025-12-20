using MediatR;
using SchoolManagement.Application.DTOs;

namespace SchoolManagement.Application.Features.Classes.Queries;

public record GetAllClassesQuery : IRequest<List<ClassDto>>;
