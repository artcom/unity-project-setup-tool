using System.IO;

namespace ArtCom.ProjectSetupTool.Editor.Extensions
{
    public static class DirectoryInfoExtensions
    {
        public static void CreateWithKeepFile(this DirectoryInfo directory)
        {
            directory.Create();
            
            string keepFilePath = Path.Combine(directory.FullName, ".keep");
            if (!File.Exists(keepFilePath))
            {
                File.Create(keepFilePath);
            }
        }
    }
}