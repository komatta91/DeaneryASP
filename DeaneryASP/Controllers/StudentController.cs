using DeaneryASP.Models;
using DeaneryASP.Models.Storage;
using DeaneryASP.Models.Storage.Custom;
using DeaneryASP.Models.Storage.Entity;
using DeaneryASP.Models.Storage.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeaneryASP.Controllers
{
    
    [Authorize(Roles = "Student")]
    public class StudentController : Controller
    {
        private IStudentStorage storage = new StudentStorage();

        public StudentController()
        {
            storage = new StudentStorage();
        }

        public StudentController(IStudentStorage storage)
        {
            this.storage = storage;
        }

        // GET: Student
        public ActionResult Index()
        {
            BaseViewModel model = new BaseViewModel();
            if (ModelState.IsValid)
            {
                int studentId = storage.GetStudentID(User.Identity.Name);
                List<Semesters> semestrs = storage.GetSemestrList(studentId);
                Dictionary<Semesters, List<RealisationsItem>> dictionary = new Dictionary<Semesters, List<RealisationsItem>>();
                foreach (var semestr in semestrs)
                {
                    dictionary.Add(semestr, storage.GetSubjectList(semestr.SemesterID, studentId));
                    //model.SubjectsList = storage.GetSubjectList(semestr.SemesterID, studentId);
                }
                model.Dictionary = dictionary;
                

            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(BaseViewModel model)
        {
            //studentId = storage.getStudentID(User.Identity.Name);
            //StudentViewModel model = new StudentViewModel();
            //model.Semestrs = storage.getSemestrList(studentId);
            if (ModelState.IsValid)
            {
                return RedirectToAction("GradeList", model);
            }
            return View(model);
        }

        public ActionResult GradeList(BaseViewModel model)
        {
            GradeListViewModel modelOut = new GradeListViewModel();
            if (model.RealisationsID.HasValue)
            {
                modelOut.GradeItems = storage.GetGradeList(model.RealisationsID.Value, storage.GetStudentID(User.Identity.Name));
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View(modelOut);
        }



    }
}