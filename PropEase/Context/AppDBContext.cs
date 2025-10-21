using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PropEase.Models;

namespace PropEase.Context
{
    public class AppDBContext:DbContext
    {
        public DbSet<ContactDetail> ContactsMsg { get; set; }
        public DbSet<User> UsersMsg { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Role> Roles { get; set; }
        public AppDBContext(DbContextOptions<AppDBContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactDetail>().HasNoKey();
            base.OnModelCreating(modelBuilder);
        }
        public async Task SaveContactDetailAsync(string name,string email,string subject,string message)
        {
            var parameters = new[] { 
            new SqlParameter("@name",name),
            new SqlParameter("@email",email),
            new SqlParameter("@subject",subject),
            new SqlParameter("@message",message)
            };
            await Database.ExecuteSqlRawAsync("EXEC DumpContactMessage @name, @email, @subject, @message",parameters);
        }
    }
}
