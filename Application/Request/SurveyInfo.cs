using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Request
{
   public class SurveyInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public int NumberOfPage { get; set; }

       
        public IFormFile Image { get; set; }
        public bool IsActive { get; set; }
    }
}
