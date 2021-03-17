using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sightseeing.Persistence;
using System.Linq;
using System.Net.Http;

namespace Sightseeing.Api.IntegrationTests.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public HttpClient GetClient()
        {
            return CreateClient();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SightseeingDbContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                services.AddDbContext<SightseeingDbContext>(options =>
                {
                    options.UseInMemoryDatabase("SightseeingDbContextInMemoryTest");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<SightseeingDbContext>();

                    if (context.Database.EnsureCreated())
                    {
                        Utilities.InitializeDbForTests(context);
                    }
                };
            });
        }
    }
}
