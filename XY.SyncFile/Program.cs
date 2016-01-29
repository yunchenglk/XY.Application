using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;


namespace XY.SyncFile
{
    class Program
    {
        static string str = System.Configuration.ConfigurationManager.AppSettings["localhostPath"];
        static Boolean isUsing = true;
        static void Main(string[] args)
        {
            //fun();


            System.Threading.Thread thread = new System.Threading.Thread(fun);
            thread.Start();
            thread.Name = "uploadFile";
            while (true)
            {
                IList<FileInfo> lst = GetFiles(str);
                if (lst.Count > 0 && isUsing)
                {
                    isUsing = false;
                    fun();
                }
                else {
                    // Console.WriteLine("xiuxi");
                    System.Threading.Thread.Sleep(1000);
                }
            }
        }
        public static void fun()
        {
            Console.WriteLine("检索文件");
            Console.WriteLine("路径是：" + str);
            if (!str.EndsWith("\\"))
            {
                str += "\\";
            }
            IList<FileInfo> lst = GetFiles(str);
            Console.WriteLine(string.Format("检索到{0}个文件", lst.Count));
            foreach (var item in lst)
            {
                Uri url = new Uri(System.Configuration.ConfigurationManager.AppSettings["ServiceURL"]);
                IList<HttpPost.UploadFile> files = new List<HttpPost.UploadFile>() {
                    new HttpPost.UploadFile {
                                    ContentType = item.Extension,
                                    Data = System.IO.File.ReadAllBytes(item.FullName),
                                    Filename = item.Name,
                                    Name = item.FullName
                } };
                NameValueCollection collection = new NameValueCollection();
                collection.Add("filepath", GetServicePath(item.FullName));
                collection.Add("type", item.Extension);
                collection.Add("name", item.Name);
                Console.WriteLine(string.Format("开始上传文件:{0}", item.FullName));
                if (HttpPost.Post(url, files, collection))
                {
                    Console.WriteLine("文件上传完毕");
                    item.Delete();
                    Console.WriteLine("本地文件清楚完毕");
                }
            }
            Console.WriteLine("本次任务执行完毕");
            isUsing = true;
        }
        public static string GetServicePath(string path)
        {
            string[] arr = path.Split('\\');
            bool b = false;
            string spath = "/UPLOAD/";
            foreach (var item in arr.Take(arr.Length - 1))
            {
                if (b)
                {
                    spath += item + "/";
                }
                else {
                    if (item.ToUpper().Equals("UPLOAD"))
                        b = true;
                }
            }
            return spath;
        }
        /// <summary>  
        /// 遍历当前目录及子目录  
        /// </summary>  
        /// <param name="strPath">文件路径</param>  
        /// <returns>所有文件</returns>  
        private static IList<FileInfo> GetFiles(string strPath)
        {
            List<FileInfo> lstFiles = new List<FileInfo>();
            List<string> lstDirect = new List<string>();
            lstDirect.Add(strPath);
            DirectoryInfo diFliles = null;
            GetDirectorys(strPath, ref lstDirect);
            foreach (string str in lstDirect)
            {
                try
                {
                    diFliles = new DirectoryInfo(str);
                    lstFiles.AddRange(diFliles.GetFiles());
                }
                catch
                {
                    continue;
                }
            }
            return lstFiles;
        }
        private static void GetDirectorys(string strPath, ref List<string> lstDirect)
        {
            DirectoryInfo diFliles = new DirectoryInfo(strPath);
            DirectoryInfo[] diArr = diFliles.GetDirectories();
            //DirectorySecurity directorySecurity = null;  
            foreach (DirectoryInfo di in diArr)
            {
                try
                {
                    //directorySecurity = new DirectorySecurity(di.FullName, AccessControlSections.Access);  
                    //if (!directorySecurity.AreAccessRulesProtected)  
                    //{  
                    lstDirect.Add(di.FullName);
                    GetDirectorys(di.FullName, ref lstDirect);
                    //}  
                }
                catch
                {
                    continue;
                }
            }
        }























    }

}
