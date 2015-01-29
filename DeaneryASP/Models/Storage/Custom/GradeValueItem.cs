using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models.Storage.Custom
{
    public class GradeValueItem
    {
        public GradeValues GradeValues { get; set; }
        public string IndexNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int RegistrationID { get; set; }


    }
}