using PlatformService.Models;

namespace PlatformService.Data
{
    public static class PrepDb
    {
        public static void prepPopulation(IApplicationBuilder app)
        {
            using(var serviceScope = app.ApplicationServices.CreateScope()) 
            { 
              SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());
            }
        }

        private static void SeedData(AppDbContext context)
        {
            if (!context.Platforms.Any()) 
            {
                Console.WriteLine("--> Seeding Data");

                context.Platforms.AddRange(
                    new Platform() { Name = "Dot Net",Publisher="Microsoft", Cost = "Free"},
                    new Platform() { Name = "SQL Server Express",Publisher="Microsoft", Cost = "Free"},
                    new Platform() { Name = "Kubernetis",Publisher="Microsoft", Cost = "Free"}
                );

                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("--> Data already populated");
            }
        }
    }
}
