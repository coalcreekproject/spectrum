using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Spectrum.Data.Core.Models;

namespace Spectrum.Web.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Confirm Email")]
        public string ConfirmEmail { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "OrganizationName")]
        public string OrganizationName { get; set; }

        [Display(Name = "OrganizationType")]
        public int OrganizationType { get; set; }

        [Display(Name = "NewsletterSubscribe")]
        public bool NewsletterSubscribe { get; set; }

        //public IEnumerable<SelectListItem> OrganizationTypes { get; set; }
        public List<OrganizationType> OrganizationTypes { get; set; }
    }
}