using Microsoft.AspNetCore.Identity;
using RCKohi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RCKohi.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();  

            if (context.Users.Any())
            {
                return;   // DB has been seeded  
            }

            await CreateDefaultUserAndRole(userManager, roleManager);
            
        }

        private static async Task CreateDefaultUserAndRole(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            string roleAdmin = "admin";
            string roleUser = "user";

            await CreateDefaultRole(roleManager, roleAdmin);
            await CreateDefaultRole(roleManager, roleUser);
            var user = await CreateDefaultUser(userManager);
            await AddDefaultRoleToDefaultUser(userManager, roleAdmin, user);
            await AddDefaultRoleToDefaultUser(userManager, roleUser, user);
        }

        private static async Task CreateDefaultRole(RoleManager<IdentityRole> roleManager, string role)
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }

        private static async Task<ApplicationUser> CreateDefaultUser(UserManager<ApplicationUser> userManager)
        {
            var user = new ApplicationUser { Email = "5140075@qq.com", UserName = "admin" };

            await userManager.CreateAsync(user,"abc123");

            var createdUser = await userManager.FindByEmailAsync("5140075@qq.com");
            return createdUser;
        }

        private static async Task AddDefaultRoleToDefaultUser(UserManager<ApplicationUser> userManager, string role, ApplicationUser user)
        {
            await userManager.AddToRoleAsync(user, role);
        }
    }
}
