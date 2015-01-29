using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeaneryASP;
using DeaneryASP.Controllers;
using DeaneryASP.Models.Storage.Impl;
using Rhino.Mocks;
using DeaneryASP.Models.Storage;
using DeaneryASP.Models;
using DeaneryASP.Models.Storage.Custom;

namespace DeaneryASP.Tests.Controllers
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void BaseTest()
        {
            HomeController controller = new HomeController();

            ViewResult result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);


        }

        [TestMethod]
        public void LoginStorage()
        {
           
            LoginStorage storage = new LoginStorage();
            int res = storage.WhoLogins("Admin", "Admin");
            Assert.AreEqual(0, res);
            res = storage.WhoLogins("jk", "jk");
            Assert.AreEqual(1, res);
            res = storage.WhoLogins("belfer1", "belfer1");
            Assert.AreEqual(2, res);

        }

        [TestMethod]
        public void AddDeleteGradeTest()
        {

            IProfessorStorage storage = new ProfessorStorage();

            int userID = storage.GetUserID("belfer1");
            Assert.AreNotEqual(userID, 0);
            var semestr = storage.GetSemestrList(userID).First();
            Assert.IsNotNull(semestr);
            var subject = storage.GetSubjectList(semestr.SemesterID, userID).First();
            Assert.IsNotNull(subject);
            
            bool test = storage.AddGrade("123!@#123", "10", subject.RealisationsID);
            Assert.IsTrue(test);
            var grade = storage.GetGrades(subject.RealisationsID).First(m => m.Name == "123!@#123");
            Assert.IsNotNull(grade);
            bool del = storage.DeleteGrade(grade.GradeID);
            Assert.IsTrue(del);


        }

        [TestMethod]
        public void AddDeleteGradeValue()
        {
            IProfessorStorage storage = new ProfessorStorage();

            int userID = storage.GetUserID("belfer1");
            Assert.AreNotEqual(userID, 0);
            var semestr = storage.GetSemestrList(userID).First();
            Assert.IsNotNull(semestr);
            var subject = storage.GetSubjectList(semestr.SemesterID, userID).First();
            Assert.IsNotNull(subject);

            var grade = storage.GetGrades(subject.RealisationsID).First(m => !m.MaxValue.Contains(';'));
            Assert.IsNotNull(grade);

            var regList = storage.GetGradeValueList(grade.GradeID);

            GradeValueItem item = null;
            foreach (var elem in regList)
            {
                if (elem.GradeValues == null)
                {
                    item = elem;
                    break;
                }
            }

            bool add = storage.AddGradeValue(grade.GradeID, item.RegistrationID, "1991-07-01", grade.MaxValue);
            Assert.IsTrue(add);
            var gradeValue = storage.GetGradeValueList(grade.GradeID).Find(m => m.GradeValues != null && m.GradeValues.Date.Equals("1991-07-01"));

            Assert.IsNotNull(gradeValue);
            Assert.IsNotNull(gradeValue.GradeValues);
            bool del = storage.DeleteGradeValue(gradeValue.GradeValues.GradeValueID);
            Assert.IsTrue(del);

        }

        [TestMethod]
        public void ConcurrencyGradeTest()
        {

            IProfessorStorage storage = new ProfessorStorage();

            int userID = storage.GetUserID("belfer1");
            Assert.AreNotEqual(userID, 0);
            var semestr = storage.GetSemestrList(userID).First();
            Assert.IsNotNull(semestr);
            var subject = storage.GetSubjectList(semestr.SemesterID, userID).First();
            Assert.IsNotNull(subject);
            var grade = storage.GetGrades(subject.RealisationsID).First();
            Assert.IsNotNull(grade);

            bool test1 = storage.EditGrade(grade.GradeID, grade.Name, "-1", grade.TimeStamp);
            bool test2 = storage.EditGrade(grade.GradeID, grade.Name, "-1", grade.TimeStamp);
            
            var grade2 = storage.GetGrades(subject.RealisationsID).First(m => m.GradeID == grade.GradeID);
            bool test3 = storage.EditGrade(grade.GradeID, grade.Name, "-1", grade2.TimeStamp);

            Assert.IsTrue(test1);
            Assert.IsFalse(test2);
            Assert.IsTrue(test3);
        }

        [TestMethod]
        public void AddDeleteRealisationTest()
        {
            IAdminStorage storage = new AdminStorage();
            var semestr = storage.GetSemestrList().First();
            var subject = storage.GetSubjects().First();
            bool add = storage.AddRealisation("Q", semestr.SemesterID, subject.SubjectID, null);
            var rel = storage.GetRealisationItemList(semestr.SemesterID).Find(m => m.Name == subject.Name && m.Version == "Q");
            bool del = storage.DeleteRealisation(rel.RealisationID);
            Assert.IsTrue(add);
            Assert.IsTrue(del);

        }

        [TestMethod]
        public void LoginControllerMock()
        {
            var moc = MockRepository.MockWithRemoting<ILoginStorage>();

            moc.Expect(m => m.WhoLogins("Admin", "Admin"));

            LoginController controler = new LoginController(moc);

            LoginViewModel model = new LoginViewModel();
            model.Login = "Admin";
            model.Password = "Admin";

            controler.Login(model);

            moc.VerifyAllExpectations();

        }

        [TestMethod]
        public void AdminControllerMock()
        {
            var moc = MockRepository.MockWithRemoting<IAdminStorage>();


            AdminController controler = new AdminController(moc);

            moc.Expect(m => m.GetRealisation(1));
            moc.Expect(m => m.GetProfessors());
            moc.Expect(m => m.GetSubjects());


            SubjectListViewModel model = new SubjectListViewModel();
            model.SemestrID = 1;
            model.ERealisationID = 1;

            controler.EditRealisation(model);

            moc.VerifyAllExpectations();

        }

        [TestMethod]
        public void AdminControllerMock2()
        {
            var moc = MockRepository.MockWithRemoting<IAdminStorage>();


            AdminController controler = new AdminController(moc);

            moc.Expect(m => m.GetStudents(1));
            moc.Expect(m => m.GetRegistredStudents(1));

            SubjectListViewModel model = new SubjectListViewModel();
            model.ARealisationID = 1;

            controler.AddStudents(model);

            moc.VerifyAllExpectations();

        }

        [TestMethod]
        public void ProfessorControllerMock()
        {
            var moc = MockRepository.MockWithRemoting<IProfessorStorage>();

            moc.Expect(m => m.GetGrades(0));

            ProfessorController controler = new ProfessorController(moc);

            BaseViewModel model = new BaseViewModel();
            model.RealisationsID = 0;
            
            controler.GradeList(model);

            moc.VerifyAllExpectations();

        }


    }
}
