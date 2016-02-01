using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel;
using System;
using YunChengLK.Framework.Data;
namespace XY.Entity
{
	
	

    [Table("Authority")]
	[Serializable] 
    public partial class Authority :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _PID;

        [Column("PID", DbType.Guid,false,false)]
        public Guid  PID 
		{ 
		   get{ return _PID;  } 
		   set{_PID = value;}
		 }
        
		private  String _Name;

        [Column("Name", DbType.String,false,false)]
        public String  Name 
		{ 
		   get{  return _Name == null ?string.Empty:_Name;  } 
		   set{_Name = value;}
		 }
        
		private  String _Description;

        [Column("Description", DbType.String,false,false)]
        public String  Description 
		{ 
		   get{  return _Description == null ?string.Empty:_Description;  } 
		   set{_Description = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  Int32 _Sort;

        [Column("Sort", DbType.Int32,false,false)]
        public Int32  Sort 
		{ 
		   get{ return _Sort;  } 
		   set{_Sort = value;}
		 }
            }


    [Table("Class")]
	[Serializable] 
    public partial class Class :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  String _Title;

        [Column("Title", DbType.String,false,false)]
        public String  Title 
		{ 
		   get{  return _Title == null ?string.Empty:_Title;  } 
		   set{_Title = value;}
		 }
        
		private  String _EnTitle;

        [Column("EnTitle", DbType.String,false,false)]
        public String  EnTitle 
		{ 
		   get{  return _EnTitle == null ?string.Empty:_EnTitle;  } 
		   set{_EnTitle = value;}
		 }
        
		private  Guid _ParentID;

        [Column("ParentID", DbType.Guid,false,false)]
        public Guid  ParentID 
		{ 
		   get{ return _ParentID;  } 
		   set{_ParentID = value;}
		 }
        
		private  Int32 _Count;

        [Column("Count", DbType.Int32,false,false)]
        public Int32  Count 
		{ 
		   get{ return _Count;  } 
		   set{_Count = value;}
		 }
        
		private  String _Publisher;

        [Column("Publisher", DbType.String,false,false)]
        public String  Publisher 
		{ 
		   get{  return _Publisher == null ?string.Empty:_Publisher;  } 
		   set{_Publisher = value;}
		 }
        
		private  String _Pic;

        [Column("Pic", DbType.String,false,false)]
        public String  Pic 
		{ 
		   get{  return _Pic == null ?string.Empty:_Pic;  } 
		   set{_Pic = value;}
		 }
        
		private  String _URL;

        [Column("URL", DbType.String,false,false)]
        public String  URL 
		{ 
		   get{  return _URL == null ?string.Empty:_URL;  } 
		   set{_URL = value;}
		 }
        
		private  String _Description;

        [Column("Description", DbType.String,false,false)]
        public String  Description 
		{ 
		   get{  return _Description == null ?string.Empty:_Description;  } 
		   set{_Description = value;}
		 }
        
		private  Boolean _IsPublic;

        [Column("IsPublic", DbType.Boolean,false,false)]
        public Boolean  IsPublic 
		{ 
		   get{ return _IsPublic;  } 
		   set{_IsPublic = value;}
		 }
        
		private  Int32 _Sort;

        [Column("Sort", DbType.Int32,false,false)]
        public Int32  Sort 
		{ 
		   get{ return _Sort;  } 
		   set{_Sort = value;}
		 }
        
		private  Int32 _Type;

        [Column("Type", DbType.Int32,false,false)]
        public Int32  Type 
		{ 
		   get{ return _Type;  } 
		   set{_Type = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  Guid _CompanyID;

        [Column("CompanyID", DbType.Guid,false,false)]
        public Guid  CompanyID 
		{ 
		   get{ return _CompanyID;  } 
		   set{_CompanyID = value;}
		 }
        
		private  String _cate_id;

        [Column("cate_id", DbType.String,false,false)]
        public String  cate_id 
		{ 
		   get{  return _cate_id == null ?string.Empty:_cate_id;  } 
		   set{_cate_id = value;}
		 }
            }


    [Table("Company")]
	[Serializable] 
    public partial class Company :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  String _Name;

        [Column("Name", DbType.String,false,false)]
        public String  Name 
		{ 
		   get{  return _Name == null ?string.Empty:_Name;  } 
		   set{_Name = value;}
		 }
        
		private  String _URL;

        [Column("URL", DbType.String,false,false)]
        public String  URL 
		{ 
		   get{  return _URL == null ?string.Empty:_URL;  } 
		   set{_URL = value;}
		 }
        
		private  String _Logo;

        [Column("Logo", DbType.String,false,false)]
        public String  Logo 
		{ 
		   get{  return _Logo == null ?string.Empty:_Logo;  } 
		   set{_Logo = value;}
		 }
        
		private  String _Type;

        [Column("Type", DbType.String,false,false)]
        public String  Type 
		{ 
		   get{  return _Type == null ?string.Empty:_Type;  } 
		   set{_Type = value;}
		 }
        
		private  String _Address;

        [Column("Address", DbType.String,false,false)]
        public String  Address 
		{ 
		   get{  return _Address == null ?string.Empty:_Address;  } 
		   set{_Address = value;}
		 }
        
		private  String _WatermarkPIC;

        [Column("WatermarkPIC", DbType.String,false,false)]
        public String  WatermarkPIC 
		{ 
		   get{  return _WatermarkPIC == null ?string.Empty:_WatermarkPIC;  } 
		   set{_WatermarkPIC = value;}
		 }
        
		private  String _Fax;

        [Column("Fax", DbType.String,false,false)]
        public String  Fax 
		{ 
		   get{  return _Fax == null ?string.Empty:_Fax;  } 
		   set{_Fax = value;}
		 }
        
		private  String _Tel;

        [Column("Tel", DbType.String,false,false)]
        public String  Tel 
		{ 
		   get{  return _Tel == null ?string.Empty:_Tel;  } 
		   set{_Tel = value;}
		 }
        
		private  String _Mobile;

        [Column("Mobile", DbType.String,false,false)]
        public String  Mobile 
		{ 
		   get{  return _Mobile == null ?string.Empty:_Mobile;  } 
		   set{_Mobile = value;}
		 }
        
		private  String _Code;

        [Column("Code", DbType.String,false,false)]
        public String  Code 
		{ 
		   get{  return _Code == null ?string.Empty:_Code;  } 
		   set{_Code = value;}
		 }
        
		private  String _QQ;

        [Column("QQ", DbType.String,false,false)]
        public String  QQ 
		{ 
		   get{  return _QQ == null ?string.Empty:_QQ;  } 
		   set{_QQ = value;}
		 }
        
		private  String _MSN;

        [Column("MSN", DbType.String,false,false)]
        public String  MSN 
		{ 
		   get{  return _MSN == null ?string.Empty:_MSN;  } 
		   set{_MSN = value;}
		 }
        
		private  String _Email;

        [Column("Email", DbType.String,false,false)]
        public String  Email 
		{ 
		   get{  return _Email == null ?string.Empty:_Email;  } 
		   set{_Email = value;}
		 }
        
		private  String _EmailPass;

        [Column("EmailPass", DbType.String,false,false)]
        public String  EmailPass 
		{ 
		   get{  return _EmailPass == null ?string.Empty:_EmailPass;  } 
		   set{_EmailPass = value;}
		 }
        
		private  String _Description;

        [Column("Description", DbType.String,false,false)]
        public String  Description 
		{ 
		   get{  return _Description == null ?string.Empty:_Description;  } 
		   set{_Description = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
            }


    [Table("Files")]
	[Serializable] 
    public partial class Files :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _CompanyID;

        [Column("CompanyID", DbType.Guid,false,false)]
        public Guid  CompanyID 
		{ 
		   get{ return _CompanyID;  } 
		   set{_CompanyID = value;}
		 }
        
		private  Guid _RelationID;

        [Column("RelationID", DbType.Guid,false,false)]
        public Guid  RelationID 
		{ 
		   get{ return _RelationID;  } 
		   set{_RelationID = value;}
		 }
        
		private  String _Name;

        [Column("Name", DbType.String,false,false)]
        public String  Name 
		{ 
		   get{  return _Name == null ?string.Empty:_Name;  } 
		   set{_Name = value;}
		 }
        
		private  String _media_id;

        [Column("media_id", DbType.String,false,false)]
        public String  media_id 
		{ 
		   get{  return _media_id == null ?string.Empty:_media_id;  } 
		   set{_media_id = value;}
		 }
        
		private  Double _Type;

        [Column("Type", DbType.Double,false,false)]
        public Double  Type 
		{ 
		   get{ return _Type;  } 
		   set{_Type = value;}
		 }
        
		private  String _OName;

        [Column("OName", DbType.String,false,false)]
        public String  OName 
		{ 
		   get{  return _OName == null ?string.Empty:_OName;  } 
		   set{_OName = value;}
		 }
        
		private  String _FilePath;

        [Column("FilePath", DbType.String,false,false)]
        public String  FilePath 
		{ 
		   get{  return _FilePath == null ?string.Empty:_FilePath;  } 
		   set{_FilePath = value;}
		 }
        
		private  String _FileExt;

        [Column("FileExt", DbType.String,false,false)]
        public String  FileExt 
		{ 
		   get{  return _FileExt == null ?string.Empty:_FileExt;  } 
		   set{_FileExt = value;}
		 }
        
		private  Decimal _FileSize;

        [Column("FileSize", DbType.Decimal,false,false)]
        public Decimal  FileSize 
		{ 
		   get{ return _FileSize;  } 
		   set{_FileSize = value;}
		 }
        
		private  String _Large;

        [Column("Large", DbType.String,false,false)]
        public String  Large 
		{ 
		   get{  return _Large == null ?string.Empty:_Large;  } 
		   set{_Large = value;}
		 }
        
		private  String _Middle;

        [Column("Middle", DbType.String,false,false)]
        public String  Middle 
		{ 
		   get{  return _Middle == null ?string.Empty:_Middle;  } 
		   set{_Middle = value;}
		 }
        
		private  String _Small;

        [Column("Small", DbType.String,false,false)]
        public String  Small 
		{ 
		   get{  return _Small == null ?string.Empty:_Small;  } 
		   set{_Small = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  String _Description;

        [Column("Description", DbType.String,false,false)]
        public String  Description 
		{ 
		   get{  return _Description == null ?string.Empty:_Description;  } 
		   set{_Description = value;}
		 }
            }


    [Table("Messages")]
	[Serializable] 
    public partial class Messages :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Int32 _Type;

        [Column("Type", DbType.Int32,false,false)]
        public Int32  Type 
		{ 
		   get{ return _Type;  } 
		   set{_Type = value;}
		 }
        
		private  Guid _RelationID;

        [Column("RelationID", DbType.Guid,false,false)]
        public Guid  RelationID 
		{ 
		   get{ return _RelationID;  } 
		   set{_RelationID = value;}
		 }
        
		private  Guid _CompanyID;

        [Column("CompanyID", DbType.Guid,false,false)]
        public Guid  CompanyID 
		{ 
		   get{ return _CompanyID;  } 
		   set{_CompanyID = value;}
		 }
        
		private  Guid _UserID;

        [Column("UserID", DbType.Guid,false,false)]
        public Guid  UserID 
		{ 
		   get{ return _UserID;  } 
		   set{_UserID = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  String _Content;

        [Column("Content", DbType.String,false,false)]
        public String  Content 
		{ 
		   get{  return _Content == null ?string.Empty:_Content;  } 
		   set{_Content = value;}
		 }
        
		private  Int32 _Count;

        [Column("Count", DbType.Int32,false,false)]
        public Int32  Count 
		{ 
		   get{ return _Count;  } 
		   set{_Count = value;}
		 }
        
		private  String _Name;

        [Column("Name", DbType.String,false,false)]
        public String  Name 
		{ 
		   get{  return _Name == null ?string.Empty:_Name;  } 
		   set{_Name = value;}
		 }
        
		private  String _Email;

        [Column("Email", DbType.String,false,false)]
        public String  Email 
		{ 
		   get{  return _Email == null ?string.Empty:_Email;  } 
		   set{_Email = value;}
		 }
        
		private  String _Phone;

        [Column("Phone", DbType.String,false,false)]
        public String  Phone 
		{ 
		   get{  return _Phone == null ?string.Empty:_Phone;  } 
		   set{_Phone = value;}
		 }
            }


    [Table("Messages_Reply")]
	[Serializable] 
    public partial class Messages_Reply :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _UserID;

        [Column("UserID", DbType.Guid,false,false)]
        public Guid  UserID 
		{ 
		   get{ return _UserID;  } 
		   set{_UserID = value;}
		 }
        
		private  String _Content;

        [Column("Content", DbType.String,false,false)]
        public String  Content 
		{ 
		   get{  return _Content == null ?string.Empty:_Content;  } 
		   set{_Content = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  Guid _MessageID;

        [Column("MessageID", DbType.Guid,false,false)]
        public Guid  MessageID 
		{ 
		   get{ return _MessageID;  } 
		   set{_MessageID = value;}
		 }
        
		private  Guid _ParentID;

        [Column("ParentID", DbType.Guid,false,false)]
        public Guid  ParentID 
		{ 
		   get{ return _ParentID;  } 
		   set{_ParentID = value;}
		 }
            }


    [Table("Messages_ReplyDetails")]
	[Serializable] 
    public partial class Messages_ReplyDetails :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,false,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _UserID;

        [Column("UserID", DbType.Guid,false,false)]
        public Guid  UserID 
		{ 
		   get{ return _UserID;  } 
		   set{_UserID = value;}
		 }
        
		private  String _Content;

        [Column("Content", DbType.String,false,false)]
        public String  Content 
		{ 
		   get{  return _Content == null ?string.Empty:_Content;  } 
		   set{_Content = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  Guid _MessageID;

        [Column("MessageID", DbType.Guid,false,false)]
        public Guid  MessageID 
		{ 
		   get{ return _MessageID;  } 
		   set{_MessageID = value;}
		 }
        
		private  Guid _ParentID;

        [Column("ParentID", DbType.Guid,false,false)]
        public Guid  ParentID 
		{ 
		   get{ return _ParentID;  } 
		   set{_ParentID = value;}
		 }
        
		private  String _Pic;

        [Column("Pic", DbType.String,false,false)]
        public String  Pic 
		{ 
		   get{  return _Pic == null ?string.Empty:_Pic;  } 
		   set{_Pic = value;}
		 }
        
		private  String _Name;

        [Column("Name", DbType.String,false,false)]
        public String  Name 
		{ 
		   get{  return _Name == null ?string.Empty:_Name;  } 
		   set{_Name = value;}
		 }
        
		private  Boolean _Sex;

        [Column("Sex", DbType.Boolean,false,false)]
        public Boolean  Sex 
		{ 
		   get{ return _Sex;  } 
		   set{_Sex = value;}
		 }
            }


    [Table("MessagesDetails")]
	[Serializable] 
    public partial class MessagesDetails :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,false,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Int32 _Type;

        [Column("Type", DbType.Int32,false,false)]
        public Int32  Type 
		{ 
		   get{ return _Type;  } 
		   set{_Type = value;}
		 }
        
		private  Guid _RelationID;

        [Column("RelationID", DbType.Guid,false,false)]
        public Guid  RelationID 
		{ 
		   get{ return _RelationID;  } 
		   set{_RelationID = value;}
		 }
        
		private  Guid _CompanyID;

        [Column("CompanyID", DbType.Guid,false,false)]
        public Guid  CompanyID 
		{ 
		   get{ return _CompanyID;  } 
		   set{_CompanyID = value;}
		 }
        
		private  Guid _UserID;

        [Column("UserID", DbType.Guid,false,false)]
        public Guid  UserID 
		{ 
		   get{ return _UserID;  } 
		   set{_UserID = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  String _Content;

        [Column("Content", DbType.String,false,false)]
        public String  Content 
		{ 
		   get{  return _Content == null ?string.Empty:_Content;  } 
		   set{_Content = value;}
		 }
        
		private  Int32 _Count;

        [Column("Count", DbType.Int32,false,false)]
        public Int32  Count 
		{ 
		   get{ return _Count;  } 
		   set{_Count = value;}
		 }
        
		private  String _Name;

        [Column("Name", DbType.String,false,false)]
        public String  Name 
		{ 
		   get{  return _Name == null ?string.Empty:_Name;  } 
		   set{_Name = value;}
		 }
        
		private  String _Email;

        [Column("Email", DbType.String,false,false)]
        public String  Email 
		{ 
		   get{  return _Email == null ?string.Empty:_Email;  } 
		   set{_Email = value;}
		 }
        
		private  String _Phone;

        [Column("Phone", DbType.String,false,false)]
        public String  Phone 
		{ 
		   get{  return _Phone == null ?string.Empty:_Phone;  } 
		   set{_Phone = value;}
		 }
        
		private  String _Pic;

        [Column("Pic", DbType.String,false,false)]
        public String  Pic 
		{ 
		   get{  return _Pic == null ?string.Empty:_Pic;  } 
		   set{_Pic = value;}
		 }
            }


    [Table("News")]
	[Serializable] 
    public partial class News :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _ClassID;

        [Column("ClassID", DbType.Guid,false,false)]
        public Guid  ClassID 
		{ 
		   get{ return _ClassID;  } 
		   set{_ClassID = value;}
		 }
        
		private  Int32 _Type;

        [Column("Type", DbType.Int32,false,false)]
        public Int32  Type 
		{ 
		   get{ return _Type;  } 
		   set{_Type = value;}
		 }
        
		private  String _Title;

        [Column("Title", DbType.String,false,false)]
        public String  Title 
		{ 
		   get{  return _Title == null ?string.Empty:_Title;  } 
		   set{_Title = value;}
		 }
        
		private  String _STitle;

        [Column("STitle", DbType.String,false,false)]
        public String  STitle 
		{ 
		   get{  return _STitle == null ?string.Empty:_STitle;  } 
		   set{_STitle = value;}
		 }
        
		private  String _Author;

        [Column("Author", DbType.String,false,false)]
        public String  Author 
		{ 
		   get{  return _Author == null ?string.Empty:_Author;  } 
		   set{_Author = value;}
		 }
        
		private  String _Souce;

        [Column("Souce", DbType.String,false,false)]
        public String  Souce 
		{ 
		   get{  return _Souce == null ?string.Empty:_Souce;  } 
		   set{_Souce = value;}
		 }
        
		private  String _Tags;

        [Column("Tags", DbType.String,false,false)]
        public String  Tags 
		{ 
		   get{  return _Tags == null ?string.Empty:_Tags;  } 
		   set{_Tags = value;}
		 }
        
		private  String _NaviContent;

        [Column("NaviContent", DbType.String,false,false)]
        public String  NaviContent 
		{ 
		   get{  return _NaviContent == null ?string.Empty:_NaviContent;  } 
		   set{_NaviContent = value;}
		 }
        
		private  String _Description;

        [Column("Description", DbType.String,false,false)]
        public String  Description 
		{ 
		   get{  return _Description == null ?string.Empty:_Description;  } 
		   set{_Description = value;}
		 }
        
		private  String _Metakeywords;

        [Column("Metakeywords", DbType.String,false,false)]
        public String  Metakeywords 
		{ 
		   get{  return _Metakeywords == null ?string.Empty:_Metakeywords;  } 
		   set{_Metakeywords = value;}
		 }
        
		private  String _Metadesc;

        [Column("Metadesc", DbType.String,false,false)]
        public String  Metadesc 
		{ 
		   get{  return _Metadesc == null ?string.Empty:_Metadesc;  } 
		   set{_Metadesc = value;}
		 }
        
		private  String _SEOTitle;

        [Column("SEOTitle", DbType.String,false,false)]
        public String  SEOTitle 
		{ 
		   get{  return _SEOTitle == null ?string.Empty:_SEOTitle;  } 
		   set{_SEOTitle = value;}
		 }
        
		private  String _SEOTags;

        [Column("SEOTags", DbType.String,false,false)]
        public String  SEOTags 
		{ 
		   get{  return _SEOTags == null ?string.Empty:_SEOTags;  } 
		   set{_SEOTags = value;}
		 }
        
		private  Int32 _Count;

        [Column("Count", DbType.Int32,false,false)]
        public Int32  Count 
		{ 
		   get{ return _Count;  } 
		   set{_Count = value;}
		 }
        
		private  Boolean _IsAudit;

        [Column("IsAudit", DbType.Boolean,false,false)]
        public Boolean  IsAudit 
		{ 
		   get{ return _IsAudit;  } 
		   set{_IsAudit = value;}
		 }
        
		private  Boolean _IsTop;

        [Column("IsTop", DbType.Boolean,false,false)]
        public Boolean  IsTop 
		{ 
		   get{ return _IsTop;  } 
		   set{_IsTop = value;}
		 }
        
		private  Boolean _IsRecommend;

        [Column("IsRecommend", DbType.Boolean,false,false)]
        public Boolean  IsRecommend 
		{ 
		   get{ return _IsRecommend;  } 
		   set{_IsRecommend = value;}
		 }
        
		private  Boolean _IsComm;

        [Column("IsComm", DbType.Boolean,false,false)]
        public Boolean  IsComm 
		{ 
		   get{ return _IsComm;  } 
		   set{_IsComm = value;}
		 }
        
		private  Boolean _IsVote;

        [Column("IsVote", DbType.Boolean,false,false)]
        public Boolean  IsVote 
		{ 
		   get{ return _IsVote;  } 
		   set{_IsVote = value;}
		 }
        
		private  String _SlidePic;

        [Column("SlidePic", DbType.String,false,false)]
        public String  SlidePic 
		{ 
		   get{  return _SlidePic == null ?string.Empty:_SlidePic;  } 
		   set{_SlidePic = value;}
		 }
        
		private  String _HomePic;

        [Column("HomePic", DbType.String,false,false)]
        public String  HomePic 
		{ 
		   get{  return _HomePic == null ?string.Empty:_HomePic;  } 
		   set{_HomePic = value;}
		 }
        
		private  String _EditorRec;

        [Column("EditorRec", DbType.String,false,false)]
        public String  EditorRec 
		{ 
		   get{  return _EditorRec == null ?string.Empty:_EditorRec;  } 
		   set{_EditorRec = value;}
		 }
        
		private  String _Remark;

        [Column("Remark", DbType.String,false,false)]
        public String  Remark 
		{ 
		   get{  return _Remark == null ?string.Empty:_Remark;  } 
		   set{_Remark = value;}
		 }
        
		private  Int32 _status;

        [Column("status", DbType.Int32,false,false)]
        public Int32  status 
		{ 
		   get{ return _status;  } 
		   set{_status = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  DateTime _ModifyTime;

        [Column("ModifyTime", DbType.DateTime,false,false)]
        public DateTime  ModifyTime 
		{ 
		   get{ return _ModifyTime;  } 
		   set{_ModifyTime = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
            }


    [Table("NewsDetails")]
	[Serializable] 
    public partial class NewsDetails :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,false,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _ClassID;

        [Column("ClassID", DbType.Guid,false,false)]
        public Guid  ClassID 
		{ 
		   get{ return _ClassID;  } 
		   set{_ClassID = value;}
		 }
        
		private  Int32 _Type;

        [Column("Type", DbType.Int32,false,false)]
        public Int32  Type 
		{ 
		   get{ return _Type;  } 
		   set{_Type = value;}
		 }
        
		private  String _Title;

        [Column("Title", DbType.String,false,false)]
        public String  Title 
		{ 
		   get{  return _Title == null ?string.Empty:_Title;  } 
		   set{_Title = value;}
		 }
        
		private  String _STitle;

        [Column("STitle", DbType.String,false,false)]
        public String  STitle 
		{ 
		   get{  return _STitle == null ?string.Empty:_STitle;  } 
		   set{_STitle = value;}
		 }
        
		private  String _Author;

        [Column("Author", DbType.String,false,false)]
        public String  Author 
		{ 
		   get{  return _Author == null ?string.Empty:_Author;  } 
		   set{_Author = value;}
		 }
        
		private  String _Souce;

        [Column("Souce", DbType.String,false,false)]
        public String  Souce 
		{ 
		   get{  return _Souce == null ?string.Empty:_Souce;  } 
		   set{_Souce = value;}
		 }
        
		private  String _Tags;

        [Column("Tags", DbType.String,false,false)]
        public String  Tags 
		{ 
		   get{  return _Tags == null ?string.Empty:_Tags;  } 
		   set{_Tags = value;}
		 }
        
		private  String _NaviContent;

        [Column("NaviContent", DbType.String,false,false)]
        public String  NaviContent 
		{ 
		   get{  return _NaviContent == null ?string.Empty:_NaviContent;  } 
		   set{_NaviContent = value;}
		 }
        
		private  String _Description;

        [Column("Description", DbType.String,false,false)]
        public String  Description 
		{ 
		   get{  return _Description == null ?string.Empty:_Description;  } 
		   set{_Description = value;}
		 }
        
		private  String _Metakeywords;

        [Column("Metakeywords", DbType.String,false,false)]
        public String  Metakeywords 
		{ 
		   get{  return _Metakeywords == null ?string.Empty:_Metakeywords;  } 
		   set{_Metakeywords = value;}
		 }
        
		private  String _Metadesc;

        [Column("Metadesc", DbType.String,false,false)]
        public String  Metadesc 
		{ 
		   get{  return _Metadesc == null ?string.Empty:_Metadesc;  } 
		   set{_Metadesc = value;}
		 }
        
		private  String _SEOTitle;

        [Column("SEOTitle", DbType.String,false,false)]
        public String  SEOTitle 
		{ 
		   get{  return _SEOTitle == null ?string.Empty:_SEOTitle;  } 
		   set{_SEOTitle = value;}
		 }
        
		private  String _SEOTags;

        [Column("SEOTags", DbType.String,false,false)]
        public String  SEOTags 
		{ 
		   get{  return _SEOTags == null ?string.Empty:_SEOTags;  } 
		   set{_SEOTags = value;}
		 }
        
		private  Int32 _Count;

        [Column("Count", DbType.Int32,false,false)]
        public Int32  Count 
		{ 
		   get{ return _Count;  } 
		   set{_Count = value;}
		 }
        
		private  Boolean _IsAudit;

        [Column("IsAudit", DbType.Boolean,false,false)]
        public Boolean  IsAudit 
		{ 
		   get{ return _IsAudit;  } 
		   set{_IsAudit = value;}
		 }
        
		private  Boolean _IsTop;

        [Column("IsTop", DbType.Boolean,false,false)]
        public Boolean  IsTop 
		{ 
		   get{ return _IsTop;  } 
		   set{_IsTop = value;}
		 }
        
		private  Boolean _IsRecommend;

        [Column("IsRecommend", DbType.Boolean,false,false)]
        public Boolean  IsRecommend 
		{ 
		   get{ return _IsRecommend;  } 
		   set{_IsRecommend = value;}
		 }
        
		private  Boolean _IsComm;

        [Column("IsComm", DbType.Boolean,false,false)]
        public Boolean  IsComm 
		{ 
		   get{ return _IsComm;  } 
		   set{_IsComm = value;}
		 }
        
		private  Boolean _IsVote;

        [Column("IsVote", DbType.Boolean,false,false)]
        public Boolean  IsVote 
		{ 
		   get{ return _IsVote;  } 
		   set{_IsVote = value;}
		 }
        
		private  String _SlidePic;

        [Column("SlidePic", DbType.String,false,false)]
        public String  SlidePic 
		{ 
		   get{  return _SlidePic == null ?string.Empty:_SlidePic;  } 
		   set{_SlidePic = value;}
		 }
        
		private  String _HomePic;

        [Column("HomePic", DbType.String,false,false)]
        public String  HomePic 
		{ 
		   get{  return _HomePic == null ?string.Empty:_HomePic;  } 
		   set{_HomePic = value;}
		 }
        
		private  String _EditorRec;

        [Column("EditorRec", DbType.String,false,false)]
        public String  EditorRec 
		{ 
		   get{  return _EditorRec == null ?string.Empty:_EditorRec;  } 
		   set{_EditorRec = value;}
		 }
        
		private  String _Remark;

        [Column("Remark", DbType.String,false,false)]
        public String  Remark 
		{ 
		   get{  return _Remark == null ?string.Empty:_Remark;  } 
		   set{_Remark = value;}
		 }
        
		private  Int32 _status;

        [Column("status", DbType.Int32,false,false)]
        public Int32  status 
		{ 
		   get{ return _status;  } 
		   set{_status = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  DateTime _ModifyTime;

        [Column("ModifyTime", DbType.DateTime,false,false)]
        public DateTime  ModifyTime 
		{ 
		   get{ return _ModifyTime;  } 
		   set{_ModifyTime = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Guid _CompanyID;

        [Column("CompanyID", DbType.Guid,false,false)]
        public Guid  CompanyID 
		{ 
		   get{ return _CompanyID;  } 
		   set{_CompanyID = value;}
		 }
            }


    [Table("Product")]
	[Serializable] 
    public partial class Product :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _ClassID;

        [Column("ClassID", DbType.Guid,false,false)]
        public Guid  ClassID 
		{ 
		   get{ return _ClassID;  } 
		   set{_ClassID = value;}
		 }
        
		private  String _Title;

        [Column("Title", DbType.String,false,false)]
        public String  Title 
		{ 
		   get{  return _Title == null ?string.Empty:_Title;  } 
		   set{_Title = value;}
		 }
        
		private  Int32 _Numbers;

        [Column("Numbers", DbType.Int32,false,false)]
        public Int32  Numbers 
		{ 
		   get{ return _Numbers;  } 
		   set{_Numbers = value;}
		 }
        
		private  Decimal _Price;

        [Column("Price", DbType.Decimal,false,false)]
        public Decimal  Price 
		{ 
		   get{ return _Price;  } 
		   set{_Price = value;}
		 }
        
		private  String _Description;

        [Column("Description", DbType.String,false,false)]
        public String  Description 
		{ 
		   get{  return _Description == null ?string.Empty:_Description;  } 
		   set{_Description = value;}
		 }
        
		private  String _Author;

        [Column("Author", DbType.String,false,false)]
        public String  Author 
		{ 
		   get{  return _Author == null ?string.Empty:_Author;  } 
		   set{_Author = value;}
		 }
        
		private  String _Souce;

        [Column("Souce", DbType.String,false,false)]
        public String  Souce 
		{ 
		   get{  return _Souce == null ?string.Empty:_Souce;  } 
		   set{_Souce = value;}
		 }
        
		private  String _Tags;

        [Column("Tags", DbType.String,false,false)]
        public String  Tags 
		{ 
		   get{  return _Tags == null ?string.Empty:_Tags;  } 
		   set{_Tags = value;}
		 }
        
		private  String _NaviContent;

        [Column("NaviContent", DbType.String,false,false)]
        public String  NaviContent 
		{ 
		   get{  return _NaviContent == null ?string.Empty:_NaviContent;  } 
		   set{_NaviContent = value;}
		 }
        
		private  String _Metakeywords;

        [Column("Metakeywords", DbType.String,false,false)]
        public String  Metakeywords 
		{ 
		   get{  return _Metakeywords == null ?string.Empty:_Metakeywords;  } 
		   set{_Metakeywords = value;}
		 }
        
		private  String _Metadesc;

        [Column("Metadesc", DbType.String,false,false)]
        public String  Metadesc 
		{ 
		   get{  return _Metadesc == null ?string.Empty:_Metadesc;  } 
		   set{_Metadesc = value;}
		 }
        
		private  Int32 _Count;

        [Column("Count", DbType.Int32,false,false)]
        public Int32  Count 
		{ 
		   get{ return _Count;  } 
		   set{_Count = value;}
		 }
        
		private  Boolean _IsAudit;

        [Column("IsAudit", DbType.Boolean,false,false)]
        public Boolean  IsAudit 
		{ 
		   get{ return _IsAudit;  } 
		   set{_IsAudit = value;}
		 }
        
		private  Boolean _IsTop;

        [Column("IsTop", DbType.Boolean,false,false)]
        public Boolean  IsTop 
		{ 
		   get{ return _IsTop;  } 
		   set{_IsTop = value;}
		 }
        
		private  Boolean _IsRecommend;

        [Column("IsRecommend", DbType.Boolean,false,false)]
        public Boolean  IsRecommend 
		{ 
		   get{ return _IsRecommend;  } 
		   set{_IsRecommend = value;}
		 }
        
		private  String _SlidePic;

        [Column("SlidePic", DbType.String,false,false)]
        public String  SlidePic 
		{ 
		   get{  return _SlidePic == null ?string.Empty:_SlidePic;  } 
		   set{_SlidePic = value;}
		 }
        
		private  String _EditorRec;

        [Column("EditorRec", DbType.String,false,false)]
        public String  EditorRec 
		{ 
		   get{  return _EditorRec == null ?string.Empty:_EditorRec;  } 
		   set{_EditorRec = value;}
		 }
        
		private  String _itemid;

        [Column("itemid", DbType.String,false,false)]
        public String  itemid 
		{ 
		   get{  return _itemid == null ?string.Empty:_itemid;  } 
		   set{_itemid = value;}
		 }
        
		private  String _opt;

        [Column("opt", DbType.String,false,false)]
        public String  opt 
		{ 
		   get{  return _opt == null ?string.Empty:_opt;  } 
		   set{_opt = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  Int32 _status;

        [Column("status", DbType.Int32,false,false)]
        public Int32  status 
		{ 
		   get{ return _status;  } 
		   set{_status = value;}
		 }
        
		private  DateTime _ModifyTime;

        [Column("ModifyTime", DbType.DateTime,false,false)]
        public DateTime  ModifyTime 
		{ 
		   get{ return _ModifyTime;  } 
		   set{_ModifyTime = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
            }


    [Table("Product_Att_Key")]
	[Serializable] 
    public partial class Product_Att_Key :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _CompanyID;

        [Column("CompanyID", DbType.Guid,false,false)]
        public Guid  CompanyID 
		{ 
		   get{ return _CompanyID;  } 
		   set{_CompanyID = value;}
		 }
        
		private  String _Name;

        [Column("Name", DbType.String,false,false)]
        public String  Name 
		{ 
		   get{  return _Name == null ?string.Empty:_Name;  } 
		   set{_Name = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
            }


    [Table("Product_Att_Val")]
	[Serializable] 
    public partial class Product_Att_Val :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _ProductID;

        [Column("ProductID", DbType.Guid,false,false)]
        public Guid  ProductID 
		{ 
		   get{ return _ProductID;  } 
		   set{_ProductID = value;}
		 }
        
		private  Guid _Att_Key_ID;

        [Column("Att_Key_ID", DbType.Guid,false,false)]
        public Guid  Att_Key_ID 
		{ 
		   get{ return _Att_Key_ID;  } 
		   set{_Att_Key_ID = value;}
		 }
        
		private  Int32 _Short;

        [Column("Short", DbType.Int32,false,false)]
        public Int32  Short 
		{ 
		   get{ return _Short;  } 
		   set{_Short = value;}
		 }
        
		private  String _Value;

        [Column("Value", DbType.String,false,false)]
        public String  Value 
		{ 
		   get{  return _Value == null ?string.Empty:_Value;  } 
		   set{_Value = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
            }


    [Table("Product_Price")]
	[Serializable] 
    public partial class Product_Price :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _ProductID;

        [Column("ProductID", DbType.Guid,false,false)]
        public Guid  ProductID 
		{ 
		   get{ return _ProductID;  } 
		   set{_ProductID = value;}
		 }
        
		private  Guid _Att_Key;

        [Column("Att_Key", DbType.Guid,false,false)]
        public Guid  Att_Key 
		{ 
		   get{ return _Att_Key;  } 
		   set{_Att_Key = value;}
		 }
        
		private  Guid _Att_Val;

        [Column("Att_Val", DbType.Guid,false,false)]
        public Guid  Att_Val 
		{ 
		   get{ return _Att_Val;  } 
		   set{_Att_Val = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Decimal _Price;

        [Column("Price", DbType.Decimal,false,false)]
        public Decimal  Price 
		{ 
		   get{ return _Price;  } 
		   set{_Price = value;}
		 }
        
		private  Int32 _Stock;

        [Column("Stock", DbType.Int32,false,false)]
        public Int32  Stock 
		{ 
		   get{ return _Stock;  } 
		   set{_Stock = value;}
		 }
        
		private  Int64 _DR;

        [Column("DR", DbType.Int64,false,false)]
        public Int64  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
            }


    [Table("Role")]
	[Serializable] 
    public partial class Role :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _PID;

        [Column("PID", DbType.Guid,false,false)]
        public Guid  PID 
		{ 
		   get{ return _PID;  } 
		   set{_PID = value;}
		 }
        
		private  String _Name;

        [Column("Name", DbType.String,false,false)]
        public String  Name 
		{ 
		   get{  return _Name == null ?string.Empty:_Name;  } 
		   set{_Name = value;}
		 }
        
		private  String _Description;

        [Column("Description", DbType.String,false,false)]
        public String  Description 
		{ 
		   get{  return _Description == null ?string.Empty:_Description;  } 
		   set{_Description = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
            }


    [Table("Role_PK_Authority_s")]
	[Serializable] 
    public partial class Role_PK_Authority_s :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _Role_ID;

        [Column("Role_ID", DbType.Guid,false,false)]
        public Guid  Role_ID 
		{ 
		   get{ return _Role_ID;  } 
		   set{_Role_ID = value;}
		 }
        
		private  Guid _Authority_ID;

        [Column("Authority_ID", DbType.Guid,false,false)]
        public Guid  Authority_ID 
		{ 
		   get{ return _Authority_ID;  } 
		   set{_Authority_ID = value;}
		 }
            }


    [Table("USER")]
	[Serializable] 
    public partial class USER :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  String _LoginName;

        [Column("LoginName", DbType.String,false,false)]
        public String  LoginName 
		{ 
		   get{  return _LoginName == null ?string.Empty:_LoginName;  } 
		   set{_LoginName = value;}
		 }
        
		private  String _LoginPwd;

        [Column("LoginPwd", DbType.String,false,false)]
        public String  LoginPwd 
		{ 
		   get{  return _LoginPwd == null ?string.Empty:_LoginPwd;  } 
		   set{_LoginPwd = value;}
		 }
        
		private  String _Name;

        [Column("Name", DbType.String,false,false)]
        public String  Name 
		{ 
		   get{  return _Name == null ?string.Empty:_Name;  } 
		   set{_Name = value;}
		 }
        
		private  Int32 _Type;

        [Column("Type", DbType.Int32,false,false)]
        public Int32  Type 
		{ 
		   get{ return _Type;  } 
		   set{_Type = value;}
		 }
        
		private  Boolean _Sex;

        [Column("Sex", DbType.Boolean,false,false)]
        public Boolean  Sex 
		{ 
		   get{ return _Sex;  } 
		   set{_Sex = value;}
		 }
        
		private  String _PIC;

        [Column("PIC", DbType.String,false,false)]
        public String  PIC 
		{ 
		   get{  return _PIC == null ?string.Empty:_PIC;  } 
		   set{_PIC = value;}
		 }
        
		private  String _Telphone;

        [Column("Telphone", DbType.String,false,false)]
        public String  Telphone 
		{ 
		   get{  return _Telphone == null ?string.Empty:_Telphone;  } 
		   set{_Telphone = value;}
		 }
        
		private  String _Email;

        [Column("Email", DbType.String,false,false)]
        public String  Email 
		{ 
		   get{  return _Email == null ?string.Empty:_Email;  } 
		   set{_Email = value;}
		 }
        
		private  Int32 _Count;

        [Column("Count", DbType.Int32,false,false)]
        public Int32  Count 
		{ 
		   get{ return _Count;  } 
		   set{_Count = value;}
		 }
        
		private  DateTime _Last_Login_Time;

        [Column("Last_Login_Time", DbType.DateTime,false,false)]
        public DateTime  Last_Login_Time 
		{ 
		   get{ return _Last_Login_Time;  } 
		   set{_Last_Login_Time = value;}
		 }
        
		private  Guid _CompanyID;

        [Column("CompanyID", DbType.Guid,false,false)]
        public Guid  CompanyID 
		{ 
		   get{ return _CompanyID;  } 
		   set{_CompanyID = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
            }


    [Table("User_PK_Role_s")]
	[Serializable] 
    public partial class User_PK_Role_s :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _User_ID;

        [Column("User_ID", DbType.Guid,false,false)]
        public Guid  User_ID 
		{ 
		   get{ return _User_ID;  } 
		   set{_User_ID = value;}
		 }
        
		private  Guid _Role_ID;

        [Column("Role_ID", DbType.Guid,false,false)]
        public Guid  Role_ID 
		{ 
		   get{ return _Role_ID;  } 
		   set{_Role_ID = value;}
		 }
            }


    [Table("WeShop")]
	[Serializable] 
    public partial class WeShop :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _CompanyID;

        [Column("CompanyID", DbType.Guid,false,false)]
        public Guid  CompanyID 
		{ 
		   get{ return _CompanyID;  } 
		   set{_CompanyID = value;}
		 }
        
		private  String _appkey;

        [Column("appkey", DbType.String,false,false)]
        public String  appkey 
		{ 
		   get{  return _appkey == null ?string.Empty:_appkey;  } 
		   set{_appkey = value;}
		 }
        
		private  String _secret;

        [Column("secret", DbType.String,false,false)]
        public String  secret 
		{ 
		   get{  return _secret == null ?string.Empty:_secret;  } 
		   set{_secret = value;}
		 }
        
		private  String _access_token;

        [Column("access_token", DbType.String,false,false)]
        public String  access_token 
		{ 
		   get{  return _access_token == null ?string.Empty:_access_token;  } 
		   set{_access_token = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
            }


    [Table("WX_Config")]
	[Serializable] 
    public partial class WX_Config :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  String _OrgID;

        [Column("OrgID", DbType.String,false,false)]
        public String  OrgID 
		{ 
		   get{  return _OrgID == null ?string.Empty:_OrgID;  } 
		   set{_OrgID = value;}
		 }
        
		private  String _Token;

        [Column("Token", DbType.String,false,false)]
        public String  Token 
		{ 
		   get{  return _Token == null ?string.Empty:_Token;  } 
		   set{_Token = value;}
		 }
        
		private  String _AppID;

        [Column("AppID", DbType.String,false,false)]
        public String  AppID 
		{ 
		   get{  return _AppID == null ?string.Empty:_AppID;  } 
		   set{_AppID = value;}
		 }
        
		private  String _EncodingAESKey;

        [Column("EncodingAESKey", DbType.String,false,false)]
        public String  EncodingAESKey 
		{ 
		   get{  return _EncodingAESKey == null ?string.Empty:_EncodingAESKey;  } 
		   set{_EncodingAESKey = value;}
		 }
        
		private  String _access_token;

        [Column("access_token", DbType.String,false,false)]
        public String  access_token 
		{ 
		   get{  return _access_token == null ?string.Empty:_access_token;  } 
		   set{_access_token = value;}
		 }
        
		private  String _AppSecret;

        [Column("AppSecret", DbType.String,false,false)]
        public String  AppSecret 
		{ 
		   get{  return _AppSecret == null ?string.Empty:_AppSecret;  } 
		   set{_AppSecret = value;}
		 }
        
		private  Guid _CompanyID;

        [Column("CompanyID", DbType.Guid,false,false)]
        public Guid  CompanyID 
		{ 
		   get{ return _CompanyID;  } 
		   set{_CompanyID = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
            }


    [Table("WX_Fans")]
	[Serializable] 
    public partial class WX_Fans :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _ConpanyID;

        [Column("ConpanyID", DbType.Guid,false,false)]
        public Guid  ConpanyID 
		{ 
		   get{ return _ConpanyID;  } 
		   set{_ConpanyID = value;}
		 }
        
		private  String _OPENID;

        [Column("OPENID", DbType.String,false,false)]
        public String  OPENID 
		{ 
		   get{  return _OPENID == null ?string.Empty:_OPENID;  } 
		   set{_OPENID = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
            }


    [Table("WX_KeyWord")]
	[Serializable] 
    public partial class WX_KeyWord :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _CompanyID;

        [Column("CompanyID", DbType.Guid,false,false)]
        public Guid  CompanyID 
		{ 
		   get{ return _CompanyID;  } 
		   set{_CompanyID = value;}
		 }
        
		private  Guid _ConfigID;

        [Column("ConfigID", DbType.Guid,false,false)]
        public Guid  ConfigID 
		{ 
		   get{ return _ConfigID;  } 
		   set{_ConfigID = value;}
		 }
        
		private  String _KeyWords;

        [Column("KeyWords", DbType.String,false,false)]
        public String  KeyWords 
		{ 
		   get{  return _KeyWords == null ?string.Empty:_KeyWords;  } 
		   set{_KeyWords = value;}
		 }
        
		private  String _Description;

        [Column("Description", DbType.String,false,false)]
        public String  Description 
		{ 
		   get{  return _Description == null ?string.Empty:_Description;  } 
		   set{_Description = value;}
		 }
        
		private  Int32 _Type;

        [Column("Type", DbType.Int32,false,false)]
        public Int32  Type 
		{ 
		   get{ return _Type;  } 
		   set{_Type = value;}
		 }
        
		private  String _Content;

        [Column("Content", DbType.String,false,false)]
        public String  Content 
		{ 
		   get{  return _Content == null ?string.Empty:_Content;  } 
		   set{_Content = value;}
		 }
        
		private  Guid _RelationID;

        [Column("RelationID", DbType.Guid,false,false)]
        public Guid  RelationID 
		{ 
		   get{ return _RelationID;  } 
		   set{_RelationID = value;}
		 }
        
		private  Int32 _Push;

        [Column("Push", DbType.Int32,false,false)]
        public Int32  Push 
		{ 
		   get{ return _Push;  } 
		   set{_Push = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
            }


    [Table("WX_Menu")]
	[Serializable] 
    public partial class WX_Menu :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _ParentID;

        [Column("ParentID", DbType.Guid,false,false)]
        public Guid  ParentID 
		{ 
		   get{ return _ParentID;  } 
		   set{_ParentID = value;}
		 }
        
		private  String _Name;

        [Column("Name", DbType.String,false,false)]
        public String  Name 
		{ 
		   get{  return _Name == null ?string.Empty:_Name;  } 
		   set{_Name = value;}
		 }
        
		private  Int32 _Type;

        [Column("Type", DbType.Int32,false,false)]
        public Int32  Type 
		{ 
		   get{ return _Type;  } 
		   set{_Type = value;}
		 }
        
		private  String _URL;

        [Column("URL", DbType.String,false,false)]
        public String  URL 
		{ 
		   get{  return _URL == null ?string.Empty:_URL;  } 
		   set{_URL = value;}
		 }
        
		private  Guid _KeyWordID;

        [Column("KeyWordID", DbType.Guid,false,false)]
        public Guid  KeyWordID 
		{ 
		   get{ return _KeyWordID;  } 
		   set{_KeyWordID = value;}
		 }
        
		private  Guid _CompanyID;

        [Column("CompanyID", DbType.Guid,false,false)]
        public Guid  CompanyID 
		{ 
		   get{ return _CompanyID;  } 
		   set{_CompanyID = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Int32 _Short;

        [Column("Short", DbType.Int32,false,false)]
        public Int32  Short 
		{ 
		   get{ return _Short;  } 
		   set{_Short = value;}
		 }
            }


    [Table("WX_Message")]
	[Serializable] 
    public partial class WX_Message :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  String _Title;

        [Column("Title", DbType.String,false,false)]
        public String  Title 
		{ 
		   get{  return _Title == null ?string.Empty:_Title;  } 
		   set{_Title = value;}
		 }
        
		private  Guid _CompanyID;

        [Column("CompanyID", DbType.Guid,false,false)]
        public Guid  CompanyID 
		{ 
		   get{ return _CompanyID;  } 
		   set{_CompanyID = value;}
		 }
        
		private  Guid _ConfigID;

        [Column("ConfigID", DbType.Guid,false,false)]
        public Guid  ConfigID 
		{ 
		   get{ return _ConfigID;  } 
		   set{_ConfigID = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Boolean _IsAycnc;

        [Column("IsAycnc", DbType.Boolean,false,false)]
        public Boolean  IsAycnc 
		{ 
		   get{ return _IsAycnc;  } 
		   set{_IsAycnc = value;}
		 }
        
		private  String _media_id;

        [Column("media_id", DbType.String,false,false)]
        public String  media_id 
		{ 
		   get{  return _media_id == null ?string.Empty:_media_id;  } 
		   set{_media_id = value;}
		 }
            }


    [Table("WX_MessageGroup")]
	[Serializable] 
    public partial class WX_MessageGroup :IEntity
    {
        
		private  Guid _ID;

        [Column("ID", DbType.Guid,true,false)]
        public Guid  ID 
		{ 
		   get{ return _ID;  } 
		   set{_ID = value;}
		 }
        
		private  Guid _MessageID;

        [Column("MessageID", DbType.Guid,false,false)]
        public Guid  MessageID 
		{ 
		   get{ return _MessageID;  } 
		   set{_MessageID = value;}
		 }
        
		private  String _Title;

        [Column("Title", DbType.String,false,false)]
        public String  Title 
		{ 
		   get{  return _Title == null ?string.Empty:_Title;  } 
		   set{_Title = value;}
		 }
        
		private  String _Author;

        [Column("Author", DbType.String,false,false)]
        public String  Author 
		{ 
		   get{  return _Author == null ?string.Empty:_Author;  } 
		   set{_Author = value;}
		 }
        
		private  String _URL;

        [Column("URL", DbType.String,false,false)]
        public String  URL 
		{ 
		   get{  return _URL == null ?string.Empty:_URL;  } 
		   set{_URL = value;}
		 }
        
		private  Guid _FilesID;

        [Column("FilesID", DbType.Guid,false,false)]
        public Guid  FilesID 
		{ 
		   get{ return _FilesID;  } 
		   set{_FilesID = value;}
		 }
        
		private  String _ImgUrl;

        [Column("ImgUrl", DbType.String,false,false)]
        public String  ImgUrl 
		{ 
		   get{  return _ImgUrl == null ?string.Empty:_ImgUrl;  } 
		   set{_ImgUrl = value;}
		 }
        
		private  String _Img_media_id;

        [Column("Img_media_id", DbType.String,false,false)]
        public String  Img_media_id 
		{ 
		   get{  return _Img_media_id == null ?string.Empty:_Img_media_id;  } 
		   set{_Img_media_id = value;}
		 }
        
		private  String _Img_type;

        [Column("Img_type", DbType.String,false,false)]
        public String  Img_type 
		{ 
		   get{  return _Img_type == null ?string.Empty:_Img_type;  } 
		   set{_Img_type = value;}
		 }
        
		private  String _Img_created_at;

        [Column("Img_created_at", DbType.String,false,false)]
        public String  Img_created_at 
		{ 
		   get{  return _Img_created_at == null ?string.Empty:_Img_created_at;  } 
		   set{_Img_created_at = value;}
		 }
        
		private  String _Content;

        [Column("Content", DbType.String,false,false)]
        public String  Content 
		{ 
		   get{  return _Content == null ?string.Empty:_Content;  } 
		   set{_Content = value;}
		 }
        
		private  Boolean _DR;

        [Column("DR", DbType.Boolean,false,false)]
        public Boolean  DR 
		{ 
		   get{ return _DR;  } 
		   set{_DR = value;}
		 }
        
		private  DateTime _CreateTime;

        [Column("CreateTime", DbType.DateTime,false,false)]
        public DateTime  CreateTime 
		{ 
		   get{ return _CreateTime;  } 
		   set{_CreateTime = value;}
		 }
        
		private  Int32 _Short;

        [Column("Short", DbType.Int32,false,false)]
        public Int32  Short 
		{ 
		   get{ return _Short;  } 
		   set{_Short = value;}
		 }
            }


}