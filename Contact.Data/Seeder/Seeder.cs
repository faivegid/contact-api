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
        private readonly UserManager<Models.DomainModels.UserContact> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<Seeder> _logger;

        public Seeder(DataContext context,
            UserManager<Models.DomainModels.UserContact> userManager,
            RoleManager<IdentityRole> roleManager,
            ILogger<Seeder> logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        /// <summary>
        /// Seed a user with an admin role this will be the default admin that add other users as admin
        /// </summary>
        public async Task SeedAdminAsync()
        {
            try
            {
                await _context.Database.EnsureCreatedAsync();
                if (!_userManager.Users.Any())
                {
                    Models.DomainModels.UserContact user = new Models.DomainModels.UserContact
                    {
                        FirstName = "Gideon",
                        LastName = "Faive",
                        Email = "faivegid@gmail.com",
                        Gender = Gender.Male,
                        CreatedAt = DateTime.Now,
                        UserName = "faivegid@gmail.com"
                    };
                    var result = await _userManager.CreateAsync(user, "admin@Gid123");
                    if (result.Succeeded)
                        _logger.LogInformation($"Successfully added admin {user.FirstName}");
                    else
                        throw new ArgumentException(string.Join("\n", result.Errors.Select(x => x.Description)));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in seeding Message: { ex.Message}");
            }
        }


        /// <summary>
        /// Adds Roles to the Database all roles are contained in the Role Enum in the Models Class Library
        /// </summary>
        public async Task AddRolesAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    List<IdentityRole> roles = new List<IdentityRole> {
                    new IdentityRole(UserRole.Admin.ToString()),
                    new IdentityRole(UserRole.Customer.ToString())
                };

                    foreach (IdentityRole role in roles)
                    {
                        var result = await _roleManager.CreateAsync(role);
                        if (result.Succeeded)                        
                            _logger.LogInformation("Successfully added role: {0}", role.Name);
                    }
                    _logger.LogInformation("Added all Roles successfully");
                }
                else
                {
                    _logger.LogInformation("Roles not added.Roles already exist in the application");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

        }
    }
}
