namespace ApiDirectoyABM.Modelos
{
    public class PageDirectory
    {
        public PathPosition pathPosition = new PathPosition();

        public List<DirectoryBase> directoryBases;

        public PageDirectory(String currentPath,List<DirectoryBase> directoryBases)
        {
            pathPosition.currentPath = currentPath;
            this.directoryBases = directoryBases;
        }
    }
}
