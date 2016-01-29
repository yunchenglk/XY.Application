using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class ResultBase_Page
    {
        public ResultBase_Page(int pageIndex, int pageSize, int total)
        {
            this._pageIndex = pageIndex;
            this._pageSize = pageSize;
            this._total = total;
        }
        public ResultBase_Page()
        {

        }
        private int _pageIndex;

        public int pageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        private int _pageSize;

        public int pageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        private int _total;

        public int total
        {
            get { return _total; }
            set { _total = value; }
        }

        private object _content;

        public object content
        {
            get { return _content; }
            set { _content = value; }
        }


    }
}
