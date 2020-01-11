using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebCore.Areas.Identity.Models
{
    public class TwilioCheckCodeResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
    }
    public class TwilioSendVerificationCodeResponse
    {
        public string Carrier { get; set; }
        public bool IsCellphone { get; set; }
        public string Message { get; set; }
        public string SecondsToExpire { get; set; }
        public Guid Uuid { get; set; }
        public bool Success { get; set; }
    }

    public class InputModel
    {
        [Required]
        [Display(Name = "Country dialing code")]
        public int DialingCode { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        public string VerificationCode { get; set; }
    }




}
