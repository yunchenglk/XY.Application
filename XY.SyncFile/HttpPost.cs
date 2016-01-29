using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace XY.SyncFile
{
    public class HttpPost
    {
        
        /// <summary>
        /// 以Post 形式提交数据到 uri
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="files"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool Post(Uri uri, IEnumerable<UploadFile> files, NameValueCollection values)
        {
            try
            {
                string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.ContentType = "multipart/form-data; boundary=" + boundary;
                request.Method = "POST";
                request.KeepAlive = true;
                request.Credentials = CredentialCache.DefaultCredentials;
                MemoryStream stream = new MemoryStream();
                byte[] line = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                //提交文本字段
                if (values != null)
                {
                    string format = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";
                    foreach (string key in values.Keys)
                    {
                        string s = string.Format(format, key, values[key]);
                        byte[] data = Encoding.UTF8.GetBytes(s);
                        stream.Write(data, 0, data.Length);
                    }
                    stream.Write(line, 0, line.Length);
                }
                //提交文件
                if (files != null)
                {
                    string fformat = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
                    foreach (UploadFile file in files)
                    {
                        string s = string.Format(fformat, file.Name, file.Filename);
                        byte[] data = Encoding.UTF8.GetBytes(s);
                        stream.Write(data, 0, data.Length);
                        stream.Write(file.Data, 0, file.Data.Length);
                        stream.Write(line, 0, line.Length);
                    }
                }
                request.ContentLength = stream.Length;
                Stream requestStream = request.GetRequestStream();
                stream.Position = 0L;
                stream.CopyTo(requestStream);
                stream.Close();
                requestStream.Close();
                using (var response = request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                using (var mstream = new MemoryStream())
                {
                    responseStream.CopyTo(mstream);
                    mstream.ToArray();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("错误：");
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        public class UploadFile
        {
            public UploadFile()
            {
                ContentType = "application/octet-stream";
            }
            public string Name { get; set; }
            public string Filename { get; set; }
            public string ContentType { get; set; }
            public byte[] Data { get; set; }
        }
    }
}
