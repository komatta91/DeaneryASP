using DeaneryASP.Models.Storage.Custom;
using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace DeaneryASP.Models.Storage.Impl
{
    public class AdminStorage : IAdminStorage
    {
        public List<Entity.Semesters> GetSemestrList()
        {
            using (var db = new StorageContext())
            {
                return db.Semesters.OrderBy(n => n.Name).ToList();
            }
        }


        public List<SubjectItem> GetRealisationItemList(int semestrID)
        {
            using (var db = new StorageContext())
            {
                var list = (from realisation in db.Realisations
                            where realisation.SemesterID == semestrID
                            select new { realisation, realisation.Subjects, realisation.Users });
                List<SubjectItem> ans = new List<SubjectItem>();
                foreach (var elem in list)
                {
                    ans.Add(new SubjectItem()
                    {
                        RealisationID = elem.realisation.RealisationID,
                        Name = elem.Subjects.Name,
                        Version = elem.realisation.Ver,
                        FirstName = (elem.Users != null ? elem.Users.FirstName : null),
                        LastName = (elem.Users != null ? elem.Users.LastName : null)
                    });
                }
                return ans;
            }
        }

        public bool AddRealisation(string ver, int semestrID, int subjectID, int? userID)
        {
            Random rand = new Random();
            int id = rand.Next(899999) + 100000;
            using (var db = new StorageContext())
            {
                Semesters semestr = db.Semesters.Find(semestrID);
                Subjects subject = db.Subjects.Find(subjectID);
                Users user = null;
                if (userID.HasValue)
                {
                    user = db.Users.Find(userID.Value);
                }
                Realisations realisation = new Realisations()
                {
                    RealisationID = id,
                    Semesters = semestr,
                    Subjects = subject,
                    Users = user,
                    Ver = ver
                };
                db.Realisations.Add(realisation);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
        }

        public bool DeleteRealisation(int realisationID)
        {
            using (var db = new StorageContext())
            {
                var realisation = db.Realisations.Find(realisationID);
                db.Realisations.Remove(realisation);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }

        public bool SaveRealisation(int realisationID, string ver, int semestrID, int subjectID, int? userID, byte[] timeStamp)
        {
            using (var db = new StorageContext())
            {
                Realisations original = db.Realisations.Find(realisationID);
                if (original != null)
                {
                    if (!original.TimeStamp.SequenceEqual(timeStamp))
                    {
                        return false;
                    }
                    Semesters semestr = db.Semesters.Find(semestrID);
                    Subjects subject = db.Subjects.Find(subjectID);
                    Users user = null;
                    if (userID.HasValue)
                    {
                        user = db.Users.Find(userID.Value);

                        original.Users = user;
                    }
                    else
                    {

                        original.UserID = null;
                    }
                    original.Ver = ver;
                    original.Semesters = semestr;
                    original.Subjects = subject;
                    
                    
                    db.Entry(original).State = System.Data.Entity.EntityState.Modified;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                    return true;
                }
                return false;
                
            }
        }


        public List<Users> GetProfessors()
        {
            using (var db = new StorageContext())
            {
                return (from user in db.Users
                        where user.UID != "Admin" && user.Students == null
                        select user).OrderBy(m => m.LastName).ThenBy(m => m.FirstName).ToList();
            }
        }

        public List<Subjects> GetSubjects()
        {
            using (var db = new StorageContext())
            {
                return db.Subjects.ToList();
            }
        }


        public Realisations GetRealisation(int realisationID)
        {
            using (var db = new StorageContext())
            {
                return db.Realisations.Find(realisationID);
            }
        }


        public List<StudentItem> GetRegistredStudents(int realisationID)
        {

            using (var db = new StorageContext())
            {
                return (from registration in db.Registrations
                        where registration.RealisationID == realisationID
                        select new StudentItem(){ IndexNo = registration.Students.IndexNo, StudentID = registration.StudentID,
                        FirstName = registration.Students.Users.FirstName, LastName = registration.Students.Users.LastName }).OrderBy(m => m.IndexNo).ToList();
            }
        }

        public List<StudentItem> GetStudents(int realisationID)
        {
            using (var db = new StorageContext())
            {
               

                return (from student in db.Students
                        where (db.Registrations.Where(m => m.StudentID == student.StudentID && m.RealisationID == realisationID).Count() == 0)
                        select new StudentItem(){ IndexNo =student.IndexNo, StudentID = student.StudentID,
                        FirstName = student.Users.FirstName, LastName = student.Users.LastName }).OrderBy(m => m.IndexNo).ToList();
            }
        }


        public bool AddRegistration(int studentID, int realisationID)
        {
            Random rand = new Random();
            int id = rand.Next(899999) + 100000;
            using (var db = new StorageContext())
            {
                Students s = db.Students.Find(studentID);
                Realisations r = db.Realisations.Find(realisationID);
                Registrations reg = new Registrations();
                reg.RegistrationID = id;
                reg.Students = s;
                reg.Realisations = r;
                db.Registrations.Add(reg);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }

        public bool DeleteRegistration(int realisationID, int studentID)
        {
            using (var db = new StorageContext())
            {
                var registratiion = (from registration in db.Registrations
                                     where registration.StudentID == studentID && registration.RealisationID == realisationID
                                     select registration).First();
                db.Registrations.Remove(registratiion);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    return false;
                }

                return true;
            }
        }
    }
}