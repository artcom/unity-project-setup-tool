using ArtCom.ProjectSetupTool.Editor.Models;
using UnityEditor;

namespace ArtCom.ProjectSetupTool.Editor.Controllers
{
    public class ProjectSettingsInitializer
    {
        public void Initialize(ProjectSettings settings)
        {
            if (!NamespaceValidator.HasInvalidCharacter(settings.rootNamespace))
            {
                EditorSettings.projectGenerationRootNamespace = settings.rootNamespace;
            }
            EditorSettings.prefabModeAllowAutoSave = !settings.isPrefabAutoSaveDisabled;
            EditorSettings.refreshImportMode = settings.isParallelAssetImportEnabled
                ? AssetDatabase.RefreshImportMode.OutOfProcessPerQueue
                : AssetDatabase.RefreshImportMode.InProcess;
            EditorSettings.enterPlayModeOptionsEnabled = settings.isEnterPlayModeOptionsEnabled;
            EditorSettings.spritePackerMode = settings.spritePackerMode;
            EditorSettings.gameObjectNamingScheme = settings.gameObjectNamingScheme;
            EditorSettings.gameObjectNamingDigits = settings.gameObjectNamingDigits;
        }
    }
}