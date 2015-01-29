using DeaneryASP.Models.Storage.Custom;
using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeaneryASP.Models.Storage
{
    public interface IProfessorStorage
    {
        int GetUserID(String uid);
        List<Semesters> GetSemestrList(int userId);
        List<RealisationsItem> GetSubjectList(int semestrID, int userId);
        List<Grades> GetGrades(int realisationID);
        bool DeleteGrade(int GradeID);
        bool AddGrade(string name, string maxValue, int realisationID);
        bool EditGrade(int gradeID, string name, string maxValue, byte[] timeStamp);
        List<GradeValueItem> GetGradeValueList(int gradeID);
        bool AddGradeValue(int gradeID, int registrationID, string date, string value);
        bool DeleteGradeValue(int gradeValueID);
        List<StudentItem> GetErroredStudent(int p, string value);

        List<string> GetPossibleValue(int gtadeID);
    }
}
