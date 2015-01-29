using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeaneryASP.Models.Storage.Entity;
using System.ComponentModel.DataAnnotations;

namespace DeaneryASP.Models
{
    public class AdminViewModel
    {
        [Display(Name = "Wybierz semestr")]
        public List<Semesters> Semestrs { get; set; }
        public int? SemestrID { get; set; }

    }
}