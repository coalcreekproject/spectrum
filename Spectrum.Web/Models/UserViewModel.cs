﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Spectrum.Web.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            UserProfiles = new List<UserProfileViewModel>();
            Roles = new List<RoleViewModel>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public ICollection<RoleViewModel> Roles { get; set; }
        public ICollection<UserProfileViewModel> UserProfiles { get; set; }
    }
}