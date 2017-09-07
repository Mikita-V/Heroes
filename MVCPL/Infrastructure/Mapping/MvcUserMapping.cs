using System.Collections.Generic;
using System.Linq;
using BLL.Entities;
using MVCPL.Models;
using MVCPL.Util.Extensions;
using MVCPL.Util.HelperModels;

namespace MVCPL.Infrastructure.Mapping
{
    public static class MvcUserMapping
    {
        public static UserViewModel ToViewModel(this BllUser bllUser, List<RewardViewModel> rewards = null)
        {
            if (bllUser == null)
            {
                return null;
            }

            return new UserViewModel
            {
                Id = bllUser.Id,
                Name = bllUser.Name,
                BirthDate = bllUser.BirthDate,
                Photo = MemoryPostedFile.CreateInstance(bllUser.Photo),
                Rewards = rewards ?? bllUser.Rewards
                    ?.Select(_ => _.ToViewModel())
                    .ToList()
            };
        }

        public static BllUser ToBllModel(this UserViewModel user, List<BllReward> rewards = null)
        {
            if (user == null)
            {
                return null;
            }

            return new BllUser
            {
                Id = user.Id,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Photo = user.Photo.ToBytes(),
                Rewards = rewards ?? user.Rewards
                    ?.Select(_ => _.ToBllModel())
                    .ToList()
            };
        }
    }
}