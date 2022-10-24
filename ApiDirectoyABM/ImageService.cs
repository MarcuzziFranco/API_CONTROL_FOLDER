
using System;
using System.IO;
using System.Drawing;


namespace ApiDirectoyABM  
{

    public class ImageService
    {
        public void SaveImage(ImageObj imageObj)
        {
            
            string ext = "."+Tools.GetFileExtension(imageObj.data);
            string path =Constans.PATH_FOLDER_DIRECTORY + "/" + imageObj.name+ext;

            byte[] imageBytes = Convert.FromBase64String(imageObj.data);
           
            File.WriteAllBytes(path, imageBytes);
        }

    }


  
}
