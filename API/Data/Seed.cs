using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    //public class Seed
    //{
    //    public static async Task SeedUsers(UserManager<AppUser> userManager)
    //    {
    //        if (await userManager.Users.AnyAsync()) return;

    //        var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
    //        var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

    //        foreach (var user in users)
    //        {
               

    //            user.UserName = user.UserName;
    //            //user.DateOfBirth = DateOnly.FromDateTime(user.DateOfBirth);
    //            //user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));

    //            //user.PasswordSalt = hmac.Key;

    //            await userManager.CreateAsync(user, "Pa$$w0rd");
    //            //await context.SaveChangesAsync();

    //           //  await Task.FromResult("How far");
    //        }


    //      //  OverLoadedMethods f1 = new OverLoadedMethods();

    //       // f1.FunctionOverload();
    //    }
    //}

    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });
        }
    }

}