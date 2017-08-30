using System;
using System.Collections.Generic;

namespace MVCPL.Models
{
    public class UserViewModel
    {
        public int Id  { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age
        {
            get
            {
                var age = DateTime.Today.Year - BirthDate.Year;
                return (BirthDate > DateTime.Today.AddYears(-age)) ? age : --age;
            }
        }
        public byte[] Photo { get; set; }
        public IEnumerable<RewardViewModel> Rewards { get; set; }
    }
}