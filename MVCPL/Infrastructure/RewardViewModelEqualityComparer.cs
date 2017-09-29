using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCPL.Models;

namespace MVCPL.Infrastructure
{
    public class RewardViewModelEqualityComparer : IEqualityComparer<RewardViewModel>
    {
        public bool Equals(RewardViewModel x, RewardViewModel y)
        {
            return x?.Id == y?.Id;
        }

        public int GetHashCode(RewardViewModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}