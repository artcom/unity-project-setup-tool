using UnityEditor;

namespace ArtCom.ProjectSetupTool.Editor.Models
{
    public class ProjectSettings
    {
        public string rootNamespace;
        public bool isPrefabAutoSaveDisabled;
        public bool isParallelAssetImportEnabled;
        public bool isEnterPlayModeOptionsEnabled;
        public SpritePackerMode spritePackerMode;
        public EditorSettings.NamingScheme gameObjectNamingScheme;
        public int gameObjectNamingDigits;

        public static ProjectSettings Default() => new()
        {
            rootNamespace = "CompanyName.ProjectName",
            isPrefabAutoSaveDisabled = true,
            isParallelAssetImportEnabled = true,
            isEnterPlayModeOptionsEnabled = false,
            spritePackerMode = SpritePackerMode.Disabled,
            gameObjectNamingScheme = EditorSettings.NamingScheme.Dot,
            gameObjectNamingDigits = 2
        };
    }
}