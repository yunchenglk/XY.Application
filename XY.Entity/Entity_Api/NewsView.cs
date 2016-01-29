using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Entity
{
    public class NewsView
    {
        private Guid _ID;

        public Guid ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        private Guid _ClassID;

        public Guid ClassID
        {
            get { return _ClassID; }
            set { _ClassID = value; }
        }
        public string ClassName
        {
            get; set;
        }

        private Int32 _Type;

        public Int32 Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private String _Title;

        public String Title
        {
            get { return _Title == null ? string.Empty : _Title; }
            set { _Title = value; }
        }

        private String _STitle;

        public String STitle
        {
            get { return _STitle == null ? string.Empty : _STitle; }
            set { _STitle = value; }
        }

        private String _Author;

        public String Author
        {
            get { return _Author == null ? string.Empty : _Author; }
            set { _Author = value; }
        }

        private String _Souce;

        public String Souce
        {
            get { return _Souce == null ? string.Empty : _Souce; }
            set { _Souce = value; }
        }

        private String _Tags;

        public String Tags
        {
            get { return _Tags == null ? string.Empty : _Tags; }
            set { _Tags = value; }
        }

        private String _NaviContent;

        public String NaviContent
        {
            get { return _NaviContent == null ? string.Empty : _NaviContent; }
            set { _NaviContent = value; }
        }

        private String _DescriptionStr;

        public String DescriptionStr
        {
            get { return _DescriptionStr == null ? string.Empty : _DescriptionStr; }
            set { _DescriptionStr = value; }
        }
        public string Short_Dec
        {
            get
            {
                return Util.Utils.DropHTML(DescriptionStr);
            }
        }


        private String _Metakeywords;

        public String Metakeywords
        {
            get { return _Metakeywords == null ? string.Empty : _Metakeywords; }
            set { _Metakeywords = value; }
        }

        private String _Metadesc;

        public String Metadesc
        {
            get { return _Metadesc == null ? string.Empty : _Metadesc; }
            set { _Metadesc = value; }
        }

        private String _SEOTitle;

        public String SEOTitle
        {
            get { return _SEOTitle == null ? string.Empty : _SEOTitle; }
            set { _SEOTitle = value; }
        }

        private String _SEOTags;

        public String SEOTags
        {
            get { return _SEOTags == null ? string.Empty : _SEOTags; }
            set { _SEOTags = value; }
        }

        private Int32 _Count;

        public Int32 Count
        {
            get { return _Count; }
            set { _Count = value; }
        }

        private Boolean _IsAudit;

        public Boolean IsAudit
        {
            get { return _IsAudit; }
            set { _IsAudit = value; }
        }

        private Boolean _IsTop;

        public Boolean IsTop
        {
            get { return _IsTop; }
            set { _IsTop = value; }
        }

        private Boolean _IsRecommend;

        public Boolean IsRecommend
        {
            get { return _IsRecommend; }
            set { _IsRecommend = value; }
        }

        private Boolean _IsComm;

        public Boolean IsComm
        {
            get { return _IsComm; }
            set { _IsComm = value; }
        }

        private Boolean _IsVote;

        public Boolean IsVote
        {
            get { return _IsVote; }
            set { _IsVote = value; }
        }

        private String _SlidePicStr;

        public String SlidePicStr
        {
            get { return _SlidePicStr == null ? string.Empty : _SlidePicStr; }
            set { _SlidePicStr = value; }
        }

        private String _HomePic;

        public String HomePic
        {
            get { return _HomePic == null ? string.Empty : _HomePic; }
            set { _HomePic = value; }
        }

        private String _EditorRec;

        public String EditorRec
        {
            get { return _EditorRec == null ? string.Empty : _EditorRec; }
            set { _EditorRec = value; }
        }

        private String _Remark;

        public String Remark
        {
            get { return _Remark == null ? string.Empty : _Remark; }
            set { _Remark = value; }
        }

        private Int32 _status;

        public Int32 status
        {
            get { return _status; }
            set { _status = value; }
        }

        private Boolean _DR;

        public Boolean DR
        {
            get { return _DR; }
            set { _DR = value; }
        }

        private DateTime _ModifyTime;

        public DateTime ModifyTime
        {
            get { return _ModifyTime; }
            set { _ModifyTime = value; }
        }

        private DateTime _CreateTime;

        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        public string CreateTimeStr
        {
            get
            {
                return CreateTime.ToString("yyyy-MM-dd");
            }
        }

    }
}
