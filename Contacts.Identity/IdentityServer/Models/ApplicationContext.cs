using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Models;

public class ApplicationContext : IdentityDbContext<User>
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        var created = Database.EnsureCreated();
        if (created)
        {

            var userAdmin = AddUser(
                "admin@admin.admin", "admin",
                "5C26B4D6-E58E-4C4B-889B-322F744FEF2B");

            var userUser = AddUser(
                "user@user.user", "user",
                "6EE7EC0C-1F14-40EF-97FD-7ECB79400962");

            var roleAdmin = AddRole("admin", "92AF075A-97C2-4F0D-B256-2B00D021B4A7");
            var roleUser = AddRole("user", "5DA0C506-B448-4D8C-8A5B-E147A5B1AFEF");

            SetRole(userAdmin, roleAdmin);
            SetRole(userUser, roleUser);

            SaveChanges();

        }
    }


    private User AddUser(string key, string pass, string id)
    {
        var newUser = new User(key, pass, id);
        Users.Add(newUser);
        return newUser;
    }

    private IdentityRole AddRole(string name, string id)
    {
        var newRole = new IdentityRole(name);
        newRole.Id = id;

        Roles.Add(newRole);

        return newRole;
    }

    private void SetRole(User user, IdentityRole role)
    {
        var newRole = new IdentityUserRole<string>();
        newRole.UserId = user.Id;
        newRole.RoleId = role.Id;
        UserRoles.Add(newRole);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
