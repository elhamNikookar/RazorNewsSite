using Blazor.Data.Entities.NewsEntities;
using Blazor.Data.Entities.UserEntities;
using CommonLayer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        #region DbSet
        public DbSet<News> Newses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<News>()
                .HasQueryFilter(x => !x.IsDeleted);

            //modelBuilder.Entity<Comment>()
            //    .HasQueryFilter(x => !x.IsActive);

            modelBuilder.Entity<User>()
                .HasQueryFilter(x => !x.IsDelete);

            #region Seed Role Data
            modelBuilder.Entity<Role>().HasData
                (new Role()
                {
                    RoleId = 1,
                    RoleTitle = "Admin",
                    IsDelete = false
                },
                new Role()
                {
                    RoleId = 2,
                    RoleTitle = "Operator",
                    IsDelete = false
                },
                new Role()
                {
                    RoleId = 3,
                    RoleTitle = "Customer",
                    IsDelete = false
                }
            );
            modelBuilder.Entity<User>().HasData
                (new User()
                {
                    UserId = 1,
                    UserName = "admin",
                    Email = "admin@mail.com",
                    Password = "admin@123",
                    IsActive = true,
                    UserAvatar = StaticDetail.DefaultUserAvatar,
                    RoleId = 1
                });
            #endregion

            base.OnModelCreating(modelBuilder);
        }

    }
}
