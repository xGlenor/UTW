using AutoMapper;
using BaseLibrary.DTOs;
using BaseLibrary.Models;

namespace ServerUTW;

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

        CreateMap<EnrollmentDTO, Enrolllment>();
        CreateMap<Enrolllment, EnrollmentDTO>()
            .ForMember(dest => dest.StudentId,
                opt => opt.MapFrom(
                    src => src.StudentId))
            .ForMember(dest => dest.LessonId,
                opt => opt.MapFrom(
                    src => src.LessonId));

    }
}