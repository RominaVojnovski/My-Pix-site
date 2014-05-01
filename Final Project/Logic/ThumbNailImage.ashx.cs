using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Configuration;
using Final_Project.Models;
using System.Data.Entity;

namespace Final_Project.Logic
{
    /// <summary>
    /// Summary description for ThumbNailImage
    /// </summary>
    public class ThumbNailImage : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string image_name = context.Request.QueryString["img"];
            if (image_name != null || image_name != "") 
            {
                Console.Write("IMAGE_NAME IS:" + image_name);
                
                byte[] buffer = File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath(image_name));
                Stream str = new MemoryStream(buffer);

                Bitmap loBMP = new Bitmap(str);
                Bitmap bmpOut = new Bitmap(100, 75);

                Graphics g = Graphics.FromImage(bmpOut);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.FillRectangle(Brushes.Black, 0, 0, 100, 75);
                g.DrawImage(loBMP, 0, 0,100, 75);

                MemoryStream ms = new MemoryStream();
                bmpOut.Save(ms, ImageFormat.Png);
                byte[] bmpBytes = ms.GetBuffer();
                bmpOut.Dispose();
                ms.Close();
                context.Response.BinaryWrite(bmpBytes);
            
            }
            
            /*context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");*/
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