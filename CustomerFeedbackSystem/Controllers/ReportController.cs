using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace CustomerFeedbackSystem.Controllers
{
    public class ReportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
       

        public IActionResult Reports()
        {
            #region ViewBag
            List<SelectListItem> ReportTypeList = new List<SelectListItem>() {
        new SelectListItem {
            Text = "WGS_WASHROOM", Value = "1"
        },
        new SelectListItem {
            Text = "WGS_Reception", Value = "2"
        },
       

    };
            ViewBag.Report = ReportTypeList;
            #endregion
            return View();
        }
    }
}
