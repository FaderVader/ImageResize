using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace ImageResize
{
    public class ImageResizer
    {
        public void ImageResizeAndSave(string sourceName, string destinationPath, int destinationHeight)
        {
            int destinationWidth = CalculateNewWidth(sourceName, destinationHeight);

            using (Bitmap destinationImage = new Bitmap(destinationWidth, destinationHeight))
            {
                using (Graphics graphics = Graphics.FromImage(destinationImage))
                {
                    graphics.Clear(Color.White);
                    using (var sourceImage = new Bitmap(sourceName))
                    {                        
                        var w = destinationWidth;  
                        var h = destinationHeight;                 

                        // Use alpha blending in case the source image has transparencies.
                        graphics.CompositingMode = CompositingMode.SourceOver;

                        // Use high quality compositing and interpolation.
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        graphics.DrawImage(sourceImage, 0, 0, w, h);
                    }
                }                

                string fileName = destinationPath + @"\" +Path.GetFileName(sourceName);
                destinationImage.Save(fileName);   //destinationPath
            }
        }   
        
        private int CalculateNewWidth(string sourceName, int requestedHeight)
        {
            decimal newX;
           
            using (Bitmap sourceImage = new Bitmap(sourceName))
            {
                int origX = sourceImage.Width;
                int origY = sourceImage.Height;

                decimal xFactor = (decimal)requestedHeight / (decimal)origY;
                newX = (decimal)origX * xFactor;
            }

            return (int)newX;
        }
    }
}
