using Microsoft.EntityFrameworkCore;
using BachelorOppgaveBackend.Model;

namespace BachelorOppgaveBackend.PostgreSQL
{
   public class ApplicationDbContext : DbContext
   {
       // This constructor must exist so you can register it as a service (next slide)
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
       { }

       // Each DB set maps to a table in the database
       public DbSet<User> Users { get; set; }
       public DbSet<UserRole> UsersRoles { get; set; }
       
   }
   
   public class ApplicationDbInitializer
   {
       public void Initialize(ApplicationDbContext db)
       {
           // Delete the database before we initialize it. This is common to do during development.
           db.Database.EnsureDeleted();

           // Recreate the database and tables according to our models
           db.Database.EnsureCreated();

           // Add test data to simplify debugging and testing
           var userRole = new[]
           {
               new UserRole("Admin", "Granted full permission"),
               new UserRole("User", "Regular user. Can only manage their own content")
           };

           db.UsersRoles.AddRange(userRole);
           db.SaveChanges(); // Finally save changes

           var adminUser = db.UsersRoles.Where(t => t.Type == "Admin").FirstOrDefault();

           var user = new[]
           {
                new User(Guid.NewGuid(), "Admin", "admin@admin.admin", adminUser.Id)
           };
            
           db.Users.AddRange(user);
           db.SaveChanges();
       }
   }
}


