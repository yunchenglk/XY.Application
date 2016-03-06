using System.IO;
using System.Net;
using System.Web;

namespace XY.Services.Weixin.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// 根据完整文件路径获取FileStream
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static FileStream GetFileStream(string fileName)
        {
            FileStream fileStream = null;
            if (!string.IsNullOrEmpty(fileName) && File.Exists(fileName))
            {
                fileStream = new FileStream(fileName, FileMode.Open);
            }
            return fileStream;
        }


        /// <summary>
        /// 下载文件到本地
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string DownLoadFile(string path) {

            string savePath = HttpContext.Current.Server.MapPath(path);
            string dirPath = HttpContext.Current.Server.MapPath(path.Substring(0, path.LastIndexOf("/") + 1));
            WebResponse response = null;
            Stream stream = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(System.Web.Configuration.WebConfigurationManager.AppSettings["sourceWeb"] + path);
                response = request.GetResponse();
                stream = response.GetResponseStream();

                byte[] buffer = new byte[1024];
                Stream outStream = null;
                Stream inStream = null;
                if (File.Exists(savePath)) File.Delete(savePath);

                if (!Directory.Exists(dirPath))
                {
                    DirectoryInfo d = new DirectoryInfo(dirPath);
                    d.Create();
                }
                outStream = System.IO.File.Create(savePath);
                inStream = response.GetResponseStream();
                int l;
                do
                {
                    l = inStream.Read(buffer, 0, buffer.Length);
                    if (l > 0) outStream.Write(buffer, 0, l);
                } while (l > 0);
                if (outStream != null) outStream.Close();
                if (inStream != null) inStream.Close();
            }
            finally
            {
                if (stream != null) stream.Close();
                if (response != null) response.Close();
            }
            return savePath;
        }

    }
}
