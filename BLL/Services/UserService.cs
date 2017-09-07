using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Entities;
using BLL.Interface;
using BLL.Mapping;
using DAL.Interface;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public IEnumerable<BllUser> GetAllUsers()
        {
            var users = _uow.Users
                .GetAll()
                .Select(_ => _.ToBllModel())
                .ToList();

            return users;
        }

        public BllUser GetUserById(int id)
        {
            var user = _uow.Users
                .GetById(id)
                .ToBllModel();

            return user;
        }

        public void CreateUser(BllUser user)
        {
            var dalUser = user.ToDalModel();
            _uow.Users.Create(dalUser);
            _uow.Commit();
        }

        public void UpdateUser(BllUser user)
        {
            var dalUser = user.ToDalModel();
            _uow.Users.Update(dalUser);
            _uow.Commit();
        }

        public void DeleteUser(BllUser user)
        {
            var dalUser = user.ToDalModel();
            _uow.Users.Delete(dalUser);
            _uow.Commit();
        }

        //TODO: Separate class??
        public byte[] UsersToByteArray()
        {
            var users = GetAllUsers();
            var stringBulder = new StringBuilder();
            stringBulder.Append("***********USERS LIST***********");
            stringBulder.Append(Environment.NewLine);
            stringBulder.Append(Environment.NewLine);
            foreach (var user in users)
            {
                stringBulder.Append(Environment.NewLine);
                stringBulder.Append($"NAME:\t\t{user.Name}");
                stringBulder.Append(Environment.NewLine);
                stringBulder.Append($"BIRTH DATE:\t{user.BirthDate.ToShortDateString()}");
                stringBulder.Append(Environment.NewLine);
                stringBulder.Append("REWARDS:\t");
                if (user.Rewards != null && user.Rewards.Any())
                {
                    foreach (var reward in user.Rewards)
                    {
                        stringBulder.Append($"{reward.Title}; ");
                    }
                }
                else
                {
                    stringBulder.Append("No rewards.");
                }

                stringBulder.Append(Environment.NewLine);
                stringBulder.Append("------------------------------------");
            }

            stringBulder.Append(Environment.NewLine);

            var bytes = Encoding.ASCII.GetBytes(stringBulder.ToString());

            return bytes;
        }
    }
}
