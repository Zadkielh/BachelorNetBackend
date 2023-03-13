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
       public DbSet<UserRole> UsersRoles { get; set; }
       public DbSet<User> Users { get; set; }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Post> Posts { get; set; }
       public DbSet<Status> Statuses { get; set; }
       public DbSet<Comment> Comments { get; set; }
       public DbSet<Vote> Votes { get; set; }
       public DbSet<Favorit> Favorites { get; set; }
   }
   
   public class ApplicationDbInitializer
   {
       public void Initialize(ApplicationDbContext db)
       {
           // Delete the database before we initialize it. This is common to do during development.
           db.Database.EnsureDeleted();

           // Recreate the database and tables according to our models
           db.Database.EnsureCreated();

           
           // Add UserRoles test data
           var userRole = new[]
           {
               new UserRole("Admin", "Granted full permission"),
               new UserRole("User", "Regular user. Can only manage their own content")
           };

           db.UsersRoles.AddRange(userRole);
           db.SaveChanges(); // Finally save changes
            
           
           // Add Users test data
           var adminUser = db.UsersRoles.Where(t => t.Type == "Admin").FirstOrDefault();
           var normalUser = db.UsersRoles.Where(t => t.Type == "User").FirstOrDefault();

           var user = new[]
           {
                new User(adminUser, Guid.NewGuid(), "Admin", "admin@admin.admin"),
                new User(normalUser, Guid.NewGuid(), "Trine Trynet", "trine@trynet.no"),
                new User(normalUser, Guid.NewGuid(), "Jonny Bravo", "jonny@bravo.no"),
                new User(normalUser, Guid.NewGuid(), "Hans Henrik", "hans@henrik.no")
           };
            
           db.Users.AddRange(user);
           db.SaveChanges();
           
           
           // Add Categories test data
           var category = new[]
           {
               new Category("Ris", "Noe som er negativt"),
               new Category("Ros", "Noe som er positivt"),
               new Category("Funksjonalitet", "Forslag til ny funksjonalitet")
           };
           
           db.Categories.AddRange(category);
           db.SaveChanges();
           
           
           // Add Status test data
           var s1 = new Status(null, "Pending", "Venter på svar");
           var s2 = new Status(null, "Pending", "Venter på svar");

           var status = new[]
           {
               s1, 
               s2
           };
           
           db.Statuses.AddRange(status);
           db.SaveChanges();
           
           // Add Posts test data
           var getUsers = db.Users.ToList();
           var getRis = db.Categories.Where(r => r.Type == "Ris").FirstOrDefault();
           var getRos = db.Categories.Where(r => r.Type == "Ros").FirstOrDefault();

           var post = new[]
           {
               new Post(getUsers[0], getRis, s1,"Elendig UX design", "Kunne gjort det mye bedre selv"),
               new Post(getUsers[1], getRos, s2, "Drit bra animasjoner", "10 av 10 animasjoner på forsiden. Dere har flinke utviklere")
           };
            
           db.Posts.AddRange(post);
           db.SaveChanges();
       }
   }
}


