using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Request
{
    public  class QuestionInfo
    {
        public long Id { get; set; }
        public int SurveyId { get; set; }
        public string Description { get; set; }
        public int NumberOfPage { get; set; }

        public int AnswerType { get; set; }

        public List<AnswerInfo> answerInfos { get; set; }
        public List<Answer> answerResonpes { get; set; }
        

     
    }
}
