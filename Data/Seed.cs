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
                        Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        Description = "This is the description of the first cinema",

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
                        Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        Description = "This is the description of the first cinema",
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
                        Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        Description = "This is the description of the first club",
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
                        Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        Description = "This is the description of the first race",
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
                        Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                        Description = "This is the description of the first race",
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