using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Greenlight.Models
{
    internal class RoleActions
    {
        internal void AddUserAndRole()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            IdentityResult idRoleResult;
            IdentityResult idUserResult;

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            if (!roleManager.RoleExists("Developer"))
            {
                idRoleResult = roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            var appUser = new ApplicationUser
            {
                UserName = "DCC_Games",
                Email = "dev@dccgames.com"
            };
            idUserResult = userManager.Create(appUser, "Password1!");
            if (!userManager.IsInRole(userManager.FindByEmail("dev@dccgames.com").Id, "Developer")) 
            {
                idUserResult = userManager.AddToRole(userManager.FindByEmail("dev@dccgames.com").Id, "Developer");
            }
        }
    }
}