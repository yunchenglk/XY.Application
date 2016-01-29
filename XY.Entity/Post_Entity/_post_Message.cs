using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class _post_Message
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Content { get; set; }
        private string _PID;

        public string PID
        {
            get
            {
                if (string.IsNullOrEmpty(_PID))
                    return Guid.Empty.ToString();
                return _PID;
            }
            set { _PID = value; }
        }


        public string MessageID { get; set; }
        //Mail信息
        public string Title { get; set; }//主题
    }
    
}
