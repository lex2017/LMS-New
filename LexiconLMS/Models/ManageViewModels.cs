﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace LexiconLMS.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [StringLength(100, ErrorMessage = "Fältet {0} måste innehålla minst {2} tecken.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        //[Display(Name = "New password")]
        [Display(Name = "Nytt lösenord")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        //[Display(Name = "Confirm new password")]
        [Display(Name = "Konfirmera nytt lösenord")]
        //[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [Compare("NewPassword", ErrorMessage = "Nytt lösenord och konfirmerat lösenord är olika.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        //[Display(Name = "Current password")]
        [Display(Name = "Nuvarande lösenord")]
        public string OldPassword { get; set; }

        [Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [StringLength(100, ErrorMessage = "Fältet {0} måste innehålla minst {2} tecken.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        //[Display(Name = "Confirm new password")]
        [Display(Name = "Konfirmera nytt lösenord")]
        //[Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        [Compare("NewPassword", ErrorMessage = "Nytt lösenord och konfirmerat lösenord är olika.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Telefonnummer")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        //[Display(Name = "Code")]
        [Display(Name = "Roktnummer")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Telefonnummer")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }
}