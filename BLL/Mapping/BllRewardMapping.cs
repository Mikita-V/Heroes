using BLL.Entities;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public static class BllRewardMapping
    {
        public static BllReward ToBllModel(this DalReward dalReward)
        {
            return new BllReward
            {
                Id = dalReward.Id,
                Title = dalReward.Title,
                Description = dalReward.Description,
                Image = dalReward.Image,
                User = dalReward.User.ToBllModel()
            };
        }

        public static DalReward ToDalModel(this BllReward reward)
        {
            return new DalReward
            {
                Id = reward.Id,
                Title = reward.Title,
                Description = reward.Description,
                Image = reward.Image,
                User = reward.User.ToDalModel()
            };
        }
    }
}
