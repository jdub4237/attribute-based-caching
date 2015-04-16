using System.Collections.Generic;

namespace TestCache
{
    public interface IUserDal
    {
        void AddUser(User u);
        void DeleteUser(User u);
        List<User> GetAllUsers();
        User GetUserById(int id);

        User GetUserBySuperId(SuperId superId);
    }
}
