using DeaneryASP.Models.Storage.Custom;
using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models.Storage.Impl
{
    public class StudentStorage : IStudentStorage
    {
        public int GetStudentID(string uid)
        {
            using (var db = new StorageContext())
            {
                int studentID = (from student in db.Users
                                 where student.UID == uid
                                 select student.UserID).Single();
                return studentID;
            }
        }


        public List<Semesters> GetSemestrList(int studentID)
        {
            using (var db = new StorageContext())
            {
                List<Semesters> ans = new List<Semesters>();
                var list2 = (from registration in db.Registrations
                            where registration.StudentID == studentID
                            select registration.Realisations.Semesters).OrderBy(semestr => semestr.Name).Distinct();
                foreach (var elem in list2)
                {
                    ans.Add(new Semesters { Active = elem.Active, Name = elem.Name, SemesterID = elem.SemesterID, TimeStamp = elem.TimeStamp });
                }
                return ans;
            }
        }

        public List<RealisationsItem> GetSubjectList(int semestrID, int studentID)
        {
            using (var db = new StorageContext())
            {
                List<RealisationsItem> ans = new List<RealisationsItem>();
                var list = (
                    from registration in db.Registrations
                    where registration.StudentID == studentID && registration.Realisations.Semesters.SemesterID == semestrID
                    select new { registration.Realisations.Subjects.Name, registration.Realisations.RealisationID }).Distinct();
                var list2 = list.OrderBy(name => name.Name);
                foreach (var elem in list2)
                {
                    ans.Add(new RealisationsItem { Name = elem.Name, RealisationsID = elem.RealisationID });
                }
                return ans;
            }
        }

        public List<GradeItem> GetGradeList(int realisationID, int studentID)
        {
            using (var db = new StorageContext())
            {
                List<GradeItem> ans = new List<GradeItem>();
                var list = (
                    from gardeValue in db.GradeValues
                    where gardeValue.Grades.RealisationID == realisationID && gardeValue.Registrations.StudentID == studentID
                    select new { gardeValue.Value, gardeValue.Grades.Name, gardeValue.Date});


                var list2 = list.OrderBy(name => name.Name);
                foreach (var elem in list2)
                {
                    ans.Add(new GradeItem { Name = elem.Name, Value = elem.Value, Date = elem.Date});
                }
                return ans;
            }
        }
    }
}