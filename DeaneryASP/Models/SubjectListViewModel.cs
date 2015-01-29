using DeaneryASP.Models.Storage.Custom;
using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models
{
    public class SubjectListViewModel
    {
        public int? SemestrID { get; set; }
        public int? ERealisationID { get; set; }
        public int? DRealisationID { get; set; }
        public int? ARealisationID { get; set; }
        public String AddNew { get; set; }
        public List<SubjectItem> Subjects { get; set; }
    }
}