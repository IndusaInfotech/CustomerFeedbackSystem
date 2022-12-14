using Application.Interfaces.Repositories;
using Application.Request;
using AspNetCoreHero.Results;
using AutoMapper;
using Domain.Entities;
using EllipticCurve;
using Infrastructure.Migrations.ApplicationDb;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Answer = Domain.Entities.Answer;
using Question = Domain.Entities.Question;
using ResultInfo = Domain.Entities.ResultInfo;

namespace CustomerFeedbackSystem.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly ISurveyRepository _surveyRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IResultRepository _reultRepository;
        public object SurveyId { get; private set; }
        public object Data { get; private set; }

        public QuestionController(IQuestionRepository questionRepository, IResultRepository resultRepository, ISurveyRepository surveyRepositor,IAnswerRepository answerRepository)
        {
            _questionRepository = questionRepository;
            _surveyRepository = surveyRepositor;
            _answerRepository = answerRepository;
            _reultRepository = resultRepository;
          
        }

        public IActionResult Index()
        {
         
            return View();  
           
        }
        public IActionResult AddQuestion(int SurveyId, long? questionId)
        {
            TempData["SurveyId"] = SurveyId;
            var Survey = _surveyRepository.GetListAsync().Result.Where(x => x.Id == SurveyId);
            ViewBag.SurveyData = Survey;

            var Question = _questionRepository.GetListAsync().Result.Where(x => x.SurveyId == SurveyId);
            var AnswerList = _answerRepository.GetListAsync();

            List<QuestionInfo> questions = new List<QuestionInfo>();
            Question.ToList().ForEach(x =>
            {
                QuestionInfo questionInfo = new QuestionInfo();

                questionInfo.Description = x.Description;
                questionInfo.NumberOfPage = x.NumberOfPage;
                questionInfo.AnswerType = x.AnswerType;
                questionInfo.Id = x.Id;
                if (questionInfo.AnswerType == 1 || questionInfo.AnswerType == 7)
                {
                    var answers = AnswerList.Result.Where(m => m.QuestionId == x.Id);

                    questionInfo.answerResonpes = answers.ToList();
                }
                questions.Add(questionInfo);


            });


            #region ViewBag
            List<SelectListItem> AnswerTypeList = new List<SelectListItem>() {
        new SelectListItem {
            Text = "Single Answer/Radio Button", Value = "1"
        },
       
        new SelectListItem {
            Text = "Radio Button With Smileys", Value = "2"
        },
        new SelectListItem {
            Text = "Name", Value = "3"
        },
        new SelectListItem {
            Text = "Email", Value = "4"
        },
        new SelectListItem {
            Text = "Mobile", Value = "5"
        },
        new SelectListItem {
            Text = "Text Field/Text Area", Value = "6"
        },
           new SelectListItem {
            Text = "Multiple Answer/Check Box", Value = "7"
        },

    };
            ViewBag.AnswerType = AnswerTypeList;
            #endregion

           
            ViewBag.QuestionData = questions;
            if (questionId == null)
            {
                return View();
            }
            else
            {
                var data = _questionRepository.GetByIdAsync(questionId).Result;

                return View(data);
            }

        }
        [HttpPost]
        public IActionResult AddQuestion(QuestionInfo questionInfo)
        {
            TempData["SurveyId"] = questionInfo.SurveyId;
            var SId = TempData["SurveyId"];
            Question question = new Question();
          
            question.Description = questionInfo.Description;
            question.NumberOfPage = questionInfo.NumberOfPage;
            question.AnswerType = questionInfo.AnswerType;
            question.Id = questionInfo.Id;
            question.CreatedOn = DateTime.Now;
            question.SurveyId = (int)SId;
            if (questionInfo.Id == 0)
            {
                var Data = _questionRepository.InsertAsync(question);

                if (Data.Result > 0)
                {
                    if (questionInfo.AnswerType == 1 || questionInfo.AnswerType == 7)
                    {
                     
                        List<Answer> AnswerList = new List<Answer>();
                        questionInfo.answerInfos.ForEach(x =>
                        {
                           
                            //var mapper = new Mapper(config);
                            //var Ans = mapper.Map<Answer>(x);
                            Answer Ans = new Answer();
                            Ans.OpetionText = x.OpetionText;
                            Ans.QuestionId =Convert.ToInt32(Data.Result);
                            Ans.Type = questionInfo.AnswerType;
                            AnswerList.Add(Ans);
                            _answerRepository.InsertAsync(Ans);
                           
                        });
                       
                    }

                    return RedirectToAction("Question/AddQuestion", new { SurveyId = SId });
                }
                
            }
            else
            {
                var Data = _questionRepository.UpdateAsync(question);
                return RedirectToAction("AddQuestion", new { SurveyId = SId });

            }


            return View();
        }
        public IActionResult Delete(Question question)
        {
            TempData["SurveyId"] = question.SurveyId;
            var SId = TempData["SurveyId"];
         
            var Data = _questionRepository.DeleteAsync(question);
            return RedirectToAction("AddQuestion", new { SurveyId = SId });
        }

        public IActionResult Survey(int SurveyId)
        {
            
            var Question = _questionRepository.GetListAsync().Result.Where(x => x.SurveyId == SurveyId);
            var AnswerList = _answerRepository.GetListAsync();


            List<QuestionInfo> questions = new List<QuestionInfo>();
            Question.ToList().ForEach(x =>
            {
                QuestionInfo questionInfo = new QuestionInfo();
              
                questionInfo.Description = x.Description;
                questionInfo.NumberOfPage = x.NumberOfPage;
                questionInfo.AnswerType = x.AnswerType;
                questionInfo.Id = x.Id;
                if (questionInfo.AnswerType == 1 || questionInfo.AnswerType == 7)
                {
                    var answers = AnswerList.Result.Where(m => m.QuestionId == x.Id);

                    questionInfo.answerResonpes = answers.ToList();
                }
                    questions.Add(questionInfo);
              
            });

        
            var data = questions.OrderBy(x => x.NumberOfPage);
            ViewBag.AnswerData = data.Count();
            ViewBag.SId = SurveyId;
            return View(data);
           


        }
        public IActionResult Welcome(int SurveyId)
        {
            var Survey = _surveyRepository.GetListAsync().Result.Where(x => x.Id == SurveyId);
            ViewBag.SurveyData = Survey;
             return View();
        }

        public IActionResult ThankYou(int SurveyId)
        {
            var Survey = _surveyRepository.GetListAsync().Result.Where(x => x.Id == SurveyId);
            ViewBag.SurveyData = Survey;
            return View();
        }
        public IActionResult ShowResult(string Guied,String Survey)
        {
            var results = _reultRepository.GetListAsync().Result.Where(x => x.GuidId == Guied);
            ViewBag.SurveyData = Survey;
            return View(results);
        }
        public JsonResult SaveAnswer(ResultInfos result)
        {

            if(result.GuidId=="" || result.GuidId == null)
            {
                Guid guid = Guid.NewGuid();
                result.GuidId = guid.ToString();
            }


            ResultInfo result1 = new ResultInfo();
            result1.SurveyId = result.SurveyId;
            result1.QuestionId = result.QuestionId;
            result1.AnswerText = result.AnswerText;
            result1.QuestionType = result.QuestionType;
            result1.CreatedOn = DateTime.Now;
            result1.QuestionText = result.QuestionText;
            result1.GuidId = result.GuidId;          
            var Data = _reultRepository.InsertAsync(result1);
            if (Data.Id == null)
            {
                return new JsonResult(new { message = "Error Messages" })
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else
            {
                return new JsonResult(new { message = result.GuidId})
                {
                    StatusCode = StatusCodes.Status200OK
                };
            }
        }

        public IActionResult SaveSurvey(ResultInfo obj)
        {

            return new  JsonResult(new {message="done"});

        }
        public IActionResult MainReports()
        {
            return View();
        }

    }
}
