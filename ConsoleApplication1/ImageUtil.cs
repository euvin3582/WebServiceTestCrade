using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCradle
{
    public static class ImageUtil
    {
        /// <summary>
        /// All images to import to this service must be in the c:/Temp folder
        /// Need to pass the image name and extension to make this work
        /// </summary>
        /// <param name="companyFileName"></param>
        /// <param name="coid"></param>
        /// <returns></returns>
        public static int InsertImageToDb(string companyFileName, int coid)
        {
            Image image = Bitmap.FromFile("c:\\Temp\\" + companyFileName);
            int index = companyFileName.IndexOf(".png"); 
            string imageName = companyFileName.Remove(index);
            byte[] array = MTSUtilities.ImageUtilities.ImageProcess.ImageToByteArray(image);
            
            if(array.Length > 0)
                return DataLayer.Controller.InsertCustomerImages(imageName, coid, array, "Image item for company " + coid + " FileName: " + imageName);

            return -1;
        }

        
    }
}
