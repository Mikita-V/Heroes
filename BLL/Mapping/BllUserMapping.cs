using BLL.Entities;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapping
{
    public static class BllUserMapping
    {
        public static BllUser ToBllModel(this DalUser dalUser, List<BllReward> rewards = null)
        {
            if (dalUser == null)
            {
                return null;
            }

            return new BllUser
            {
                Id = dalUser.Id,
                Name = dalUser.Name,
                BirthDate = dalUser.BirthDate,
                Photo = dalUser.Photo,
                Rewards = rewards ?? dalUser.Rewards
                    ?.Select(_ => _.ToBllModel())
                    ?.ToList()
            };
        }

        public static DalUser ToDalModel(this BllUser user, List<DalReward> rewards = null)
        {
            if (user == null)
            {
                return null;
            }

            return new DalUser
            {
                Id = user.Id,
                Name = user.Name,
                BirthDate = user.BirthDate,
                Photo = user.Photo,
                Rewards = rewards ?? user.Rewards
                    ?.Select(_ => _.ToDalModel())
                    ?.ToList()
            };
        }
    }
}
