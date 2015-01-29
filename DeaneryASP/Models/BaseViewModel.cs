using DeaneryASP.Models.Storage.Custom;
using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models
{
    [Serializable]
    public class BaseViewModel
    {
        public BaseViewModel()
        {
            //Semestrs = new List<Semesters>();
            //SubjectsList = new List<Subjects>();
            Dictionary = new Dictionary<Semesters, List<RealisationsItem>>();

        }
        /*
        [Display(Name = "Wybierz semestr: ")]
        public List<Semesters> Semestrs { get; set; }
        [Display(Name = "Wybierz przedmiot: ")]
        public List<Subjects> SubjectsList { get; set; }
        public int? Semestr { get; set; }
         */

        [Display(Name = "Wybierz przedmiot: ")]
        public Dictionary<Semesters, List<RealisationsItem>> Dictionary { get; set; }
        public int? RealisationsID { get; set; }
    }
}