using CacheAspect;
using CacheAspect.Supporting;

namespace TestCache
{
    public class UserAddRepository
    {
        public IUserDal Dal { get; set; }

        [Cache.TriggerInvalidation("Users", CacheSettings.EntireGroup)]
        public void AddUser(User user)
        {
            Dal.AddUser(user);
        }
    }
}