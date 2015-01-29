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
    [Authorize(Roles = "Professor")]
    public class ProfessorController : Controller
    {
        private IProfessorStorage storage = new ProfessorStorage();

        public ProfessorController()
        {
            storage = new ProfessorStorage();
        }

        public ProfessorController(IProfessorStorage storage)
        {
            this.storage = storage;
        }

        // GET: Professor
        public ActionResult Index()
        {
            BaseViewModel model = new BaseViewModel();
            int userID = storage.GetUserID(User.Identity.Name);
            List<Semesters> semestrs = storage.GetSemestrList(userID);
            Dictionary<Semesters, List<RealisationsItem>> dictionary = new Dictionary<Semesters, List<RealisationsItem>>();
            foreach (var semestr in semestrs)
            {
                dictionary.Add(semestr, storage.GetSubjectList(semestr.SemesterID, userID));
            }
            model.Dictionary = dictionary;
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(BaseViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("GradeList", model);
            }
            return View(model);
        }

        public ActionResult GradeList(BaseViewModel model)
        {
            ProfessorGradeListViewModel modelOut = new ProfessorGradeListViewModel();
            if (model.RealisationsID.HasValue)
            {
                modelOut.RealisationID = model.RealisationsID;
                modelOut.Grades = storage.GetGrades(model.RealisationsID.Value);
            }
            else
            {
                return RedirectToAction("Index");
            }
            return View(modelOut);
        }

        [HttpPost]
        public ActionResult GradeList(ProfessorGradeListViewModel model)
        {
            if ("yes".Equals(model.AddNew))
            {
                return RedirectToAction("EditGrade", model);
            }
            if (model.DGradeID.HasValue)
            {
                if (!storage.DeleteGrade(model.DGradeID.Value))
                {
                    ModelState.AddModelError("", "NIe można usunąć oceny.");
                }
            }
            if (model.EGradeID.HasValue)
            {
                return RedirectToAction("EditGrade", model);
            }
            if (model.WGradeID.HasValue)
            {
                return RedirectToAction("MarkGrades", model);
            }
            model.Grades = storage.GetGrades(model.RealisationID.Value);
            return View(model);
        }

        public ActionResult EditGrade(ProfessorGradeListViewModel model)
        {
            if (!model.RealisationID.HasValue)
            {
                return RedirectToAction("Index");
            }
            EditGradeViewModel outModel = new EditGradeViewModel();
            outModel.RealisationID = model.RealisationID;

            if (model.EGradeID.HasValue)
            {
                Grades g = storage.GetGrades(model.RealisationID.Value).Find(grade => grade.GradeID == model.EGradeID);
                if (g != null)
                {
                    outModel.GradeID = model.EGradeID;
                    outModel.Name = g.Name;
                    outModel.MaxValue = g.MaxValue;
                    outModel.TimeStamp = g.TimeStamp;
                }
            }

            //outModel.Error = true;
            return View(outModel);
        }

        [HttpPost]
        public ActionResult EditGrade(EditGradeViewModel model)
        {
            if ("cancel".Equals(model.Action))
            {
                BaseViewModel outModel = new BaseViewModel();
                outModel.RealisationsID = model.RealisationID;
                return RedirectToAction("GradeList", outModel);
            }
            if ("save".Equals(model.Action))
            {
                if (model.GradeID.HasValue)
                {
                    string name = model.Name.ToString();
                    string value = model.MaxValue.ToString();
                    if (!storage.EditGrade(model.GradeID.Value, name, value, model.TimeStamp))
                    {
                        ModelState.AddModelError("", "Nie można edytować oceny.");
                        model.Error = true;
                        model.ErroredStudents = storage.GetErroredStudent(model.GradeID.Value, value);
                        return View(model);
                    }
                    BaseViewModel outModel = new BaseViewModel();
                    outModel.RealisationsID = model.RealisationID;
                    return RedirectToAction("GradeList", outModel);
                }
                else
                {
                    string name = model.Name.ToString();
                    string value = model.MaxValue.ToString();
                    if (!storage.AddGrade(name, value, model.RealisationID.Value))
                    {
                        ModelState.AddModelError("", "Nie można dodać oceny.");
                        return View(model);
                    }
                    BaseViewModel outModel = new BaseViewModel();
                    outModel.RealisationsID = model.RealisationID;
                    return RedirectToAction("GradeList", outModel);
                }
            }
            return View(model);
        }

        public ActionResult MarkGrades(ProfessorGradeListViewModel model)
        {
            if (!model.WGradeID.HasValue)
            {
                return RedirectToAction("Index");
            }
            MarkGradesViewModel outModel = new MarkGradesViewModel() { RealisationID = model.RealisationID,  GradeID = model.WGradeID };
            outModel.GradeValues = storage.GetGradeValueList(outModel.GradeID.Value);
            outModel.PosibleValue = storage.GetPossibleValue(outModel.GradeID.Value);
            return View(outModel);
        }

        [HttpPost]
        public ActionResult MarkGrades(MarkGradesViewModel model)
        {
            if ("back".Equals(model.Action))
            {
                BaseViewModel outModel = new BaseViewModel();
                outModel.RealisationsID = model.RealisationID;
                return RedirectToAction("GradeList", outModel);
            }
            if (model.RegistrationID.HasValue)
            {
                bool ok = true;
                if (model.Date == null)
                {
                    //ModelState.AddModelError("", "Data nie może być pusta.");
                    //ok = false;
                    model.Date = DateTime.Today.ToString("yyyy-MM-dd");
                }
                if (model.Grade == null)
                {
                    ModelState.AddModelError("", "Ocena nie może być pusta.");
                    ok = false;
                }
                if (ok && !storage.AddGradeValue(model.GradeID.Value, model.RegistrationID.Value, model.Date, model.Grade))
                {
                    ModelState.AddModelError("", "Nie można wystawić oceny.");
                }
                
            }
            if (model.DelValID.HasValue)
            {
                if (!storage.DeleteGradeValue(model.DelValID.Value))
                {
                    ModelState.AddModelError("", "Nie można usunąć oceny.");
                }
            }

            model.GradeValues = storage.GetGradeValueList(model.GradeID.Value);
            model.PosibleValue = storage.GetPossibleValue(model.GradeID.Value);
            return View(model);
        }
    }
}