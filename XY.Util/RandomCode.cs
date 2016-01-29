using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XY.Util
{
    public class RandomCode
    {
        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <param name="checkCode">验证码</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="bgcolor">背景颜色</param>
        /// <returns></returns>
        public System.IO.MemoryStream CreateImage(string checkCode, Color fontcolor, Color bgcolor)
        {
            int iwidth = (int)(checkCode.Length * 19);
            ColorConverter colConvert = new ColorConverter();
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 25);
            Graphics g = Graphics.FromImage(image);
            Font f = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold);
            Brush b = new System.Drawing.SolidBrush(fontcolor);

            Color color = bgcolor;
            g.Clear(color);
            g.DrawString(checkCode, f, b, 12, 3);

            Pen blackPen = new Pen(Color.Red, 0);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            g.Dispose();
            image.Dispose();
            return ms;
        }
        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <param name="checkCode">验证码</param>
        /// <param name="bgcolor">背景颜色</param>
        /// <returns></returns>
        public System.IO.MemoryStream CreateImage(string checkCode, Color bgcolor)
        {
            int iwidth = (int)(checkCode.Length * 12);
            ColorConverter colConvert = new ColorConverter();
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 20);
            Graphics g = Graphics.FromImage(image);
            Font f = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            Brush b = new System.Drawing.SolidBrush((Color)colConvert.ConvertFromString("#FFFFFF"));

            Color color = bgcolor;
            g.Clear(color);
            g.DrawString(checkCode, f, b, 3, 3);

            Pen blackPen = new Pen(Color.Red, 0);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            g.Dispose();
            image.Dispose();
            return ms;
        }
        /// <summary>
        /// 获取验证码图片
        /// </summary>
        /// <param name="checkCode">验证码</param>
        /// <returns></returns>
        public System.IO.MemoryStream CreateImage(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 12);
            ColorConverter colConvert = new ColorConverter();
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 20);
            Graphics g = Graphics.FromImage(image);
            Font f = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold);
            Brush b = new System.Drawing.SolidBrush((Color)colConvert.ConvertFromString("#FFFFFF"));

            Color color = (System.Drawing.Color)colConvert.ConvertFromString("#315CA1");
            g.Clear(color);
            g.DrawString(checkCode, f, b, 3, 3);

            Pen blackPen = new Pen(Color.Red, 0);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            g.Dispose();
            image.Dispose();
            return ms;
        }

        /// <summary>
        /// 获取随机数
        /// </summary>
        /// <param name="codeCount">个数</param>
        /// <returns></returns>
        public string CreateRandomCode(int codeCount)
        {
            string allChar = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,W,X,Y,Z";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            Random rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(allCharArray.Length);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }
    }
}
