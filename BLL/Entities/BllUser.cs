using System;
using System.Collections.Generic;

namespace BLL.Entities
{
    [Serializable]
    public class BllUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public byte[] Photo { get; set; }
        public IEnumerable<BllReward> Rewards { get; set; }
    }
}
