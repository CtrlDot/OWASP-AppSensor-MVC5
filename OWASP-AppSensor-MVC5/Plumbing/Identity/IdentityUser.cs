using System;
using Microsoft.AspNet.Identity;

namespace OWASP_AppSensor_MVC5.Plumbing.Identity
{
    public class IdentityUser : IUser
    {
        public IdentityUser(string id, string userName, string hashedPassword)
        {
            Id = id;
            UserName = userName;
            this.hashedPassword = hashedPassword;
        }

        public IdentityUser(string userName)
        {
            UserName = userName;
        }

        public string Id { get; private set; }
        public string UserName { get; set; }
        public string hashedPassword { get; set; }
    }
}