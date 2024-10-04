using GeocachingApp.Data.Enum;
using GeocachingApp.Models;

namespace GeocachingApp.Data;

public class Seed
{
    public static void SeedData(IApplicationBuilder applicationBuilder)
    {
        using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        {
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            if (!context.Clubs.Any())
            {
                context.Clubs.AddRange(new List<Club>()
                {
                    new Club()
                    {
                        Title = "Geocaching Club 1",
                        Image = "https://imgs.search.brave.com/gZOwT6aSBjkpLKuGG44co9AO5cB_z7tY3DnNtGKWgYg/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvNDY3/MjI3MzgzL3Bob3Rv/L2dlb2NhY2hpbmcu/anBnP3M9NjEyeDYx/MiZ3PTAmaz0yMCZj/PWZkMUNuMkhNSTBk/Y3kzb2VMNnh6VEVO/MDNJRS1MUTg3dk1w/Ty1fZmtDaTQ9",
                        Description = "This is club description",
                        Address = new Address()
                        {
                            Street = "Alexander Platze 1",
                            City = "Berlin",
                            Country = "Germany"
                        }
                    },
                    new Club()
                    {
                        Title = "Geocaching Club 2",
                        Image = "https://imgs.search.brave.com/gZOwT6aSBjkpLKuGG44co9AO5cB_z7tY3DnNtGKWgYg/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvNDY3/MjI3MzgzL3Bob3Rv/L2dlb2NhY2hpbmcu/anBnP3M9NjEyeDYx/MiZ3PTAmaz0yMCZj/PWZkMUNuMkhNSTBk/Y3kzb2VMNnh6VEVO/MDNJRS1MUTg3dk1w/Ty1fZmtDaTQ9",
                        Description = "This is club description",
                        Address = new Address()
                        {
                            Street = "Alexander Platze 1",
                            City = "Berlin",
                            Country = "Germany"
                        }
                    },
                    new Club()
                    {
                        Title = "Geocaching Club 3",
                        Image = "https://imgs.search.brave.com/gZOwT6aSBjkpLKuGG44co9AO5cB_z7tY3DnNtGKWgYg/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvNDY3/MjI3MzgzL3Bob3Rv/L2dlb2NhY2hpbmcu/anBnP3M9NjEyeDYx/MiZ3PTAmaz0yMCZj/PWZkMUNuMkhNSTBk/Y3kzb2VMNnh6VEVO/MDNJRS1MUTg3dk1w/Ty1fZmtDaTQ9",
                        Description = "This is club description",
                        Address = new Address()
                        {
                            Street = "Alexander Platze 1",
                            City = "Berlin",
                            Country = "Germany"
                        }
                    }
                });
                context.SaveChanges();
            }
            //Caches
            if (!context.Caches.Any())
            {
                context.Caches.AddRange(new List<Cache>()
                {
                    new Cache()
                    {
                        Title = "Geocaching Cache 1",
                        Image = "https://imgs.search.brave.com/gZOwT6aSBjkpLKuGG44co9AO5cB_z7tY3DnNtGKWgYg/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvNDY3/MjI3MzgzL3Bob3Rv/L2dlb2NhY2hpbmcu/anBnP3M9NjEyeDYx/MiZ3PTAmaz0yMCZj/PWZkMUNuMkhNSTBk/Y3kzb2VMNnh6VEVO/MDNJRS1MUTg3dk1w/Ty1fZmtDaTQ9",
                        Description = "This is cache description",
                        CacheType= CacheType.MultiCache,
                        Address = new Address()
                        {
                            Street = "Alexander Platze 1",
                            City = "Berlin",
                            Country = "Germany"
                        }
                    },
                    new Cache()
                    {
                        Title = "Geocaching Cache 2",
                        Image = "https://imgs.search.brave.com/gZOwT6aSBjkpLKuGG44co9AO5cB_z7tY3DnNtGKWgYg/rs:fit:860:0:0:0/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vaWQvNDY3/MjI3MzgzL3Bob3Rv/L2dlb2NhY2hpbmcu/anBnP3M9NjEyeDYx/MiZ3PTAmaz0yMCZj/PWZkMUNuMkhNSTBk/Y3kzb2VMNnh6VEVO/MDNJRS1MUTg3dk1w/Ty1fZmtDaTQ9",
                        Description = "This is cache description",
                        CacheType = CacheType.Earth,
                        AddressId = 5,
                        Address = new Address()
                        {
                            Street = "Alexander Platze 1",
                            City = "Berlin",
                            Country = "Germany"
                        }
                    }
                });
                context.SaveChanges();
            }
        }
    }
}