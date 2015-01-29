using DeaneryASP.Models.Storage.Custom;
using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models
{
    public class EditGradeViewModel
    {
        public int? RealisationID { get; set; }
        public int? GradeID { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Nazwa")]
        public String Name { get; set; }
        
        [StringLength(80)]
        [Display(Name = "Maksymalna wartość")]
        public String MaxValue { get; set; }
        public String Action { get; set; }
        public byte[] TimeStamp { get; set; }
        public Boolean Error { get; set; }
        public List<StudentItem> ErroredStudents { get; set; }
    }
}