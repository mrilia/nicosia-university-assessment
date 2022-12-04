using Nicosia.Assessment.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Nicosia.Assessment.Domain.Models.Course;
using Nicosia.Assessment.Domain.Models.Period;
using Nicosia.Assessment.Domain.Models.Section;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Persistence.Seed
{
    internal class DbContextSeed
    {
        private static readonly List<Student> SampleStudents = new List<Student>()
        {
            new Student
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Ross",
                Lastname = "Geller",
                Email = "ross.geller@mail.com",
                PhoneNumber = "+989121111234",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Monica",
                Lastname = "Geller",
                Email = "monica.geller@mail.com",
                PhoneNumber = "+989122221234",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Rachel",
                Lastname = "Green",
                Email = "Rachel.Green@mail.com",
                PhoneNumber = "+989123331234",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Phoebe",
                Lastname = "Buffay",
                Email = "Phoebe.Buffay@mail.com",
                PhoneNumber = "+9891244441234",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Joey",
                Lastname = "Tribbiani",
                Email = "Joey.Tribbiani@mail.com",
                PhoneNumber = "+989125551234",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Chandler",
                Lastname = "Bing",
                Email = "Chandler.Bing@mail.com",
                PhoneNumber = "+989126661234",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Janice",
                Lastname = "Hosenstein",
                Email = "Janice.Hosenstein@mail.com",
                PhoneNumber = "+989127771234",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Mike",
                Lastname = "Hannigan",
                Email = "Mike.Hannigan@mail.com",
                PhoneNumber = "+989128881234",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Richard",
                Lastname = "Burke",
                Email = "Richard.Burke@mail.com",
                PhoneNumber = "+989129991234",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            }
        };

        private static readonly List<Lecturer> SampleLecturers = new List<Lecturer>()
        {
            new Lecturer
            {
                LecturerId = Guid.NewGuid(),
                Firstname = "Charlie",
                Lastname = "Wheeler",
                Email = "Charlie.Wheeler@mail.com",
                PhoneNumber = "+989121114321",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            },
            new Lecturer()
            {
                LecturerId = Guid.NewGuid(),
                Firstname = "Benjamin",
                Lastname = "Hobart",
                Email = "Benjamin.Hobart@mail.com",
                PhoneNumber = "+989122224321",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            },
            new Lecturer()
            {
                LecturerId = Guid.NewGuid(),
                Firstname = "David",
                Lastname = "Scientist",
                Email = "David.Scientist@mail.com",
                PhoneNumber = "+989123334321",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            },
            new Lecturer()
            {
                LecturerId = Guid.NewGuid(),
                Firstname = "Dummy",
                Lastname = "Spafford",
                Email = "Dummy.Spafford@mail.com",
                PhoneNumber = "+9891244441234",
                Password = "10000.p2/Ke+kf2/wtMVFl0Xe6zg==.QAImMHZZpv+bAp9OfIP+mUvO1xBldk5dHla5e65fhAY="
            }
        };

        private static readonly List<Period> SamplePeriods = new List<Period>()
        {
            new Period
            {
                PeriodId = Guid.NewGuid(),
                Name = "FALL SEMESTER 2022",
                StartDate = new DateOnly(2022, 9, 26),
                EndDate = new DateOnly(2023, 1, 27),
            },
            new Period
            {
                PeriodId = Guid.NewGuid(),
                Name = "SPRING SEMESTER 2023",
                StartDate = new DateOnly(2023, 2, 1),
                EndDate = new DateOnly(2023, 5, 28),
            }
        };

        private static readonly List<Course> SampleCourses = new List<Course>()
        {
            new Course
            {
                CourseId = Guid.NewGuid(),
                Code = "COMP-032",
                Title = "Introduction to Programming",
            },
            new Course
            {
                CourseId = Guid.NewGuid(),
                Code = "ENG-431",
                Title = "English For Beginners",
            },
            new Course
            {
                CourseId = Guid.NewGuid(),
                Code = "PAL-122",
                Title = "Everything About Dinosaurs!",
            },
        };

        public async Task SeedMigrationAsync(SqliteDbContext context)
        {
            CreateDefaultStudents(context);
            CreateDefaultLecturers(context);
            CreateDefaultPeriods(context);
            CreateDefaultCourses(context);
            CreateDefaultSections(context);

            await context.SaveChangesAsync();
        }

        private static void CreateDefaultStudents(SqliteDbContext context)
        {
            if (context.Set<Student>().Any())
            {
                return;
            }

            context.Set<Student>().AddRange(SampleStudents);
        }

        private static void CreateDefaultLecturers(SqliteDbContext context)
        {
            if (context.Set<Lecturer>().Any())
            {
                return;
            }

            context.Set<Lecturer>().AddRange(SampleLecturers);
        }

        private static void CreateDefaultPeriods(SqliteDbContext context)
        {
            if (context.Set<Period>().Any())
            {
                return;
            }

            context.Set<Period>().AddRange(SamplePeriods);
        }

        private static void CreateDefaultCourses(SqliteDbContext context)
        {
            if (context.Set<Course>().Any())
            {
                return;
            }

            context.Set<Course>().AddRange(SampleCourses);
        }

        private static void CreateDefaultSections(SqliteDbContext context)
        {
            if (context.Set<Section>().Any())
            {
                return;
            }

            context.Set<Section>().AddRange(new List<Section>()
            {
                new Section
                {
                    SectionId = Guid.NewGuid(),
                    Number = "Sec-010",
                    Details = "Engineering Inst. 1st Floor. Room 3.",

                    Course = SampleCourses.First(),
                    Period = SamplePeriods.First(),
                    Lecturers = SampleLecturers.Take(1).ToList(),
                    Students = SampleStudents.Take(3).ToList()
                },
                new Section
                {
                    SectionId = Guid.NewGuid(),
                    Number = "Sec-008",
                    Details = "Language Inst. 3rd Floor. Room 3.",

                    Course = SampleCourses.Skip(1).Take(1).First(),
                    Period = SamplePeriods.First(),
                    Lecturers = SampleLecturers.Skip(1).Take(2).ToList(),
                    Students = SampleStudents.Skip(2).Take(4).ToList()
                },
                new Section
                {
                    SectionId = Guid.NewGuid(),
                    Number = "Sec-014",
                    Details = "History Inst. 2nd Floor, Grand Hall.",

                    Course = SampleCourses.Skip(2).Take(1).First(),
                    Period = SamplePeriods.Skip(1).Take(1).First(),
                    Lecturers = SampleLecturers.Skip(2).Take(2).ToList(),
                    Students = SampleStudents.Skip(3).Take(4).ToList()
                },
            });
        }

    }
}
