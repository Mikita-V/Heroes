using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Entities;
using BLL.Interface;
using DAL.DTO;
using DAL.Interface;
using BLL.Mapping;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork uow;

        public UserService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IEnumerable<BllUser> GetAllUsers()
        {
            var users = uow.Users
                .GetAll()
                .Select(_ => _.ToBllModel())
                .ToList();

            return users;
        }

        public BllUser GetUserById(int id)
        {
            var user = uow.Users
                .GetById(id)
                .ToBllModel();

            return user;
        }

        public void CreateUser(BllUser user)
        {
            var dalUser = user.ToDalModel();
            uow.Users.Create(dalUser);
            uow.Commit();
        }

        public void UpdateUser(BllUser user)
        {
            var dalUser = user.ToDalModel();
            uow.Users.Update(dalUser);
            uow.Commit();
        }

        //change user to id
        public void DeleteUser(BllUser user)
        {
            var dalUser = user.ToDalModel();
            uow.Users.Delete(dalUser);
            uow.Commit();
        }

        public byte[] UsersToByteArray()
        {
            var users = GetAllUsers();
            var stringBulder = new StringBuilder();
            stringBulder.Append("USERS LIST:");
            stringBulder.Append(Environment.NewLine);
            stringBulder.Append("NAME\t\tBIRTHDATE\n");
            stringBulder.Append(Environment.NewLine);
            foreach (var user in users)
            {
                stringBulder.Append($"{user.Name}\t\t{user.BirthDate.ToShortDateString()}");
                stringBulder.Append(Environment.NewLine);
            }

            var bytes = Encoding.ASCII.GetBytes(stringBulder.ToString());

            return bytes;
        }
    }
}
