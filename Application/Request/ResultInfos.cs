using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Request
{
    public  class ResultInfos
    {
        public int QuestionType { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public string QuestionText { get; set; }
        public string SurveyId { get; set; }
        public string GuidId { get; set; }
    }
}
