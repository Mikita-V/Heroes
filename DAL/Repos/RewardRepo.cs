using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DTO;
using DAL.Interface;
using ORM.Entities;

namespace DAL.Repos
{
    //TODO: Refactor
    public class RewardRepo : IRepository<DalReward>
    {
        private readonly DbContext _context;

        public RewardRepo(DbContext context)
        {
            this._context = context;
        }

        public IEnumerable<DalReward> GetAll()
        {
            var rewards = _context.Set<Reward>()
                .Select(_ => new DalReward
                {
                    Id = _.Id,
                    Title = _.Title,
                    Description = _.Description,
                    Image = _.Image,
                    //FUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU
                    User = _.User == null ? null : new DalUser { Id = _.User.Id }
                })
                .ToList();

            return rewards;
        }

        public DalReward GetById(int id)
        {
            var reward = _context.Set<Reward>().SingleOrDefault(r => r.Id == id);

            return new DalReward
            {
                Id = reward.Id,
                Title = reward.Title,
                Description = reward.Description,
                Image = reward.Image,
                //FUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUUU
                User = reward.User == null ? null : new DalUser { Id = reward.User.Id }
            };
        }

        public void Create(DalReward entity)
        {
            //null reference
            var reward = new Reward
            {
                Id = entity.Id,
                Description = entity.Description,
                Title = entity.Title,
                Image = entity.Image,
                //User = context.Set<User>().SingleOrDefault(_ => _.Id == entity.User.Id)
            };
            _context.Entry(reward).State = EntityState.Added;
        }

        public void Update(DalReward entity)
        {
            var currentReward = _context.Set<Reward>().SingleOrDefault(_ => _.Id == entity.Id);
            var reward = new Reward
            {
                Id = entity.Id,
                Description = entity.Description,
                Title = entity.Title,
                Image = entity.Image ?? currentReward.Image,
                //User = context.Set<User>().SingleOrDefault(_ => _.Id == entity.User.Id)
            };
            //context.Entry(reward).State = EntityState.Modified;
            _context.Entry(currentReward).CurrentValues.SetValues(reward);
        }

        public void Delete(DalReward entity)
        {
            var reward = _context.Set<Reward>().Single(u => u.Id == entity.Id);
            _context.Entry(reward).State = EntityState.Deleted;
        }
    }
}
