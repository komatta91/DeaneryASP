using DeaneryASP.Models.Storage.Custom;
using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeaneryASP.Models.Storage
{
    public interface IStudentStorage
    {
        int GetStudentID(String uid);
        List<Semesters> GetSemestrList(int studentID);
        List<RealisationsItem> GetSubjectList(int semestrID, int studentID);
        List<GradeItem> GetGradeList(int realisationID, int studentID);

    }
}
