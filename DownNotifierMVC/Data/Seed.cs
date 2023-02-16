using AccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using ModelsLayer.Entities;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DownNotifierMVC.Data
{
    public class Seed
    {
        public static async Task SeedUsers(ApplicationDbContext context)
        {
            if (await context.SystemUsers.AnyAsync()) return;

            using var hamc = new HMACSHA512();
            var item = new SystemUser()
            {
                UserName = "admin",
                Email = "monzer.al.khiami@gmail.com",
                PasswordHash = hamc.ComputeHash(Encoding.UTF8.GetBytes("admin")),
                PasswordSalt = hamc.Key
            };
            context.SystemUsers.Add(item);
            await context.SaveChangesAsync();

        }
    }
}
