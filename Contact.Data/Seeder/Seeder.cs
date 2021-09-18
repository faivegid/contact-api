using Contact.Models.DomainModels;
using Contact.Models.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Data
{
    public class Seeder 
    {
        private readonly DataContext _context;
        private readonly UserManager<UserContact> _userManager;
        private readonly ILogger<Seeder> _logger;

        public Seeder(DataContext context,UserManager<UserContact> userManager, ILogger<Seeder> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }
        public async Task SeedAdminAsync()
        {
            try
            {
                await _context.Database.EnsureCreatedAsync();
                if (!_userManager.Users.Any())
                {
                    UserContact user = new UserContact
                    {
                        FirstName = "Gideon",
                        LastName = "Faive",
                        Email = "faivegid@gmail.com",
                        Gender = Gender.Male,
                        CreatedAt = DateTime.Now,
                        UserName = "faivegid@gmail.com"
                    };
                    await _userManager.CreateAsync(user, "admin@Gid123");
                    await _userManager.AddToRoleAsync(user, UserRole.Admin.ToString());                   
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in seeding Message: { ex.Message}");
            }
        }
    }
}
