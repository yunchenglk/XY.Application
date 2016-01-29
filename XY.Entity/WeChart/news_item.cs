using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class news_item
    {
        public string title { get; set; }
        public string author { get; set; }
        public string digest { get; set; }
        public string content { get; set; }
        public string content_source_url { get; set; }
        public string thumb_media_id { get; set; }
        public string show_cover_pic { get; set; }
        public string url { get; set; }
        public string thumb_url { get; set; }
    }
}
