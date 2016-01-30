using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace XY.Source_ext
{
    /// <summary>
    /// Img 的摘要说明
    /// </summary>
    public class Img : IHttpHandler
    {
        private static object obj = new object();
        public void ProcessRequest(HttpContext context)
        {
            lock (obj)
            {
                string comeURL = HttpContext.Current.Request.UrlReferrer.GetLeftPart(UriPartial.Authority);
                string url = ConfigurationManager.AppSettings["sourceWeb"] + HttpContext.Current.Request.Url.AbsolutePath;
                url = url.Replace("/Img.ashx/", "");
                WebRequest request = null;
                WebResponse response = null;
                try
                {
                    request = WebRequest.Create(url);
                    response = request.GetResponse();
                    if (response == null)
                    {
                        HttpContext.Current.Response.Write("图片不存在");
                        return;
                    }
                    Stream stream = response.GetResponseStream();

                    ////
                    string[] Position = ConfigurationManager.AppSettings["Position"].ToLower().Split(',');
                    int X = 0;
                    int Y = 0;
                    System.Drawing.Image imgSource = System.Drawing.Image.FromStream(stream);
                    System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(imgSource);
                    //图片水印
                    request = WebRequest.Create(ConfigurationManager.AppSettings["sourceWeb"] + "/Company/GetCompanyWatermarkPIC//?url=" + comeURL);
                    response = request.GetResponse();
                    string WatermarkPIC = new StreamReader(response.GetResponseStream(), Encoding.Default).ReadToEnd();
                    request = WebRequest.Create(ConfigurationManager.AppSettings["sourceWeb"] + WatermarkPIC);
                    response = request.GetResponse();
                    stream = response.GetResponseStream();
                    System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                    if (Position.Length > 0)
                    {
                        int num;
                        if (int.TryParse(Position[0], out num))
                        {
                            X = (int)Math.Ceiling((imgSource.Width - image.Width) * (Convert.ToDouble(num) / Convert.ToDouble(100)));
                        }
                    }
                    if (Position.Length > 1)
                    {
                        int num;
                        if (int.TryParse(Position[1], out num))
                        {
                            Y = (int)Math.Ceiling((imgSource.Height - image.Height) * (Convert.ToDouble(num) / Convert.ToDouble(100)));
                        }
                    }
                    Rectangle rec = new Rectangle((int)X, (int)Y, image.Width, image.Height);
                    graphic.DrawImage(image, rec,
                        0,
                        0,
                        image.Width,
                        image.Height,
                        GraphicsUnit.Pixel);
                    //文字水印
                    //System.Drawing.Font font = new System.Drawing.Font("Arial Black", 10.0f, System.Drawing.FontStyle.Bold);
                    //SizeF sizrf = Graphics.FromImage(imgSource).MeasureString("test", font);
                    //if (Position.Length > 0)
                    //{
                    //    int num;
                    //    if (int.TryParse(Position[0], out num))
                    //    {
                    //        X = (int)Math.Ceiling((imgSource.Width - sizrf.Width) * (Convert.ToDouble(num) / Convert.ToDouble(100)));
                    //    }
                    //}
                    //if (Position.Length > 1)
                    //{
                    //    int num;
                    //    if (int.TryParse(Position[1], out num))
                    //    {
                    //        Y = (int)Math.Ceiling((imgSource.Height - sizrf.Height) * (Convert.ToDouble(num) / Convert.ToDouble(100)));
                    //    }
                    //}
                    //graphic.DrawString("test", font, System.Drawing.Brushes.Red, X, Y);
                    imgSource.Save(HttpContext.Current.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
                    imgSource.Dispose();
                    graphic.Dispose();
                    HttpContext.Current.Response.ContentType = "image/jpeg";
                    HttpContext.Current.Response.Flush();
                    HttpContext.Current.Response.End();
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                }

            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}