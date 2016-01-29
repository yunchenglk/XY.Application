using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class Messages_ReplyView
    {
        private Guid _ID;

        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private Guid _UserID;

        public Guid UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        private String _Content;

        public String Content
        {
            get { return _Content == null ? string.Empty : _Content; }
            set { _Content = value; }
        }

        private DateTime _CreateTime;

        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }

        private Boolean _DR;

        public Boolean DR
        {
            get { return _DR; }
            set { _DR = value; }
        }

        private Guid _MessageID;

        public Guid MessageID
        {
            get { return _MessageID; }
            set { _MessageID = value; }
        }

        private Guid _ParentID;

        public Guid ParentID
        {
            get { return _ParentID; }
            set { _ParentID = value; }
        }
        public Boolean IsChild
        {
            get
            {
                return ChildItem == null ? false : ChildItem.Count() > 0;
            }
        }
        public IEnumerable<Messages_ReplyView> ChildItem { get; set; }
    }
}
