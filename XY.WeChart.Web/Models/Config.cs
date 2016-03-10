using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.WeChart.Web.Models
{
    public class Config
    {
        public int QrCodeId { get; set; }
        public List<string> Versions { get; set; }
        public int DownloadCount { get; set; }

    }
}
