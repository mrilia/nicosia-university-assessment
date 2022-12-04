
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Nicosia.Assessment.Application.Interfaces;
using Nicosia.Assessment.Domain.Models.Course;
using Nicosia.Assessment.Domain.Models.Period;
using Nicosia.Assessment.Domain.Models.Section;
using Nicosia.Assessment.Domain.Models.User;

namespace Nicosia.Assessment.Persistence.Context
{
    public class SqliteDbContext : DbContext, IBaseContext, ILecturerContext, IStudentContext, ICourseContext, IPeriodContext, ISectionContext
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


        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Section> Sections { get; set; }

        public Task SaveAsync(CancellationToken cancellationToken) =>
            base.SaveChangesAsync(cancellationToken);

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
