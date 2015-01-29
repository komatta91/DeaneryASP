using DeaneryASP.Models.Storage.Custom;
using DeaneryASP.Models.Storage.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Web;

namespace DeaneryASP.Models.Storage.Impl
{
    public class ProfessorStorage : IProfessorStorage
    {
        public int GetUserID(string uid)
        {
            using (var db = new StorageContext())
            {
                return (from user in db.Users
                        where user.UID == uid
                        select user.UserID).Single();

            }
        }

        public List<Semesters> GetSemestrList(int userId)
        {
            using (var db = new StorageContext())
            {
                List<Semesters> ans = new List<Semesters>();
                var list2 = (from realisations in db.Realisations
                             where realisations.UserID == userId
                             select realisations.Semesters).OrderBy(semestr => semestr.Name).Distinct();
                foreach (var elem in list2)
                {
                    ans.Add(elem);
                }
                return ans;
            }
        }

        public List<Custom.RealisationsItem> GetSubjectList(int semestrID, int userID)
        {
            using (var db = new StorageContext())
            {
                List<RealisationsItem> ans = new List<RealisationsItem>();
                var list = (
                    from realisations in db.Realisations
                    where realisations.UserID == userID && realisations.SemesterID == semestrID
                    select new { realisations.RealisationID, realisations.Subjects.Name }).Distinct();
                var list2 = list.OrderBy(name => name.Name);
                foreach (var elem in list2)
                {
                    ans.Add(new RealisationsItem { Name = elem.Name, RealisationsID = elem.RealisationID });
                }
                return ans;
            }
        }

        public List<Grades> GetGrades(int realisationID)
        {
            using (var db = new StorageContext())
            {
                List<Grades> ans = new List<Grades>();
                var list = (
                    from grade in db.Grades
                    where grade.Realisations.RealisationID == realisationID
                    select grade).Distinct();
                var list2 = list.OrderBy(name => name.Name);
                foreach (var elem in list2)
                {
                    ans.Add(elem);
                }
                return ans;
            }
        }


        public bool DeleteGrade(int gradeID)
        {
            using (var db = new StorageContext())
            {
                var grade = db.Grades.Find(gradeID);
                db.Grades.Remove(grade);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return false;
                }
                
                return true;
            }
        }


