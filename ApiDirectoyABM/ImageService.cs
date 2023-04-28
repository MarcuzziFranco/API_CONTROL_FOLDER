
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
                string imageExtensionFile = Tools.GetFileExtension(imageObj.data);
                string imageNameFileComplete = imageObj.name + imageExtensionFile;
                string path = Constans.PATH_FOLDER_DIRECTORY + "/" + imageNameFileComplete;

                byte[] imageBytes = Convert.FromBase64String(imageObj.data);

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
