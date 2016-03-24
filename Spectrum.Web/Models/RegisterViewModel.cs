using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Web.Mvc;
using Spectrum.Data.Core.Models;

namespace Spectrum.Web.Models
{
    public class RegisterViewModel
    {

        #region Basic User Info

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

        [Display(Name = "OrganizationTypeId")]
        public int OrganizationTypeId { get; set; }

        [Display(Name = "NewsletterSubscribe")]
        public bool NewsletterSubscribe { get; set; }

        #endregion

        #region User Profile Info

        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Nickname { get; set; }
        public string SecondaryEmail { get; set; }
        public string SecondaryPhone { get; set; }
        public string TimeZone { get; set; }
        public bool? DstAdjust { get; set; }
        public string Language { get; set; }

        #endregion

        #region Organization Profile

        public string PrimaryContact { get; set; }
        public string OrgPhone { get; set; }
        public string Fax { get; set; }
        public string OrgEmail { get; set; }
        public string Country { get; set; }
        public string County { get; set; }
        public string OrgTimeZone { get; set; }
        public bool? OrgDstAdjust { get; set; }
        public string OrgLanguage { get; set; }

        #endregion
        public OrganizationTypeViewModel OrganizationType { get; set; }
        public List<OrganizationTypeViewModel> OrganizationTypes { get; set; }
    }
}