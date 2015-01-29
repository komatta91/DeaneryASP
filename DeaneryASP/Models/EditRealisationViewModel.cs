using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models
{
    public class EditRealisationViewModel
    {
        public int? RealisationID { get; set; }

        [Display(Name = "Przedmiot")]
        public int? SubjectID { get; set; }

        public int? SemestrID { get; set; }

        [Display(Name = "Prowadzący")]
        public int? UserID { get; set; }

        public byte[] TimeStamp { get; set; }

        [StringLength(1)]
        [Display(Name = "Wersja")]
        public String Ver { get; set; }

        public String Action { get; set; }

        public List<Users> Professors { get; set; }

        public List<Subjects> Subjects { get; set; }
    }
}