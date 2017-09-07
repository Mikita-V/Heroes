using System;
using System.Collections.Generic;

namespace ORM.Entities
{
    public class User
    {
        public int Id  { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public byte[] Photo { get; set; }
        public virtual ICollection<Reward> Rewards { get; set; } = new HashSet<Reward>();
    }
}