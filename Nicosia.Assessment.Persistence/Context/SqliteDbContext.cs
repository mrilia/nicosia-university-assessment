﻿
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Domain.Models.ApprovalRequests;
using Nicosia.Assessment.Domain.Models.Course;
using Nicosia.Assessment.Domain.Models.Period;
using Nicosia.Assessment.Domain.Models.Section;
using Nicosia.Assessment.Domain.Models.User;
using Nicosia.Assessment.Persistence.Seed;

namespace Nicosia.Assessment.Persistence.Context
{
    public class SqliteDbContext : DbContext, IBaseContext, ILecturerContext, IStudentContext, ICourseContext, IPeriodContext, ISectionContext, IAdminContext, IApprovalRequestContext
    {
        public SqliteDbContext()
        {
        }

        public SqliteDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite(@"Data Source=Database.db");
            }
        }


        public DbSet<Admin> Admins { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<ApprovalRequest> ApprovalRequests { get; set; }

        public Task SaveAsync(CancellationToken cancellationToken) =>
            base.SaveChangesAsync(cancellationToken);

        public Task SeedDefaultData()
        {
            return new DbContextSeed().SeedMigrationAsync(this);
        }

        public Task CloseConnection() =>
            base.Database.CloseConnectionAsync();


        public Task CloseConnection(CancellationToken cancellationToken)
            => base.Database.CloseConnectionAsync();

        public void Save() =>
            base.SaveChanges();

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SqliteDbContext).Assembly);
    }
}
