using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Identity.Models
{
    public static class ExtensionsModel
    {
        public async static Task<AppUser> AddNewUserAsync(this UserManager<AppUser> userManager,
            string userName, string fullName, string email, string password, string id = "")
        {

            var user = new AppUser
            {
                UserName = userName,
                Email = email,
                EmailConfirmed = true,
            };

            if (!String.IsNullOrEmpty(id))
                user.Id = id;

            var thisResult = await userManager.CreateAsync(user, password);
            if (!thisResult.Succeeded)
                throw new Exception(thisResult.Errors.First().Description);

            thisResult = await userManager.AddClaimsAsync(user, 
                new Claim[]{
                    new Claim(JwtClaimTypes.Name, fullName),
                    //new Claim(JwtClaimTypes.Id, user.Id),
                    //new Claim(JwtClaimTypes.Subject, user.Id)
                });

            if (!thisResult.Succeeded)
                throw new Exception(thisResult.Errors.First().Description);

            thisResult = await userManager.AddToRoleAsync(user, Configuration.UserRoleName);
            if (!thisResult.Succeeded)
                throw new Exception(thisResult.Errors.First().Description);

            return user;

        }
    }
}
