﻿using System;
using System.Collections.Generic;
using DAL.Interface;

namespace DAL.DTO
{
    public class DalUser : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public byte[] Photo { get; set; }
        public IEnumerable<DalReward> Rewards { get; set; }
    }
}
