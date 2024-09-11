using AutoMapper;
using BaseLibrary.DTOs;
using BaseLibrary.Models;

namespace BaseLibrary;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<StudentDTO, Student>();
        CreateMap<Student, StudentDTO>();

        CreateMap<SessionDTO, Session>();
        CreateMap<Session, SessionDTO>();

        CreateMap<LessonDTO, Lesson>();
        CreateMap<Lesson, LessonDTO>();
        
        CreateMap<ApplicationUser, ApplicationUserDTO>();
        CreateMap<ApplicationUserDTO, ApplicationUser>();

        CreateMap<EnrollmentDTO, Enrolllment>();
        CreateMap<Enrolllment, EnrollmentDTO>()
            .ForMember(dest => dest.StudentId,
                opt => opt.MapFrom(
                    src => src.StudentId))
            .ForMember(dest => dest.LessonId,
                opt => opt.MapFrom(
                    src => src.LessonId));
        
        CreateMap<TeacherEnrollmentDto, TeacherEnrollment>();
        CreateMap<TeacherEnrollment, TeacherEnrollmentDto>()
            .ForMember(dest => dest.TeacherId,
                opt => opt.MapFrom(
                    src => src.TeacherId))
            .ForMember(dest => dest.LessonId,
                opt => opt.MapFrom(
                    src => src.LessonId));

    }
}