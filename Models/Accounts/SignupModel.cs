﻿using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace WebAdvert.Web31.Models.Accounts
{
    public class SignupModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Email")]
        public string Email { get; set; }
        
        [Required][DataType(DataType.Password)]
        [StringLength(6,ErrorMessage ="Password must be at least 6 characters long")]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation don`t catch")]
        public string ConfirmPassword { get; set; }
    }
}
