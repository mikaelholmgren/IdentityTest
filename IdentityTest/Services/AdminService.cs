using IdentityTest.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityTest.Services
{
    public class AdminService
    {
        private readonly UserManager<AppUser> _um;
        private readonly RoleManager<IdentityRole> _rm;
        private readonly IConfiguration _config;

        public AdminService(UserManager<AppUser> um, RoleManager<IdentityRole> rm, IConfiguration config)
        {
            this._um = um;
            this._rm = rm;
            this._config = config;
        }
        public async Task<bool> AdminExistsAsync()
        {
            var adminName = _config["AdminSettings:AdminUserName"];
            var roleExist = await _rm.RoleExistsAsync("Admin");
            if (!roleExist) return false;
            var adminUser = await _um.FindByNameAsync(adminName);
            if (adminUser == null) return false;

            return await _um.IsInRoleAsync(adminUser, "Admin");
        }
        public async Task CreateAdmin(string adminPass)
        {
            var adminName = _config["AdminSettings:AdminUserName"];
            var adminEmail = _config["AdminSettings:AdminEmail"];
            var user = new AppUser()
            {
                UserName = adminName,
                Email = adminEmail
            };
            await _um.CreateAsync(user, adminPass);
            var role = new IdentityRole() { Name = "Admin" };
            await _rm.CreateAsync(role);
            await _um.AddToRoleAsync(user, "Admin");

        }
    }
}
