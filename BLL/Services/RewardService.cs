using System;
using System.Collections.Generic;
using System.Linq;
using BLL.Entities;
using BLL.Interface;
using DAL.Interface;
using BLL.Mapping;

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
            var rewards = uow.Rewards
                .GetAll()
                .Select(_ => _.ToBllModel())
                .ToList();

            return rewards;
        }

        public IEnumerable<BllReward> GetAllPossibleRewards(int userId)
        {
            return this.GetAllRewards()
                .Where(_ => _.User == null || _.User.Id == userId);
        }

        public BllReward GetRewardById(int id)
        {
            //Possible nullreferenceexception
            var reward = uow.Rewards
                .GetById(id)
                .ToBllModel();

            return reward;
        }

        public void CreateReward(BllReward reward)
        {
            var dalReward = reward.ToDalModel();
            uow.Rewards.Create(dalReward);
            uow.Commit();
        }

        public void UpdateReward(BllReward reward)
        {
            var dalReward = reward.ToDalModel();
            uow.Rewards.Update(dalReward);
            uow.Commit();
        }

        public void DeleteReward(BllReward reward)
        {
            var dalReward = reward.ToDalModel();
            uow.Rewards.Delete(dalReward);
            uow.Commit();
        }
    }
}
