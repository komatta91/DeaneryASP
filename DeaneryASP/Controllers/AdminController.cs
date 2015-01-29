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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IAdminStorage storage;

        public AdminController()
        {
            storage = new AdminStorage();
        }

        public AdminController(IAdminStorage storage)
        {
            this.storage = storage;
        }

        // GET: Admin
        public ActionResult Index()
        {
            //bool test = User.IsInRole("Admin");
            AdminViewModel model = new AdminViewModel();
            model.Semestrs = storage.GetSemestrList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(AdminViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("SemestrList", model);
            }
            return View(model);
        }

        public ActionResult SemestrList(AdminViewModel model)
        {
            
            if (model.SemestrID.HasValue)
            {
                SubjectListViewModel outModel = new SubjectListViewModel() { SemestrID = model.SemestrID };
                outModel.Subjects = storage.GetRealisationItemList(outModel.SemestrID.Value);
                return View(outModel);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult SemestrList(SubjectListViewModel model)
        {
            if (model.ARealisationID.HasValue)
            {
                return RedirectToAction("AddStudents", model);
            }
            if (model.ERealisationID.HasValue)
            {
                return RedirectToAction("EditRealisation", model);
            }
            if (model.DRealisationID.HasValue)
            {
                if (!storage.DeleteRealisation(model.DRealisationID.Value))
                {
                    ModelState.AddModelError("", "Nie można usunąć.");
                }
            }
            if ("yes".Equals(model.AddNew))
            {
                return RedirectToAction("EditRealisation", model);
            }
            model.Subjects = storage.GetRealisationItemList(model.SemestrID.Value);
            return View(model);
        }

        public ActionResult EditRealisation(SubjectListViewModel model)
        {
            if (!model.SemestrID.HasValue)
            {
                return RedirectToAction("Index");
            }
            EditRealisationViewModel outModel = new EditRealisationViewModel();
            outModel.SemestrID = model.SemestrID;
            if (model.ERealisationID.HasValue)
            {
                outModel.RealisationID = model.ERealisationID;
                Realisations r = storage.GetRealisation(outModel.RealisationID.Value);
                if (r != null)
                {
                    outModel.SemestrID = r.SemesterID;
                    outModel.SubjectID = r.SubjectID;
                    outModel.UserID = r.UserID;
                    outModel.Ver = r.Ver;
                    outModel.TimeStamp = r.TimeStamp;
                }
            }
            outModel.Professors = storage.GetProfessors();
            outModel.Subjects = storage.GetSubjects();
            return View(outModel);
        }

        [HttpPost]
        public ActionResult EditRealisation(EditRealisationViewModel model)
        {
            
            if ("save".Equals(model.Action))
            {
                bool test = true;
                if (model.Ver == null)
                {
                    ModelState.AddModelError("", "Należy podać wersję.");
                    test = false;
                }
                if (!model.SubjectID.HasValue)
                {
                    ModelState.AddModelError("", "Należy wybrać przedmiot.");
                    test = false;
                }
                if (!test)
                {
                    model.Professors = storage.GetProfessors();
                    model.Subjects = storage.GetSubjects();
                    return View(model);
                }
                if (model.RealisationID.HasValue)
                {
                    
                    if (test && !storage.SaveRealisation(
                        model.RealisationID.Value,
                        model.Ver, 
                        model.SemestrID.Value, 
                        model.SubjectID.Value, 
                        model.UserID, 
                        model.TimeStamp))
                    {
                        ModelState.AddModelError("", "Nie można zapisać.");

                        model.Professors = storage.GetProfessors();
                        model.Subjects = storage.GetSubjects();
                        return View(model);
                    }

                }
                else
                {
                    if (test && !storage.AddRealisation(model.Ver, model.SemestrID.Value, model.SubjectID.Value, model.UserID))
                    {
                        ModelState.AddModelError("", "Nie można dodać.");

                        model.Professors = storage.GetProfessors();
                        model.Subjects = storage.GetSubjects();
                        return View(model);
                    }
                }
            }
            SubjectListViewModel outModel = new SubjectListViewModel() { SemestrID = model.SemestrID };
            return RedirectToAction("SemestrList", outModel);


        }

        public ActionResult AddStudents(SubjectListViewModel model)
        {
            if (!model.ARealisationID.HasValue)
            {
                return RedirectToAction("Index");
            }
            AddStudentsViewModel outModel = new AddStudentsViewModel();
            outModel.RealisationID = model.ARealisationID;
            outModel.Students = storage.GetStudents(outModel.RealisationID.Value);
            outModel.RegistredStudents = storage.GetRegistredStudents(outModel.RealisationID.Value);
            //outModel.Students.R
            //outModel.Students = outModel.Students.Except(outModel.RegistredStudents).ToList();
            return View(outModel);
        }

        [HttpPost]
        public ActionResult AddStudents(AddStudentsViewModel model)
        {
            if ("save".Equals(model.Action))
            {
                if (!storage.AddRegistration(model.StudentID.Value, model.RealisationID.Value))
                {
                    ModelState.AddModelError("", "Nie można dodać.");
                }
            }
            if (model.DStudentID.HasValue)
            {
                if (!storage.DeleteRegistration(model.RealisationID.Value, model.DStudentID.Value))
                {
                    ModelState.AddModelError("", "Nie można usunąć.");
                }
            }
            if ("return".Equals(model.Action))
            {
                SubjectListViewModel outModel = new SubjectListViewModel() { SemestrID = model.SemestrID };
                return RedirectToAction("SemestrList", outModel);
            }
            
            model.Students = storage.GetStudents(model.RealisationID.Value);
            model.RegistredStudents = storage.GetRegistredStudents(model.RealisationID.Value);

            return View(model);
        }

    }
}