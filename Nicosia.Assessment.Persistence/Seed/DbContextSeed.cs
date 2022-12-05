using Nicosia.Assessment.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nicosia.Assessment.Domain.Models.Course;
using Nicosia.Assessment.Domain.Models.Period;
using Nicosia.Assessment.Domain.Models.Section;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Persistence.Seed
{
    internal class DbContextSeed
    {
        private static readonly List<Admin> SampleAdmins = new List<Admin>()
        {
            new Admin
            {
                AdminId = Guid.NewGuid(),
                Firstname = "Gunther",
                Lastname = "Central Perk's Manager",
                DateOfBirth = new DateOnly(1967,09,09),
                Email = "Gunther.central.perk@mail.com",
                PhoneNumber = "+989127891234",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            }
        };
        
        private static readonly List<Student> SampleStudents = new List<Student>()
        {
            new Student
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Ross",
                Lastname = "Geller",
                DateOfBirth = new DateOnly(1967,10,18),
                Email = "ross.geller@mail.com",
                PhoneNumber = "+989121111234",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Monica",
                Lastname = "Geller",
                DateOfBirth = new DateOnly(1969,3,13),
                Email = "monica.geller@mail.com",
                PhoneNumber = "+989122221234",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Rachel",
                Lastname = "Green",
                DateOfBirth = new DateOnly(1969,5,5),
                Email = "Rachel.Green@mail.com",
                PhoneNumber = "+989123331234",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Phoebe",
                Lastname = "Buffay",
                DateOfBirth = new DateOnly(1965,2,16),
                Email = "Phoebe.Buffay@mail.com",
                PhoneNumber = "+9891244441234",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Joey",
                Lastname = "Tribbiani",
                DateOfBirth = new DateOnly(1967,1,1),
                Email = "Joey.Tribbiani@mail.com",
                PhoneNumber = "+989125551234",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Chandler",
                Lastname = "Bing",
                DateOfBirth = new DateOnly(1967,4,14),
                Email = "Chandler.Bing@mail.com",
                PhoneNumber = "+989126661234",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Janice",
                Lastname = "Hosenstein",
                DateOfBirth = new DateOnly(1968,5,13),
                Email = "Janice.Hosenstein@mail.com",
                PhoneNumber = "+989127771234",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Mike",
                Lastname = "Hannigan",
                DateOfBirth = new DateOnly(1967,8,25),
                Email = "Mike.Hannigan@mail.com",
                PhoneNumber = "+989128881234",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            },
            new Student()
            {
                StudentId = Guid.NewGuid(),
                Firstname = "Richard",
                Lastname = "Burke",
                DateOfBirth = new DateOnly(1949,6,17),
                Email = "Richard.Burke@mail.com",
                PhoneNumber = "+989129991234",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            }
        };

        private static readonly List<Lecturer> SampleLecturers = new List<Lecturer>()
        {
            new Lecturer
            {
                LecturerId = Guid.NewGuid(),
                Firstname = "Charlie",
                Lastname = "Wheeler",
                DateOfBirth = new DateOnly(1969,11,1),
                Email = "Charlie.Wheeler@mail.com",
                PhoneNumber = "+989121114321",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            },
            new Lecturer()
            {
                LecturerId = Guid.NewGuid(),
                Firstname = "Benjamin",
                Lastname = "Hobart",
                DateOfBirth = new DateOnly(1960,7,1),
                Email = "Benjamin.Hobart@mail.com",
                PhoneNumber = "+989122224321",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            },
            new Lecturer()
            {
                LecturerId = Guid.NewGuid(),
                Firstname = "David",
                Lastname = "Scientist",
                DateOfBirth = new DateOnly(1963,10,10),
                Email = "David.Scientist@mail.com",
                PhoneNumber = "+989123334321",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
            },
            new Lecturer()
            {
                LecturerId = Guid.NewGuid(),
                Firstname = "Dummy",
                Lastname = "Spafford",
                DateOfBirth = new DateOnly(1950,3,20),
                Email = "Dummy.Spafford@mail.com",
                PhoneNumber = "+9891244441234",
                Password = "10000.PEkWtk97EMM1C189V9AbvA==./OIul15w/WYqS+MrBL2ERpVsDkNWUFavnhwMZS5H++0="
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
            CreateDefaultAdmins(context);
            CreateDefaultStudents(context);
            CreateDefaultLecturers(context);
            CreateDefaultPeriods(context);
            CreateDefaultCourses(context);
            CreateDefaultSections(context);

            await context.SaveChangesAsync();
        }

        private static void CreateDefaultAdmins(SqliteDbContext context)
        {
            if (context.Set<Admin>().Any())
            {
                return;
            }

            context.Set<Admin>().AddRange(SampleAdmins);
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
