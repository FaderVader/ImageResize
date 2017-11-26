using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace ImageResize
{
    public class Main
    {
        public void ProcessFolder(string sourcePath, string destinationPath, int imageSize)
        {
            ImageResizer imageResizer = new ImageResizer();
            string fileTypeFilter = Properties.Settings.Default.FileTypeFilter;
            var listOfFiles = Directory.GetFiles(sourcePath, fileTypeFilter);

            foreach (var file in listOfFiles)
            {               
                    string fileName = destinationPath + @"\" + Path.GetFileName(file);
                    imageResizer.ImageResizeAndSave(file, destinationPath, imageSize);              
            }
        }

        public void RunApp()
        {
            string sourceFolder = Properties.Settings.Default.SourcePath;
            var destinations = Properties.Settings.Default.DestinationPaths;
            var resolutions = Properties.Settings.Default.Resolutions;
            int resolutionAsInt;
            string[] resolutionsAsArray = new string[2];

            int counter = 0;
            foreach (var res in resolutions)
            {
                resolutionsAsArray[counter] = res;
                counter++;
            }

            counter = 0;
            foreach (var dest in destinations)
            {
                Int32.TryParse(resolutionsAsArray[counter], out resolutionAsInt);
                ProcessFolder(sourceFolder, dest, resolutionAsInt);
                counter++;
            }

        }
    }
}
