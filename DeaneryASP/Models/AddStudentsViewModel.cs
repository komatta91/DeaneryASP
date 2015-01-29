using DeaneryASP.Models.Storage.Custom;
using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models
{
    public class AddStudentsViewModel
    {
        public int? SemestrID { get; set; }
        public int? RealisationID { get; set; }
        public int? StudentID { get; set; }
        public int? DStudentID { get; set; }
        public String Action { get; set; }
        public List<StudentItem> RegistredStudents { get; set; }
        public List<StudentItem> Students { get; set; }

    }
}