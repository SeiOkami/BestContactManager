using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Models;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser
{
    public int Year { get; set; }

    public User()
    {

    }

    public User(string key, string pass, string id)
    {
        Id = id; 
        UserName = key;
        Email = key;
        NormalizedEmail = key.ToUpper();
        NormalizedUserName = key.ToUpper();
        
        LockoutEnabled = true;
        EmailConfirmed = true;
        
        PasswordHash = new PasswordHasher<User>().HashPassword(this, pass);
    }

}

