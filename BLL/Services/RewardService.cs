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
        private readonly IUnitOfWork _uow;

        public RewardService(IUnitOfWork uow)
        {
            this._uow = uow;
        }

        public IEnumerable<BllReward> GetAllRewards()
        {
            var rewards = _uow.Rewards
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
            var reward = _uow.Rewards
                .GetById(id)
                .ToBllModel();

            return reward;
        }

        public void CreateReward(BllReward reward)
        {
            var dalReward = reward.ToDalModel();
            _uow.Rewards.Create(dalReward);
            _uow.Commit();
        }

        public void UpdateReward(BllReward reward)
        {
            var dalReward = reward.ToDalModel();
            _uow.Rewards.Update(dalReward);
            _uow.Commit();
        }

        public void DeleteReward(BllReward reward)
        {
            var dalReward = reward.ToDalModel();
            _uow.Rewards.Delete(dalReward);
            _uow.Commit();
        }
    }
}
