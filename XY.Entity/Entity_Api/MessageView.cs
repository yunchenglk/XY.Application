using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class MessageView
    {
        private Guid _ID;
        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private Int32 _Type;

        public Int32 Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private Guid _RelationID;

        public Guid RelationID
        {
            get { return _RelationID; }
            set { _RelationID = value; }
        }

        private Guid _CompanyID;

        public Guid CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }

        private Guid _UserID;

        public Guid UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
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

        private String _Content;

        public String Content
        {
            get { return _Content == null ? string.Empty : _Content; }
            set { _Content = value; }
        }

        private Int32 _Count;

        public Int32 Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        private String _Name;

        public String Name
        {
            get { return _Name == null ? string.Empty : _Name; }
            set { _Name = value; }
        }

        private String _Email;

        public String Email
        {
            get { return _Email == null ? string.Empty : _Email; }
            set { _Email = value; }
        }

        private String _Phone;

        public String Phone
        {
            get { return _Phone == null ? string.Empty : _Phone; }
            set { _Phone = value; }
        }
        public string TypeStr
        {
            get
            {
                switch (Type)
                {
                    case 0:
                        return "留言";
                    case 1:
                        return "新闻";
                    case 2:
                        return "栏目";
                    default:
                        return "未知";
                }
            }
        }

        public string ContentStr
        {
            get
            {
                if (Content == null)
                    return "无主题";
                else if (Content.Length > 20)
                    return Content.Substring(0, 20);
                else
                    return Content;
            }
        }

        public string CreateTimeStr
        {
            get
            {
                return CreateTime.ToString();
            }
        }
    }
}
