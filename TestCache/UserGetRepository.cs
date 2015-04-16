using System.Collections.Generic;
using CacheAspect;

namespace TestCache
{
    public class UserGetRepository
    {
        public IUserDal Dal { get; set; }

        [Cache.Cacheable("Users")]
        public List<User> GetAllUsers()
        {
            return Dal.GetAllUsers();
        }

        [Cache.Cacheable("Users")]
        public User GetUserBySuperId(SuperId superId)
        {
            return Dal.GetUserBySuperId(superId);
        }
    }
}