using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public partial class Files
    {
        public string FilePathStr
        {
            get
            {
                return Util.Utils.AddURL(FilePath);
            }
        }
        public string LargeStr
        {
            get
            {
                return Util.Utils.AddURL(Large);
            }
        }
        public string MiddleStr
        {
            get
            {
                return Util.Utils.AddURL(Middle);
            }
        }

        public string SmallStr
        {
            get
            {
                return Util.Utils.AddURL(Small);
            }
        }
        public string WatermarkUEL
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["sourceWeb_"];
            }
        }
        public bool IsOnline
        {
            get
            {
                return !string.IsNullOrEmpty(media_id);
            }
        }
    }
}
