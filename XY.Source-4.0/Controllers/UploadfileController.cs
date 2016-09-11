using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XY.Source_4._0.Controllers
{
    public class UploadfileController : Controller
    {
        private long upfileSize = 9999999999;
        // public const string fileext = "gif,jpg,jpeg,png,bmp,txt,rar,zip,ppt,pptx,doc,docx,xls,xlsx,swf,flv,pdf";
        public const string fileext = "png,jpg,jpeg,gif,bmp,flv,swf,mkv,avi,rm,rmvb,mpeg,mpg,ogg,ogv,mov,wmv,mp4,webm,mp3,wav,mid,rar,zip,tar,gz,7z,bz2,cab,iso,doc,docx,xls,xlsx,ppt,pptx,pdf,txt,md,xml";
        public const string imageext = "gif,jpg,jpeg,png,bmp";
        public const string voiceext = "amr,mp3";
        public const string videoext = "mp4";
        [HttpPost]
        public void Upfile(string jsonpcallback, string filepath, string type, string forname, string backurl)
        {
            string savePath = filepath;
            string saveUrl = filepath;
            string fileTypes;
            switch (type)
            {
                case "image"://微信图片格式
                    fileTypes = imageext;
                    upfileSize = 2097152;
                    break;
                case "video"://微信视频格式
                    fileTypes = videoext;
                    upfileSize = 10485760;
                    break;
                case "voice"://微信语音格式
                    fileTypes = voiceext;
                    upfileSize = 2097152;
                    break;
                default:
                    fileTypes = fileext;
                    break;
            }
            Hashtable hash = new Hashtable();
            HttpPostedFileBase file = Request.Files["file"];
            if (file == null)
            {
                hash = new Hashtable();
                hash["error"] = 1;
                hash["message"] = "请选择文件";
            }

            string dirPath = Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                DirectoryInfo d = new DirectoryInfo(dirPath);
                d.Create();
            }

            string fileName = file.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));

            if (file.InputStream == null || file.InputStream.Length > upfileSize)
            {
                hash = new Hashtable();
                hash["error"] = 1;
                hash["message"] = "上传文件大小超过限制";
            }
            else if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                hash = new Hashtable();
                hash["error"] = 1;
                hash["message"] = "上传文件扩展名是不允许的扩展名";
            }
            else
            {
                string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
                string filePath = dirPath + newFileName;
                file.SaveAs(filePath);
                hash = new Hashtable();
                hash["error"] = 0;
                hash["message"] = "success";
                hash["newname"] = newFileName;
                hash["oldname"] = fileName;
                hash["savePath"] = savePath;
                hash["websiteurl"] = ConfigurationManager.AppSettings["webSiteUrl"];
            }
            hash["filepath"] = filepath;
            hash["filetype"] = type;
            hash["forname"] = forname;
            string url = ComUrlTxt(hash);
            Response.Write("<script type='text/javascript'>window.location.href='" + backurl + url + "'</script>");

        }

        [HttpPost]
        public Hashtable SyncFile(string filepath, string type, string name)
        {
           // Files dfile = new Files();
            string savePath = filepath;
            string saveUrl = filepath;
            string fileTypes = (type == "image" ? imageext : fileext);
            Hashtable hash = new Hashtable();
            HttpPostedFileBase file = Request.Files[0];
            if (file == null)
            {
                hash = new Hashtable();
                hash["error"] = 1;
                hash["message"] = "请选择文件";
            }

            string dirPath = Server.MapPath(savePath);
            if (!Directory.Exists(dirPath))
            {
                DirectoryInfo d = new DirectoryInfo(dirPath);
                d.Create();
            }

            string fileName = file.FileName;
            string fileExt = Path.GetExtension(fileName).ToLower();

            ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));

            if (file.InputStream == null || file.InputStream.Length > upfileSize)
            {
                hash = new Hashtable();
                hash["error"] = 1;
                hash["message"] = "上传文件大小超过限制";

            }

            if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
            {
                hash = new Hashtable();
                hash["error"] = 1;
                hash["message"] = "上传文件扩展名是不允许的扩展名";
            }
            else
            {
                string newFileName = name;
                string filePath = dirPath + newFileName;
                file.SaveAs(filePath);
            }
            return hash;
        }
        [HttpPost]
        public void UpImage(string id, string filepath)
        {
            string ID = Guid.NewGuid().ToString();
            if (!string.IsNullOrEmpty(id))
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                DateTime dtNow = DateTime.Parse(DateTime.Now.ToString());
                TimeSpan toNow = dtNow.Subtract(dtStart);
                string timeStamp = toNow.Ticks.ToString();
                timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);


                String savePath = "/UPLOAD/HEAD/";
                if (!string.IsNullOrEmpty(filepath))
                    savePath = filepath;
                String savePicName = id;

                String file_src = savePath + savePicName + "_src.jpg";
                String filenameLARGE = savePath + savePicName + "_LARGE.jpg";
                String filenameMID = savePath + savePicName + "_MID.jpg";
                String filenameSMALL = savePath + savePicName + "_SMALL.jpg";

                String pic = Request.Form["pic"];
                String pic1 = Request.Form["pic1"];
                String pic2 = Request.Form["pic2"];
                String pic3 = Request.Form["pic3"];
                if (!System.IO.Directory.Exists(Server.MapPath(savePath)))
                    System.IO.Directory.CreateDirectory(Server.MapPath(savePath));
                //原图
                if (pic.Length == 0)
                {
                }
                else
                {
                    byte[] bytes = Convert.FromBase64String(pic);  //将2进制编码转换为8位无符号整数数组

                    FileStream fs = new FileStream(Server.MapPath(file_src), System.IO.FileMode.Create);
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }

                byte[] bytes1 = Convert.FromBase64String(pic1);  //将2进制编码转换为8位无符号整数数组.
                byte[] bytes2 = Convert.FromBase64String(pic2);  //将2进制编码转换为8位无符号整数数组.
                byte[] bytes3 = Convert.FromBase64String(pic3);  //将2进制编码转换为8位无符号整数数组.



                //图1
                FileStream fs1 = new FileStream(Server.MapPath(filenameLARGE), System.IO.FileMode.Create);
                fs1.Write(bytes1, 0, bytes1.Length);

                fs1.Close();

                //图2
                FileStream fs2 = new FileStream(Server.MapPath(filenameMID), System.IO.FileMode.Create);
                fs2.Write(bytes2, 0, bytes2.Length);
                fs2.Close();

                //图3
                FileStream fs3 = new FileStream(Server.MapPath(filenameSMALL), System.IO.FileMode.Create);
                fs3.Write(bytes3, 0, bytes3.Length);
                fs3.Close();

                String picUrl = savePath + savePicName;

                Response.Write("{\"status\":1,");
                Response.Write("\"picUrl\":\"" + picUrl + "\"}");
            }
            else
            {
                Response.Write("{\"status\":-1,");
            }
        }
        public static string ComUrlTxt(Hashtable data)
        {
            string result = "";
            foreach (DictionaryEntry item in data)
            {
                result += "&" + item.Key + "=" + item.Value;
            }
            return result.Substring(1);
        }

    }
}