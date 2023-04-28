
using ApiDirectoyABM.Modelos;

namespace ApiDirectoyABM.Servicios
{
    public class DirectoryService
    {

        public PageDirectory getAllDirectorys(String currentPath)
        {
            if (currentPath == null || currentPath == "") currentPath = Constans.PATH_FOLDER_DIRECTORY;

            List<string> listAll = new();
            List<string> listDirectorys = Directory.GetDirectories(currentPath).ToList();
            List<string> listFiles = Directory.GetFiles(currentPath).ToList();
            List<DirectoryBase> listDirectoryBases = new List<DirectoryBase>();

            listAll.AddRange(listDirectorys);
            listAll.AddRange(listFiles);
            listDirectoryBases = getNameAndTypeFileFromPath(listAll);

            PageDirectory pageDirectory = new PageDirectory(currentPath,listDirectoryBases);
          

            return pageDirectory;
        }

        private List<DirectoryBase> getNameAndTypeFileFromPath(List<string> listAll)
        {

            List<DirectoryBase> listDirectorys = new();
            foreach (string file in listAll)
            {
                int index = file.LastIndexOf('/');
                if(index != -1)
                {

                    DirectoryBase directoryBase = new DirectoryBase();
                    directoryBase.Path = file;
                    directoryBase.Name = file[(index + 1)..];

                    if (directoryBase.Name.Contains('.'))
                    {
                        directoryBase.Type = directoryBase.Name.Split('.')[1];
                    }
                    else
                    {
                        directoryBase.Type = "Folder";
                    }
                    listDirectorys.Add(directoryBase);
                }
            }
            return listDirectorys;
        }
    }
}
