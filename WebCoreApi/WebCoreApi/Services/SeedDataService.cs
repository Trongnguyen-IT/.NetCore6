using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebCoreApi.Models;

namespace WebCoreApi.Services
{
    public class SeedDataService
    {
        private readonly ILogger<SeedDataService> _logger;
        private readonly AppDbContext _dataContext;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public SeedDataService(ILogger<SeedDataService> logger,
           AppDbContext dataContext,
           RoleManager<AppRole> roleManager,
           UserManager<AppUser> userManager)
        {
            _logger = logger;
            _dataContext = dataContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task Initialize()
        {
            _logger.LogInformation("Start SeedDataService");
            await MigrationSchema();
            await EnsureRoles(_roleManager);
            await EnsureUsers(_userManager);
        }

        protected async Task MigrationSchema()
        {
            _logger.LogInformation("Migration");

            await _dataContext.Database.MigrateAsync();
        }

        private async Task EnsureRoles(RoleManager<AppRole> roleManager)
        {
            _logger.LogInformation("Start EnsureRoles");

            var roles = new[] { "Owner", "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" };

            foreach (string role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new AppRole
                    {
                        Name = role
                    });
                }
            }
        }

        private async Task EnsureUsers(UserManager<AppUser> userManager)
        {
            _logger.LogInformation("Start EnsureUsers");

            var seedUsers = new List<AppUser> {
                new AppUser
            {
                UserName = "admin",
                PasswordHash = "Admin@123",
                FirstName = "admin",
                LastName = "admin",
                PhoneNumber = "0989999999",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                PhoneNumberConfirmed = true
            }};

            foreach (var user in seedUsers)
            {
                var adminUser = await userManager.FindByEmailAsync(user.Email);
                if (adminUser == null)
                {
                    IdentityResult result = await userManager.CreateAsync(user, "Admin@123");
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, "Administrator");

                    }
                }
            }
        }
    }
}
