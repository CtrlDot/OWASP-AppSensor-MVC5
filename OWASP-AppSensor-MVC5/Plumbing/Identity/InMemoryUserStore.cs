using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using OWASP_AppSensor_MVC5.Models;

namespace OWASP_AppSensor_MVC5.Plumbing.Identity
{
    public class InMemoryUserStore : IUserStore<IdentityUser>, IUserPasswordStore<IdentityUser>
    {
        private IList<IdentityUser> userList = new List<IdentityUser>();

        public InMemoryUserStore()
        {
            
            userList.Add(new IdentityUser("1", "admin", Crypto.HashPassword("admin")));
        }

        public void Dispose()
        {
            
        }

        public Task CreateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityUser> FindByIdAsync(string userId)
        {
            return Task.FromResult(userList.FirstOrDefault(x => x.Id.Equals(userId)));
        }

        public Task<IdentityUser> FindByNameAsync(string userName)
        {
            return Task.FromResult(userList.FirstOrDefault(x => x.UserName.Equals(userName)));
        }

        public Task SetPasswordHashAsync(IdentityUser user, string passwordHash)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(IdentityUser user)
        {
            return Task.FromResult(userList.First(x => x.UserName.Equals(user.UserName)).hashedPassword);
        }

        public Task<bool> HasPasswordAsync(IdentityUser user)
        {
            var appUser = userList.FirstOrDefault(x => x.UserName.Equals(user.UserName));
            if (appUser == null)
            {
                return Task.FromResult(false);
            }

            return Task.FromResult(!String.IsNullOrEmpty(appUser.hashedPassword));
        }
    }
}