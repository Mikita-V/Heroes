using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Entities;
using BLL.Interface;
using DAL.DTO;
using DAL.Interface;

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
            var users =
                uow.Users.GetAll()
                    .Select(u => new BllUser {Id = u.Id, Name = u.Name, BirthDate = u.BirthDate, Photo = u.Photo})
                    .ToList();
            return users;
        }

        public BllUser GetUserById(int id)
        {
            var user = uow.Users.GetById(id);
            return new BllUser {Id = user.Id, Name = user.Name, BirthDate = user.BirthDate, Photo = user.Photo};
        }

        public void CreateUser(BllUser user)
        {
            var dalUser = new DalUser {Id = user.Id, Name = user.Name, BirthDate = user.BirthDate, Photo = user.Photo };
            uow.Users.Create(dalUser);
            uow.Commit();
        }

        public void UpdateUser(BllUser user)
        {
            var dalUser = new DalUser { Id = user.Id, Name = user.Name, BirthDate = user.BirthDate, Photo = user.Photo };
            uow.Users.Update(dalUser);
            uow.Commit();
        }

        public void DeleteUser(BllUser user)
        {
            var dalUser = new DalUser { Id = user.Id, Name = user.Name, BirthDate = user.BirthDate, Photo = user.Photo };
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
