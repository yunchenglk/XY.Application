using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class FilesView
    {
        public string FilePathStr
        {
            get;set;
        }

        private Guid _ID;

        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private Guid _CompanyID;

        public Guid CompanyID
        {
            get { return _CompanyID; }
            set { _CompanyID = value; }
        }

        private String _Name;

        public String Name
        {
            get { return _Name == null ? string.Empty : _Name; }
            set { _Name = value; }
        }

        private String _Type;

        public String Type
        {
            get { return _Type == null ? string.Empty : _Type; }
            set { _Type = value; }
        }

        private String _OName;

        public String OName
        {
            get { return _OName == null ? string.Empty : _OName; }
            set { _OName = value; }
        }

        private String _FilePath;

        public String FilePath
        {
            get { return _FilePath == null ? string.Empty : _FilePath; }
            set { _FilePath = value; }
        }

        private String _FileExt;

        public String FileExt
        {
            get { return _FileExt == null ? string.Empty : _FileExt; }
            set { _FileExt = value; }
        }

        private Int32 _FileSize;

        public Int32 FileSize
        {
            get { return _FileSize; }
            set { _FileSize = value; }
        }

        private String _Large;

        public String Large
        {
            get { return _Large == null ? string.Empty : _Large; }
            set { _Large = value; }
        }

        private String _Middle;

        public String Middle
        {
            get { return _Middle == null ? string.Empty : _Middle; }
            set { _Middle = value; }
        }

        private String _Small;

        public String Small
        {
            get { return _Small == null ? string.Empty : _Small; }
            set { _Small = value; }
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
    }
}
