using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    [Serializable]
    public class ResultBase_form
    {
        private int _status;

        public int status
        {
            get { return _status; ; }
            set { _status = value; }
        }
        private string _resulturl;

        public string ResultURL
        {
            get { return _resulturl; }
            set { _resulturl = value; }
        }


        private string _msg;

        public string msg
        {
            get { return string.IsNullOrEmpty(_msg) ? "success" : _msg; }
            set { _msg = value; }
        }


        private object _content;

        public object content
        {
            get { return _content == null ? new object() : _content; }
            set { _content = value; }
        }
    }
}
