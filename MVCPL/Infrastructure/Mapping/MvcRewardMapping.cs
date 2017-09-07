using BLL.Entities;
using MVCPL.Models;
using MVCPL.Util.Extensions;
using MVCPL.Util.HelperModels;

namespace MVCPL.Infrastructure.Mapping
{
    public static class MvcRewardMapping
    {
        //TODO: Null reference
        public static RewardViewModel ToViewModel(this BllReward bllReward)
        {
            return new RewardViewModel
            {
                Id = bllReward.Id,
                Title = bllReward.Title,
                Description = bllReward.Description,
                Image = MemoryPostedFile.CreateInstance(bllReward.Image),
                IsSelected = (bllReward.User != null),
                User = bllReward.User.ToViewModel(),
            };
        }

        //TODO: Null reference
        public static BllReward ToBllModel(this RewardViewModel reward)
        {
            return new BllReward
            {
                Id = reward.Id,
                Title = reward.Title,
                Description = reward.Description,
                Image = reward.Image.ToBytes(),
                User = reward.User.ToBllModel(),
            };
        }
    }
}