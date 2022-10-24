
using ApiDirectoyABM.Modelos;

namespace ApiDirectoyABM.Servicios
{
    public class DirectoryService
    {

        public PageDirectory getAllDirectorys(String currentPath)
        {
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
                string[] data = file.Split("\\");
                DirectoryBase directoryBase = new DirectoryBase();
                directoryBase.Path = data[0] + "/" + data[1];
                directoryBase.Name = data[1];

                if (directoryBase.Name.Contains('.')){
                    directoryBase.Type= directoryBase.Name.Split('.')[1];
                }
                else
                {
                    directoryBase.Type= "Folder";
                }

                listDirectorys.Add(directoryBase);
            }
            return listDirectorys;
        }
    }
}
