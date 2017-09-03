using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace CustomIdentity.Identity
{
    public class DataService
    {
        private List<ApplicationUser> _users = new List<ApplicationUser>();

        public ApplicationUser GetUser(Func<ApplicationUser, bool> predicate)
        {
            return _users.FirstOrDefault(predicate);
        }

        public ApplicationUser AddUser(ApplicationUser user)
        {
            if (string.IsNullOrWhiteSpace(user.Id))
            {
                user.Id = Guid.NewGuid().ToString();
            }

            user.Claims = new List<Claim>
            {
                new Claim("UserHeroType", "superman"),
                new Claim("UserHeroBirthdate", DateTime.Now.ToString("dd-MM-yyyy"))
            };

            _users.Add(user);

            return user;
        }

        public ApplicationUser UpdateUser(ApplicationUser user)
        {
            var targetUser = _users.FirstOrDefault(m => m.Id == user.Id);

            targetUser.UserName = user.UserName;
            targetUser.City = user.City;
            targetUser.Email = user.Email;

            return user;
        }

        public void DeleteUser(ApplicationUser user)
        {
            _users = _users.Where(m => m.Id != user.Id).ToList();
        }

        public void AddClaim(ApplicationUser user, Claim claim)
        {
            if (user.Claims == null) user.Claims = new List<Claim>();

            user.Claims.Add(claim);
        }

        public void AddClaims(ApplicationUser user, List<Claim> claims)
        {
            if (user.Claims == null) user.Claims = new List<Claim>();

            user.Claims.AddRange(claims);
        }

        public void RemoveClaims(ApplicationUser user, List<Claim> claims)
        {
            if (user.Claims == null) user.Claims = new List<Claim>();

            var types = claims.Select(m => m.Type).ToList();

            user.Claims.RemoveAll(m => types.Contains(m.Type));
        }

        public List<ApplicationUser> GetUsers()
        {
            return _users;
        }
    }
}
