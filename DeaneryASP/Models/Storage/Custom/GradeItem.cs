using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models.Storage.Custom
{
    public class GradeItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Date { get; set; }
    }
}