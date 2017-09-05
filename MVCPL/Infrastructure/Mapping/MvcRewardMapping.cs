using BLL.Entities;
using MVCPL.Models;
using MVCPL.Util.Helpers;
using MVCPL.Util.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCPL.Infrastructure.Mapping
{
    public static class MvcRewardMapping
    {
        public static RewardViewModel ToViewModel(this BllReward bllReward)
        {
            return new RewardViewModel
            {
                Id = bllReward.Id,
                Title = bllReward.Title,
                Description = bllReward.Description,
                Image = (HttpPostedFileBase)MemoryPostedFile.CreateInstance(bllReward.Image),
                IsSelected = (bllReward.User != null),
                User = bllReward.User.ToViewModel(),
            };
        }

        public static BllReward ToBllModel(this RewardViewModel reward)
        {
            return new BllReward
            {
                Id = reward.Id,
                Title = reward.Title,
                Description = reward.Description,
                Image = ImageHelper.MapPicture(reward.Image),
                User = reward.User.ToBllModel(),
            };
        }
    }
}