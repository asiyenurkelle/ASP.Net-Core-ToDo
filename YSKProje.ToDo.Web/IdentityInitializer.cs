using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YSKProje.ToDo.Entities.Concrete;

namespace YSKProje.ToDo.Web
{
    public static class IdentityInitializer
    {
        public static async Task SeedData(UserManager<AppUser>userManager,RoleManager<AppRole> roleManager)
        {
            var adminRole = await roleManager.FindByNameAsync("Admin");
            if (adminRole == null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "Admin" });
            }
            var memberRole = await roleManager.FindByNameAsync("Member");
            if(memberRole== null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "Member" });
            }

            var adminUser = await userManager.FindByNameAsync("asiye");
            if (adminUser==null)
            {
                //asiye isimli kullanıcı yoksa asagıdaki özelliklere sahip asiye kullanıcısını oluşturcak.
                AppUser user = new AppUser
                {
                    Name = "Asiye",
                    Surname = "Kelle",
                    UserName = "asiye",
                    Email = "asiyekelle7@gmail.com"
                };
                //oluşturdugu asiye kullanıcısının sifresini 1 atadık.
                await userManager.CreateAsync(user,"1");
                //olusturdugumuz kullanıcıya rol atadık(admin rolü)
                await userManager.AddToRoleAsync(user, "Admin");
            }

        }
    }
}
