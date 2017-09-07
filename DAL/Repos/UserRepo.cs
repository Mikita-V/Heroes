using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DTO;
using DAL.Interface;
using ORM.Entities;

namespace DAL.Repos
{
    //TODO: Refactor
    public class UserRepo : IRepository<DalUser>
    {
        private readonly DbContext _context;

        public UserRepo(DbContext context)
        {
            this._context = context;
        }

        public IEnumerable<DalUser> GetAll()
        {
            var users = _context.Set<User>()
                .Select(user => new DalUser
                {
                    Id = user.Id,
                    Name = user.Name,
                    BirthDate = user.BirthDate,
                    Photo = user.Photo,
                    Rewards = user.Rewards
                        .Select(_ => new DalReward
                        {
                            Id = _.Id,
                            Title = _.Title,
                            Description = _.Description,
                            Image = _.Image,
                            //User = _.User
                        })
                        .ToList()
                })
                .ToList();

            return users;
        }

        public DalUser GetById(int id)
        {
            var user = _context.Set<User>().SingleOrDefault(r => r.Id == id);

            return new DalUser
            {
                Id = user.Id,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Photo = user.Photo,
                Rewards = user.Rewards.Select(_ => new DalReward
                {
                    Id = _.Id,
                    Title = _.Title,
                    Description = _.Description,
                    Image = _.Image,
                    //User = _.User
                }).ToList()
            };
        }

        public void Create(DalUser entity)
        {
            var ids = entity.Rewards?.Select(_ => _.Id) ?? new int[] { };
            var user = new User
            {
                Id = entity.Id,
                Name = entity.Name,
                BirthDate = entity.BirthDate,
                Photo = entity.Photo,
                Rewards = _context.Set<Reward>()
                    .Where(r => ids.Contains(r.Id))
                    .ToList()
            };
            _context.Entry(user).State = EntityState.Added;
        }

        public void Update(DalUser entity)
        {
            var oldRewards = _context.Set<Reward>()
                .Where(_ => _.User.Id == entity.Id)
                .ToList();
            var ids = entity.Rewards.Select(_ => _.Id);
            var newRewards = _context.Set<Reward>()
                .Where(r => ids.Contains(r.Id))
                .ToList();
            var addedRewards = newRewards.Except(oldRewards).ToList();
            var removedRewards = oldRewards.Except(newRewards).ToList();
            var currentUser = _context.Set<User>().Single(_ => _.Id == entity.Id);
            foreach (var reward in addedRewards)
            {
                reward.User = currentUser;
                //context.Entry(reward).State = EntityState.Modified;
            }

            foreach (var reward in removedRewards)
            {
                reward.User = null;
                _context.Entry(reward).State = EntityState.Modified;
            }


            var user = new User
            {
                Id = entity.Id,
                Name = entity.Name,
                BirthDate = entity.BirthDate,
                Photo = entity.Photo ?? currentUser.Photo,
                Rewards = newRewards
            };

            _context.Entry(currentUser).CurrentValues.SetValues(user);
            //context.Entry(user).State = EntityState.Modified;
        }

        public void Delete(DalUser entity)
        {
            var user = _context.Set<User>().Single(u => u.Id == entity.Id);
            _context.Entry(user).State = EntityState.Deleted;
        }
    }
}
