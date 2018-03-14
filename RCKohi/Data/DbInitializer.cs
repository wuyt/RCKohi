using Microsoft.AspNetCore.Identity;
using RCKohi.Models;
using RCKohi.Models.Demo;
using System;
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

            var animes = new Anime[]
{
            new Anime{Name="EVA", IndexShow=true, Number=203940, BroadcastDate=DateTime.Parse("1995-10-4"),},
            new Anime{Name="化物语", IndexShow=true, Number=110502, BroadcastDate=DateTime.Parse("2009-8-1"),},
            new Anime{Name="机动战士高达0079", IndexShow=false, Number=100350, BroadcastDate=DateTime.Parse("1979-3-1"),},
            new Anime{Name="魔法少女小圆", IndexShow=true, Number=84256, BroadcastDate=DateTime.Parse("2011-11-1"),},
            new Anime{Name="机动战士高达SEED DESTINY", IndexShow=false, Number=89404, BroadcastDate=DateTime.Parse("2004-8-1"),},
            new Anime{Name="机动战士高达SEED", IndexShow=false, Number=83383, BroadcastDate=DateTime.Parse("2002-8-1"),},
            new Anime{Name="凉宫春日的忧郁", IndexShow=true, Number=77154, BroadcastDate=DateTime.Parse("2006-3-1"),},
            new Anime{Name="叛逆的鲁鲁修", IndexShow=false, Number=72603, BroadcastDate=DateTime.Parse("2006-8-1"),},
            new Anime{Name="YURI!!! on ICE", IndexShow=false, Number=67741, BroadcastDate=DateTime.Parse("2016-8-1"),},
            new Anime{Name="阿松", IndexShow=false, Number=65513, BroadcastDate=DateTime.Parse("2015-8-1"),},
            new Anime{Name="星际牛仔", IndexShow=true, Number=65342, BroadcastDate=DateTime.Parse("1998-3-1"),},
            new Anime{Name="LoveLive!第二季", IndexShow=true, Number=65008, BroadcastDate=DateTime.Parse("2014-3-1"),},
            new Anime{Name="伪物语", IndexShow=false, Number=64610, BroadcastDate=DateTime.Parse("2012-11-1"),},
            new Anime{Name="叛逆的鲁鲁修 R2", IndexShow=false, Number=64204, BroadcastDate=DateTime.Parse("2008-3-1"),},
            new Anime{Name="Macross F", IndexShow=false, Number=61811, BroadcastDate=DateTime.Parse("2008-3-1"),},
            new Anime{Name="机动战士Z高达", IndexShow=false, Number=59909, BroadcastDate=DateTime.Parse("1985-3-1"),},
            new Anime{Name="轻音少女", IndexShow=true, Number=56777, BroadcastDate=DateTime.Parse("2009-3-1"),},
            new Anime{Name="凉宫春日的忧郁2009", IndexShow=true, Number=54676, BroadcastDate=DateTime.Parse("2009-3-1"),},
            new Anime{Name="钢之炼金术师", IndexShow=true, Number=55266, BroadcastDate=DateTime.Parse("2003-8-1"),},
            new Anime{Name="LoveLive!Sunshine!!", IndexShow=true, Number=110502, BroadcastDate=DateTime.Parse("2016-8-1"),},
            new Anime{Name="龙珠GT", IndexShow=false, Number=52923, BroadcastDate=DateTime.Parse("1996-11-1"),},
            new Anime{Name="进击的巨人", IndexShow=true, Number=52217, BroadcastDate=DateTime.Parse("2013-3-1"),},
            new Anime{Name="Fate/Zero First Season", IndexShow=true, Number=52133, BroadcastDate=DateTime.Parse("2011-8-1"),},
            new Anime{Name="机动战舰Nadesico", IndexShow=false, Number=48665, BroadcastDate=DateTime.Parse("1996-8-1"),},
            new Anime{Name="轻音少女第二季", IndexShow=true, Number=48271, BroadcastDate=DateTime.Parse("2010-3-1"),},
            new Anime{Name="少女与战车", IndexShow=true, Number=48026, BroadcastDate=DateTime.Parse("2012-8-1"),},
            new Anime{Name="Fate/Zero Second Season", IndexShow=true, Number=45804, BroadcastDate=DateTime.Parse("2012-3-1"),}
};

            foreach (Anime a in animes)
            {
                context.Anime.Add(a);
            }

            context.SaveChanges();
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
