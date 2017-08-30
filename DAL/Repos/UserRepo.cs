using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Mappers;
using DAL.DTO;
using DAL.Interface;
using ORM.Entities;

namespace DAL.Repos
{
    public class UserRepo : IRepository<DalUser>
    {
        private readonly DbContext context;

        public UserRepo(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalUser> GetAll()
        {
            var users = context.Set<User>().Select(u => new DalUser { Id = u.Id, Name = u.Name, BirthDate = u.BirthDate, Photo = u.Photo}).ToList();
            return users;
        }

        public DalUser GetById(int id)
        {
            var user = context.Set<User>().SingleOrDefault(r => r.Id == id);
            return new DalUser {Id = user.Id, Name = user.Name, BirthDate = user.BirthDate, Photo = user.Photo };
        }

        public void Create(DalUser entity)
        {
            var user = new User { Id = entity.Id, Name = entity.Name, BirthDate = entity.BirthDate, Photo = entity.Photo };
            context.Entry(user).State = EntityState.Added;
        }

        public void Update(DalUser entity)
        {
            var user = new User { Id = entity.Id, Name = entity.Name, BirthDate = entity.BirthDate, Photo = entity.Photo };
            context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(DalUser entity)
        {
            var user = context.Set<User>().Single(u => u.Id == entity.Id);
            context.Entry(user).State = EntityState.Deleted;
        }
    }
}
