using DeaneryASP.Models.Storage.Custom;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models
{
    public class MarkGradesViewModel
    { 
        public int? RealisationID { get; set; }
        public int? GradeID { get; set; }
        public List<GradeValueItem> GradeValues { get; set; }
        public int? RegistrationID { get; set; }
        public int? DelValID { get; set; }

        [DataType(DataType.DateTime)]
        public String Date { get; set; }
        public String Grade { get; set; }
        public String Action { get; set; }
        public List<string> PosibleValue { get; set; }
    }
}