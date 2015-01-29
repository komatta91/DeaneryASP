using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models
{
    public class ProfessorGradeListViewModel
    {
        public int? RealisationID { get; set; }
        public int? EGradeID { get; set; }
        public int? DGradeID { get; set; }
        public int? WGradeID { get; set; }
        public List<Grades> Grades { get; set; }
        public String AddNew { get; set; }
    }
}