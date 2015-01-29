using DeaneryASP.Models.Storage.Custom;
using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeaneryASP.Models.Storage
{
    public interface IAdminStorage
    {
        List<Semesters> GetSemestrList();
        List<Custom.SubjectItem> GetRealisationItemList(int semestrID);
        bool AddRealisation(string ver, int semestrID, int subjectID, int? userID);
        bool DeleteRealisation(int realisationID);
        bool SaveRealisation(int realisationID, string ver, int semestrID, int SubjectID, int? UserID, byte[] timeStamp);
        List<Users> GetProfessors();
        List<Subjects> GetSubjects();
        Realisations GetRealisation(int realisationID);
        List<StudentItem> GetRegistredStudents(int RealisationID);
        List<StudentItem> GetStudents(int realisationID);
        bool AddRegistration(int studentID, int realisationID);
        bool DeleteRegistration(int realisationID, int studentID);

    }
}
