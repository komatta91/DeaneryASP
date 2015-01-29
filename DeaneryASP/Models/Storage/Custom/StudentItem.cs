using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models.Storage.Custom
{
    public class StudentItem
    {
        public int? StudentID { get; set; }
        public string IndexNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GradeValue { get; set; }
    }
}