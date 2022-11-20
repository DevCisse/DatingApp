using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);

            foreach (var user in users)
            {
                var hmac = new HMACSHA512();

                user.UserName = user.UserName;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd"));

                user.PasswordSalt = hmac.Key;

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

               //  await Task.FromResult("How far");
            }


          //  OverLoadedMethods f1 = new OverLoadedMethods();

           // f1.FunctionOverload();
        }
    }



    public class OverLoadedMethods
    {
        public int FunctionOverload(int no1, int no2)
        {
            return no1 + no2;
        }
        public double FunctionOverload(int no1, double no2, int no3)
        {
            return no1 + no2 + no3;
        }

        public void FunctionOverload(double no1, double no2)
        {
            double sum = no1 + no2;
        }

        //answer = within the same class of the OverLoadedMethods ...
        public OverLoadedMethods()
        {

        }



    }
}