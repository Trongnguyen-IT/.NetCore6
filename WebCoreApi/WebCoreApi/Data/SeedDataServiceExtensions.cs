using WebCoreApi.Services;

namespace WebCoreApi.Data
{
    public static class SeedDataServiceExtensions
    {
        public static async Task EnsureSeedData(this WebApplicationBuilder builder, string[] args)
        {
            builder.Host.ConfigureLogging(logging =>
            {
                logging.AddConsole();
            });

            var host = builder.Build();
            var services = host.Services.CreateScope();
            var service = services.ServiceProvider.GetRequiredService<SeedDataService>();

            await service.Initialize();
        }
    }
}
