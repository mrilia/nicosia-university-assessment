
//using Microsoft.EntityFrameworkCore;
//using System.Threading;
//using System.Threading.Tasks;
//using Nicosia.Assessment.Application.Interfaces;
//using Nicosia.Assessment.Domain.Models.User;

//namespace Nicosia.Assessment.Persistence.Context
//{
//    public class StudentContext : DbContext, IStudentContext
//    {
//        public StudentContext()
//        {
//        }

//        public StudentContext(DbContextOptions<StudentContext> options) : base(options)
//        {
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//                optionsBuilder.UseSqlite(@"Data Source=Database.db");
//            }
//        }

//        public DbSet<Student> Students { get; set; }


//        public Task SaveAsync(CancellationToken cancellationToken) =>
//            base.SaveChangesAsync(cancellationToken);

//        public Task CloseConnection() =>
//            base.Database.CloseConnectionAsync();


//        public Task CloseConnection(CancellationToken cancellationToken)
//            => base.Database.CloseConnectionAsync();

//        public void Save() =>
//            base.SaveChanges();

//        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
//            modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentContext).Assembly);
//    }
//}
