﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace Zheng.Utilities.Helpers
{
    //https://github.com/Liu-Alan/Snai.NetCore.Study
    public class ValidateCodeHelper
    {
        private static string randomChars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string CreateRandom(int length)
        {
            string rndCode = "";
            Random rnd = new Random();

            for (int i = 0; i < length; i++)
            {
                rndCode += randomChars.Substring(rnd.Next(0, randomChars.Length), 1);
            }

            return rndCode;
        }

        public static byte[] CreateImage(out string code, int length = 4)
        {
            code = CreateRandom(length);
            Bitmap Img = null;
            Graphics graphics = null;
            MemoryStream ms = null;
            Random random = new Random();
            //颜色集合  
            Color[] color = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //字体集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            //定义图像的大小，生成图像的实例  
            Img = new Bitmap((int)code.Length * 18, 32);
            graphics = Graphics.FromImage(Img);//从Img对象生成新的Graphics对象    
            graphics.Clear(Color.White);//背景设为白色  

            //在随机位置画背景点  

            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(Img.Width);
                int y = random.Next(Img.Height);
                graphics.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }

            //验证码绘制在graphics中  
            for (int i = 0; i < code.Length; i++)
            {
                int colorIndex = random.Next(7);//随机颜色索引值  
                int fontIndex = random.Next(4);//随机字体索引值  
                Font font = new Font(fonts[fontIndex], 15, FontStyle.Bold);//字体  
                Brush brush = new SolidBrush(color[colorIndex]);//颜色  
                int y = 4;
                if ((i + 1) % 2 == 0)//控制验证码不在同一高度  
                {
                    y = 2;
                }
                graphics.DrawString(code.Substring(i, 1), font, brush, 4 + (i * 12), y);//绘制一个验证字符  
            }
            ms = new MemoryStream();//生成内存流对象  
            Img.Save(ms, ImageFormat.Png);//将此图像以Png图像文件的格式保存到流中  
            graphics.Dispose();
            Img.Dispose();
            ms.Close();
            ms.Dispose();

            return ms.GetBuffer();
        }
    }
}
