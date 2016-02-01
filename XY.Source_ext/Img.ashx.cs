using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using XY.Entity;
using XY.Services;
using XY.Util;

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

                string url = HttpContext.Current.Request.Url.AbsolutePath;
                url = url.Replace("/Img.ashx", "");
                try
                {
                    string locationPath = ConfigurationManager.AppSettings["path"] + url;
                    LogHelper.Info(locationPath);
                    if (!System.IO.File.Exists(locationPath))
                    {
                        HttpContext.Current.Response.Write("图片不存在");
                        return;
                    }
                    ////
                    string[] Position = ConfigurationManager.AppSettings["Position"].ToLower().Split(',');
                    int X = 0;
                    int Y = 0;
                    System.Drawing.Image imgSource = System.Drawing.Image.FromFile(locationPath);
                    System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(imgSource);
                    //图片水印

                    Company company = CompanyService.instance().GetCompanyByUrl(comeURL);
                    if (company == null)
                    {
                        HttpContext.Current.Response.Write("水印图片不存在");
                        return;
                    }

                    string locationPath_w = ConfigurationManager.AppSettings["path"] + company.WatermarkPIC;
                    //LogHelper.Info(locationPath_w);
                    System.Drawing.Image image = System.Drawing.Image.FromFile(locationPath_w);
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
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    imgSource.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    imgSource.Dispose();
                    graphic.Dispose();
                    LogHelper.Info("ok");
                    context.Response.ContentType = "image/Jpeg";
                    context.Response.ClearContent();
                    context.Response.BinaryWrite(ms.ToArray());
                }
                catch (Exception)
                {
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