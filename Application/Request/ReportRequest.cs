using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Request
{
    public class ReportRequest
    {
        public string SurveyName { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
