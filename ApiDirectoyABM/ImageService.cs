
using System;
using System.IO;
using System.Drawing;


namespace ApiDirectoyABM  
{

    public class ImageService
    {
        public string SaveImage(ImageObj imageObj)
        {

            string statusService;
            try
            {
                string imageExtensionFile = Tools.GetFileExtension(imageObj.dataBase64);
                string imageNameFileComplete = imageObj.name + imageExtensionFile;
                string path = Constans.PATH_FOLDER_DIRECTORY + "/" + imageNameFileComplete;

                byte[] imageBytes = Convert.FromBase64String(imageObj.dataBase64);

                File.WriteAllBytes(path, imageBytes);
                statusService = "Image saved successfully";

            }
            catch(Exception error)
            {
                statusService = error.Message;
            }
            return statusService;
        }

    }


  
}