        public bool AddGrade(string name, string maxValue, int realisationID)
        {
            Random rand = new Random();
            int id = rand.Next(899999) + 100000;
            using (var db = new StorageContext())
            {
                Realisations r = db.Realisations.Find(realisationID);
                Grades grade = new Grades() { GradeID = id, Name = name, MaxValue = maxValue, Realisations = r};
                
                db.Grades.Add(grade);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException)
                {
                    return false;
                }
                return true;
            }
        }

        public bool EditGrade(int gradeID, string name, string maxValue, byte[] timeStamp)
        {
            if (!ValidateMaxValue(gradeID, maxValue))
            {
                return false;
            }
            using (var db = new StorageContext())
            {
                //Realisations r = db.Realisations.Find(realisationID);

                Grades grade = db.Grades.Find(gradeID);
                if (grade != null)
                {
                    if (!grade.TimeStamp.SequenceEqual(timeStamp))
                    {
                        return false;
                    }
                    grade.Name = name;
                    grade.MaxValue = maxValue;

                    grade.TimeStamp = timeStamp;
                    db.Entry(grade).State = System.Data.Entity.EntityState.Modified;
                }
                try
                {
                    db.SaveChanges();
                }
                catch(DbUpdateConcurrencyException)
                {
                        return false;
                }
                catch (DbEntityValidationException)
                {
                    return false;
                }
                return true;
            }
        }

        public List<GradeValueItem> GetGradeValueList(int gradeID)
        {
            using (var db = new StorageContext())
            {
                List<GradeValueItem> ans = new List<GradeValueItem>();
                

                int realisationID = (db.Grades.Find(gradeID).RealisationID);

                var students = (from registration in db.Registrations
                                where registration.RealisationID == realisationID
                                select new { registration, registration.Students, registration.Students.Users });
                foreach (var student in students)
                {                  
                    var gradeValues = (from gradeValue in db.GradeValues
                                       where gradeValue.GradeID == gradeID && gradeValue.Registrations.StudentID == student.Students.StudentID
                                       select gradeValue);
                    if (gradeValues.Count() == 1)
                    {
                        var gradeVal = gradeValues.First();
                        ans.Add(new GradeValueItem() { GradeValues = gradeVal, FirstName = student.Users.FirstName, LastName = student.Users.LastName, IndexNo = student.Students.IndexNo, RegistrationID = student.registration.RegistrationID});
                    }
                    else
                    {
                        ans.Add(new GradeValueItem() { GradeValues = null, FirstName = student.Users.FirstName, LastName = student.Users.LastName, IndexNo = student.Students.IndexNo, RegistrationID = student.registration.RegistrationID });
                    }
                    
                }

                return ans;
            }
        }


        public bool AddGradeValue(int gradeID, int registrationID, string date, string value)
        {
            if (!ValidateNewValue(gradeID, value))
            {
                return false;
            }
            Random rand = new Random();
            int id = rand.Next(899999) + 100000;
            using (var db = new StorageContext())
            {
                //Realisations r = db.Realisations.Find(realisationID);
                Registrations r = db.Registrations.Find(registrationID);
                //Grades grade = new Grades() { GradeID = id, Name = name, MaxValue = maxValue, Realisations = r };
                GradeValues grade = new GradeValues() { GradeValueID = id, Date = date, Value = value, GradeID = gradeID, RegistrationID = r.RegistrationID, Registrations = r };
                db.GradeValues.Add(grade);
                try
                {
                    db.SaveChanges();
                }
                catch (DbEntityValidationException)
                {
                    return false;
                }
                return true;
            }
        }

        public bool DeleteGradeValue(int gradeValueID)
        {
            using (var db = new StorageContext())
            {
                var grade = db.GradeValues.Find(gradeValueID);
                db.GradeValues.Remove(grade);
                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    return false;
                }

                return true;
            }
        }

        private bool ValidateMaxValue(int gradeID, string newMaxValue)
        {
            using (var db = new StorageContext())
            {
                Grades grade = db.Grades.Find(gradeID);
                var gradeList = grade.GradeValues;
                if (gradeList.Count == 0)
                {
                    return true;
                }
                foreach (var gradeV in gradeList)
                {
                    if (!ValidateNewValue(gradeV.Value, newMaxValue))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        private bool ValidateNewValue(int gradeID, string value)
        {
            using (var db = new StorageContext())
            {
                Grades grade = db.Grades.Find(gradeID);
                if (grade != null)
                {
                    string maxValue = grade.MaxValue;
                    return ValidateNewValue(value, maxValue);
                    
                }
                return false;
            }
        }

        private bool ValidateNewValue(string value, string maxValue)
        {

            if (maxValue.Contains(";"))
            {
                string[] values = maxValue.Split(';');
                return values.Contains(value.Trim());
            }
            try
            {
                return Convert.ToDecimal(maxValue) >= Convert.ToDecimal(value);
            }
            catch (Exception)
            {
                return false;
            }

        }


        public List<StudentItem> GetErroredStudent(int gradeID, string newValue)
        {
            using (var db = new StorageContext())
            {
                var list = (from gradeValue in db.GradeValues
                       where gradeValue.GradeID == gradeID
                        select gradeValue); ;
                List<StudentItem> ans = new List<StudentItem>();
                foreach ( var s in list)
                {
                    if (!ValidateNewValue( s.Value, newValue))
                    {
                        ans.Add(new StudentItem
                        {
                            FirstName = s.Registrations.Students.Users.FirstName,
                            LastName = s.Registrations.Students.Users.LastName,
                            IndexNo = s.Registrations.Students.IndexNo,
                            GradeValue = s.Value
                        });
                    }
                }
                return ans;
            }
        }


        public List<string> GetPossibleValue(int gtadeID)
        {
            using (var db = new StorageContext())
            {
                var grade = db.Grades.Find(gtadeID);

                if (grade.MaxValue.Contains(";"))
                {
                    return grade.MaxValue.Split(';').ToList();
                    
                }
            }
            return null;
        }
    }
}