using MediatR;
using SchoolManagement.Application.Exceptions;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Enrollments.Commands.EnrollStudent;

public class EnrollStudentCommandHandler : IRequestHandler<EnrollStudentCommand>
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public EnrollStudentCommandHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task Handle(EnrollStudentCommand request, CancellationToken cancellationToken)
    {
        var existsingEnrollment = await _enrollmentRepository.ExistsAsync(request.StudentId, request.ClassId);

        if (existsingEnrollment)
        {
            throw new BusinessRuleViolationException(
       "Student is already enrolled in this class");
        }
        var enrollment = new Enrollment(request.StudentId, request.ClassId);//creates a new Enrollment object with the provided StudentId and ClassId and adds it to the _enrollmentRepository
        await _enrollmentRepository.AddAsync(enrollment);
    }
}
