using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Entities;

namespace BLL.Interface
{
    public interface IRewardService
    {
        IEnumerable<BllReward> GetAllRewards();
        BllReward GetRewardById(int id);
        void CreateReward(BllReward reward);
        void UpdateReward(BllReward reward);
        void DeleteReward(BllReward reward);
    }
}
