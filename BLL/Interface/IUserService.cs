using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interface
{
    public interface IUserService
    {
        IEnumerable<BllUser> GetAllUsers();
        BllUser GetUserById(int id);
        IEnumerable<BllUser> GetUserByName(string name);
        void CreateUser(BllUser user);
        void UpdateUser(BllUser user);
        void DeleteUser(BllUser user);
        byte[] UsersToByteArray();
    }
}
