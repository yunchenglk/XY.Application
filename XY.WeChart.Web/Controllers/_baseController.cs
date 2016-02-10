using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XY.Services;

namespace XY.WeChart.Web.Controllers
{
    public class _baseController : Controller
    {
        public string GetToken()
        {
            string error;
            string token = WeChartAPI.getAccessToken(UserDateTicket.Company.ID, out error);
            if (!string.IsNullOrEmpty(error))
            {
                return error;
            }
            return token;
        }
    }
}