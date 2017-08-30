using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Entities;
using BLL.Interface;
using DAL.DTO;
using DAL.Interface;

namespace BLL.Services
{
    public class RewardService : IRewardService
    {
        private readonly IUnitOfWork uow;

        public RewardService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IEnumerable<BllReward> GetAllRewards()
        {
            var rewards =
                uow.Rewards.GetAll()
                    .Select(r => new BllReward {Id = r.Id, Description = r.Description, Title = r.Title, Image = r.Image})
                    .ToList();

            return rewards;
        }

        public BllReward GetRewardById(int id)
        {
            //Possible nullreferenceexception
            var reward = uow.Rewards.GetById(id);
            return new BllReward {Id = reward.Id, Description = reward.Description, Title = reward.Title, Image = reward.Image};
        }

        public void CreateReward(BllReward reward)
        {
            var dalReward = new DalReward {Id = reward.Id, Description = reward.Description, Title = reward.Title, Image = reward.Image};
            uow.Rewards.Create(dalReward);
            uow.Commit();
        }

        public void UpdateReward(BllReward reward)
        {
            var dalReward = new DalReward { Id = reward.Id, Description = reward.Description, Title = reward.Title, Image = reward.Image };
            uow.Rewards.Update(dalReward);
            uow.Commit();
        }

        public void DeleteReward(BllReward reward)
        {
            var dalReward = new DalReward { Id = reward.Id, Description = reward.Description, Title = reward.Title, Image = reward.Image };
            uow.Rewards.Delete(dalReward);
            uow.Commit();
        }
    }
}
