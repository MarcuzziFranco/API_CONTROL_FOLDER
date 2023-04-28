using ApiDirectoyABM.Modelos;

namespace ApiDirectoyABM.Servicios
{
    public class FilesService
    {
        public Result RenameFile(FileRename fileRename)
        {
            Result result;
            try{
                File.Copy(fileRename.currentPathName, fileRename.newPathName,true);
                File.Delete(fileRename.currentPathName);
                result = new Result { codError = "0", message = "Archivo Renombrado" };
            }
            catch (Exception e){
                result = new Result { codError = "1", message = e.ToString() };
              
            }
            return result;
        }

        public Result ToPostFile(FileObj fileObj)
        {
            Result result;
            string ext = "." + Tools.GetFileExtension(fileObj.data);
            string nameFile = fileObj.name+ext;
            string pathGenerate = 
                Constans.PATH_FOLDER_DIRECTORY
                + fileObj.fileLocation
                + nameFile;

            byte[] dataFile = Convert.FromBase64String(fileObj.data);

            try{
                File.WriteAllBytes(pathGenerate, dataFile);
                result = new Result { codError = "0", message = "Archivo creado" };
            }
            catch (Exception e){
                Console.WriteLine(e.ToString());
                result = new Result { codError = "1", message = e.ToString()};
                
            }
            return result;
            
        }

        public Result ToDelete(FileObj fileObj)
        {
            Result result;
            try
            {                
                File.Delete(fileObj.fileLocation);
                result = new Result { codError = "0", message = "Archivo borrado" };
            }
            catch (Exception e)
            {
                result = new Result { codError = "1", message = e.ToString() };

            }
            return result;        
        }
    }
}
