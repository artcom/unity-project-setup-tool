namespace ArtCom.ProjectSetupTool.Editor.Models
{
    public static class RecommendedPackages
    {
        private const string GitHubUrl = "https://github.com";
        private const string CysharpRegistry = "Cysharp";

        public static readonly string uniTask =
            $"{GitHubUrl}/{CysharpRegistry}/UniTask.git?path=src/UniTask/Assets/Plugins/UniTask";

        public static readonly string memoryPack =
            $"{GitHubUrl}/{CysharpRegistry}/MemoryPack.git?path=src/MemoryPack.Unity/Assets/Plugins/MemoryPack";

        public static readonly string graphy = $"{GitHubUrl}/Tayx94/graphy.git";

        public static readonly string inputSystem = "com.unity.inputsystem";
    }
}