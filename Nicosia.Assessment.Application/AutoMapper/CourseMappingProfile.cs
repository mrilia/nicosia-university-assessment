using AutoMapper;
using Nicosia.Assessment.Application.Handlers.Course.Commands.AddNewCourse;
using Nicosia.Assessment.Application.Handlers.Course.Commands.UpdateCourse;
using Nicosia.Assessment.Application.Handlers.Course.Dto;
using Nicosia.Assessment.Domain.Models.Course;

namespace Nicosia.Assessment.Application.AutoMapper
{
    public class CourseMappingProfile : Profile
    {
        public CourseMappingProfile()
        {
            CreateMap<Course, CourseDto>();
            CreateMap<AddNewCourseCommand, Course>();
            CreateMap<UpdateCourseCommand, Course>();
        }
    }
}
