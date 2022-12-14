using Application.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
            List<DataPoint> dataPoints = new List<DataPoint>();

            dataPoints.Add(new DataPoint("2022-12-09", 67));
            dataPoints.Add(new DataPoint("2022-11-09", 80));
            dataPoints.Add(new DataPoint("2022-10-09", 32));
            dataPoints.Add(new DataPoint("2022-09-09", 3));
 

            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

            return View();
        }

        public IActionResult ReportIndex()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            for (int i = 1; i <= 30; i++)
            {
                dataPoints.Add(new DataPoint("2022-12-0" + i, 67+i));
            }
          


            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);

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
