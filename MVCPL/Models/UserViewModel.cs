﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MVCPL.Models
{
    public class UserViewModel
    {
        [Required]
        public int Id  { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Maximum lenght is 50 charasters")]
        [DisplayName("Full Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Range(0, 150)]
        public int Age
        {
            get
            {
                var age = DateTime.Today.Year - BirthDate.Year;
                return (BirthDate > DateTime.Today.AddYears(-age)) ? age : --age;
            }
        }
        
        public HttpPostedFileBase Photo { get; set; }

        public List<RewardViewModel> Rewards { get; set; }
    }
}