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
    public class PostgresDbContextSeed
    {
        public async Task SeedMigrationAsync(PostgresDbContext context)
        {
            CreateDefaultAdmins(context);
            CreateDefaultStudents(context);
            CreateDefaultLecturers(context);
            CreateDefaultPeriods(context);
            CreateDefaultCourses(context);
            CreateDefaultSections(context);

            await context.SaveChangesAsync();
        }

        private static void CreateDefaultAdmins(PostgresDbContext context)
        {
            if (context.Set<Admin>().Any())
            {
                return;
            }

            context.Set<Admin>().AddRange(SampleData.SampleData.SampleAdmins);
        }
        
        private static void CreateDefaultStudents(PostgresDbContext context)
        {
            if (context.Set<Student>().Any())
            {
                return;
            }

            context.Set<Student>().AddRange(SampleData.SampleData.SampleStudents);
        }

        private static void CreateDefaultLecturers(PostgresDbContext context)
        {
            if (context.Set<Lecturer>().Any())
            {
                return;
            }

            context.Set<Lecturer>().AddRange(SampleData.SampleData.SampleLecturers);
        }

        private static void CreateDefaultPeriods(PostgresDbContext context)
        {
            if (context.Set<Period>().Any())
            {
                return;
            }

            context.Set<Period>().AddRange(SampleData.SampleData.SamplePeriods);
        }

        private static void CreateDefaultCourses(PostgresDbContext context)
        {
            if (context.Set<Course>().Any())
            {
                return;
            }

            context.Set<Course>().AddRange(SampleData.SampleData.SampleCourses);
        }

        private static void CreateDefaultSections(PostgresDbContext context)
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

                    Course = SampleData.SampleData.SampleCourses.First(),
                    Period = SampleData.SampleData.SamplePeriods.First(),
                    Lecturers = SampleData.SampleData.SampleLecturers.Take(1).ToList(),
                    Students = SampleData.SampleData.SampleStudents.Take(3).ToList()
                },
                new Section
                {
                    SectionId = Guid.NewGuid(),
                    Number = "Sec-008",
                    Details = "Language Inst. 3rd Floor. Room 3.",

                    Course = SampleData.SampleData.SampleCourses.Skip(1).Take(1).First(),
                    Period = SampleData.SampleData.SamplePeriods.First(),
                    Lecturers = SampleData.SampleData.SampleLecturers.Skip(1).Take(2).ToList(),
                    Students = SampleData.SampleData.SampleStudents.Skip(2).Take(4).ToList()
                },
                new Section
                {
                    SectionId = Guid.NewGuid(),
                    Number = "Sec-014",
                    Details = "History Inst. 2nd Floor, Grand Hall.",

                    Course = SampleData.SampleData.SampleCourses.Skip(2).Take(1).First(),
                    Period = SampleData.SampleData.SamplePeriods.Skip(1).Take(1).First(),
                    Lecturers = SampleData.SampleData.SampleLecturers.Skip(2).Take(2).ToList(),
                    Students = SampleData.SampleData.SampleStudents.Skip(3).Take(4).ToList()
                },
            });
        }

    }
}
