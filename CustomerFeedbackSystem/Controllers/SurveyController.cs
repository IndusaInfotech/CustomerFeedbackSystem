using Application.Interfaces.Repositories;
using Application.Request;
using CustomerFeedbackSystem.Models;
using Domain.Entities;
using EllipticCurve;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CustomerFeedbackSystem.Controllers
{
    public class SurveyController : Controller
    {

      
        private readonly ISurveyRepository _surveyRepository;
        private readonly IResultRepository _reultRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        public SurveyController(ISurveyRepository surveyRepository, IResultRepository reultRepository, IWebHostEnvironment hostEnvironment)
        {
            _surveyRepository = surveyRepository;
           
            webHostEnvironment = hostEnvironment;
            _reultRepository= reultRepository;

        }

      
        public IActionResult Index()
        {
            var Survey = _surveyRepository.GetListAsync().Result;

            ViewBag.SurveyData = Survey;
            return View(Survey);
        }
        [HttpGet]
        public IActionResult AddSurvey(long? Id)
        {

            if (Id == null)
            {
                return View();
            }
            else
            {
                var data = _surveyRepository.GetByIdAsync(Id).Result;
                
                return View(data);
            }
        }

        [HttpPost]
        public IActionResult AddSurvey(SurveyInfo surveyInfo)
        {
            string uniqueFileName = UploadedFile(surveyInfo);

            Survey survey = new Survey();
            survey.Name = surveyInfo.Name;
            survey.Description = surveyInfo.Description;
            survey.NumberOfPage = surveyInfo.NumberOfPage;
            survey.Title = surveyInfo.Title;
            survey.Images = uniqueFileName;
            survey.IsActive = surveyInfo.IsActive;
            survey.CreatedOn = DateTime.Now;
            survey.Location = surveyInfo.Location;
            survey.Id = surveyInfo.Id;
            if (surveyInfo.Id == 0)
            {
                var Data = _surveyRepository.InsertAsync(survey);
                if (Data.Result > 0)
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var Data = _surveyRepository.UpdateAsync(survey);
                return RedirectToAction("Index");

            }


            return View();
        }

        private string UploadedFile(SurveyInfo surveyInfo)
        {
            string uniqueFileName = null;

            if (surveyInfo.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + surveyInfo.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    surveyInfo.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public IActionResult Delete(Survey survey)
      {
            var Data = _surveyRepository.DeleteAsync(survey);
            return RedirectToAction("Index");
      }
        [HttpGet]
       public IActionResult UploadFile()
        {
            return View();
        }


        public IActionResult Reports(ReportRequest request)
        {
           
            if (request.SurveyName != null)
            {
                var Survey = _surveyRepository.GetListAsync().Result.Where(x=>x.Name.Contains(request.SurveyName)).FirstOrDefault();
                var result = _reultRepository.GetListAsync().Result.Where(x => x.SurveyId == Survey.Id.ToString());
                return View(result);

            }
            else if (request.Location != null)
            {
                var   Survey = _surveyRepository.GetListAsync().Result.Where(x => x.Name == request.Location).FirstOrDefault();
                if (Survey != null)
                {
                    var result = _reultRepository.GetListAsync().Result.Where(x => x.SurveyId == Survey.Id.ToString());
                    return View(result);
                }

            }
            else if(request.StartDate !=null && request.EndDate !=null)
            {
                var result = _reultRepository.GetListAsync().Result.Where(x => x.CreatedOn.Date <= request.StartDate.Date && x.CreatedOn>= request.EndDate.Date);
                return View(result);
            }

            return View();
        }

    }
}
