using System.Collections.Generic;
using BLL.Entities;

namespace BLL.Interface
{
    public interface IRewardService
    {
        IEnumerable<BllReward> GetAllRewards();
        IEnumerable<BllReward> GetAllPossibleRewards(int userId);
        BllReward GetRewardById(int id);
        void CreateReward(BllReward reward);
        void UpdateReward(BllReward reward);
        void DeleteReward(BllReward reward);
    }
}
