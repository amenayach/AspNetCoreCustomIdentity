using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            _users.Add(user);

            return user;
        }

        public ApplicationUser UpdateUser(ApplicationUser user)
        {
            var targetUser = _users.FirstOrDefault(m => m.Id == user.Id);

            targetUser.UserName = user.UserName;
            targetUser.City= user.City;
            targetUser.Email= user.Email;

            return user;
        }

        public void DeleteUser(ApplicationUser user)
        {
            _users = _users.Where(m => m.Id != user.Id).ToList();
        }
    }
}
