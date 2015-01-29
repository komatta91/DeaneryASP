using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required]
        [StringLength(50)]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

    }
}