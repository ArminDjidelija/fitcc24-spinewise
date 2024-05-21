using Microsoft.EntityFrameworkCore;
using SpineWise.ClassLibrary.Models;

namespace SpineWise.Web.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Chair> Chairs { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<UserActionLog> UserActionLogs { get; set; }
        public DbSet<SignInLog> SignInLogs { get; set; }
        public DbSet<SignOutLog> SignOutLogs { get; set; }
        public DbSet<ChairModel> ChairModels { get; set; }
        public DbSet<ChairUser> ChairsUsers { get; set; }
        public DbSet<FingerprintLog> FingerprintLogs{ get; set; }
        public DbSet<SpineWiseDataLog> SpineWiseDataLogs { get; set; }
        public DbSet<SpinePostureDataLog> SpinePostureDataLogs { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().UseTptMappingStrategy();

            modelBuilder.Entity<User>()
                .HasOne(u => u.Chair)
                .WithMany()
                .HasForeignKey(u => u.ChairId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
