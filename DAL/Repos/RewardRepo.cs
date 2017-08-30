using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.DTO;
using DAL.Interface;
using ORM.Entities;

namespace DAL.Repos
{
    public class RewardRepo : IRepository<DalReward>
    {
        private readonly DbContext context;

        public RewardRepo(DbContext context)
        {
            this.context = context;
        }

        public IEnumerable<DalReward> GetAll()
        {
            var rewards = context.Set<Reward>().Select(r => new DalReward {Id = r.Id, Description = r.Description, Title = r.Title, Image = r.Image}).ToList();
            return rewards;
        }

        public DalReward GetById(int id)
        {
            var reward = context.Set<Reward>().SingleOrDefault(r => r.Id == id);
            return new DalReward {Id = reward.Id, Description = reward.Description, Title = reward.Title, Image = reward.Image};
        }

        public void Create(DalReward entity)
        {
            //null reference
            var reward = new Reward {Id = entity.Id, Description = entity.Description, Title = entity.Title, Image = entity.Image};
            context.Entry(reward).State = EntityState.Added;
        }

        public void Update(DalReward entity)
        {
            var reward = new Reward { Id = entity.Id, Description = entity.Description, Title = entity.Title, Image = entity.Image };
            context.Entry(reward).State = EntityState.Modified;
        }

        public void Delete(DalReward entity)
        {
            var reward = context.Set<Reward>().Single(u => u.Id == entity.Id);
            context.Entry(reward).State = EntityState.Deleted;
        }
    }
}
