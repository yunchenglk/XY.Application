using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XY.Entity.Weixin;

namespace XY.WeChart.Web.Models
{
    public class CodeRecord
    {
        public string Key { get; set; }
        public int QrCodeId { get; set; }
        public CreateQrCodeResult QrCodeTicket { get; set; }
        public string Version { get; set; }
        public bool Used { get; set; }
        public bool AllowDownload { get; set; }
    }
}
