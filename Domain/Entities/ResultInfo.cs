using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public  class ResultInfo : AuditableEntity
    {
        public int QuestionType { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public string GuidId { get; set; }
        public string AnswerText { get; set; }
        public string QuestionText { get; set; }
        public string SurveyId { get; set; }

          
    }
}
