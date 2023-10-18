namespace ArtCom.ProjectSetupTool.Editor.Models
{
    public class GitSetupOptions
    {
        public bool isGitUsed;
        public bool isLfsUsed;
        public string remoteUrl;
        
        public string RemoteUrlArgument => string.IsNullOrEmpty(remoteUrl) ? string.Empty : $"-r {remoteUrl}";
        public string UseLfsArgument => isLfsUsed ? "-l" : string.Empty;
    }
}