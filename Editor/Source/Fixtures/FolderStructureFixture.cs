using ArtCom.ProjectSetupTool.Editor.Models;
using UnityEngine.UIElements;

namespace ArtCom.ProjectSetupTool.Editor.Fixtures
{
    public class FolderStructureFixture
    {
        public readonly FolderStructureOptions data;
        
        private Toggle _includeUiToolkitFoldersToggle;
        private Toggle _includeTestingFoldersToggle;
        
        public FolderStructureFixture(VisualElement root)
        {
            data = new FolderStructureOptions();
            
            GetUiElements(root);
            RegisterEvents();
        }
        
        public void UnregisterEvents()
        {
            _includeUiToolkitFoldersToggle.UnregisterValueChangedCallback(ChangeIncludeUiToolkitFoldersSetting);
            _includeTestingFoldersToggle.UnregisterValueChangedCallback(ChangeIncludeTestingFoldersSetting);
        }

        private void GetUiElements(VisualElement root)
        {
            _includeUiToolkitFoldersToggle = root.Q<Toggle>("include-ui-toolkit-folders-toggle");
            _includeTestingFoldersToggle = root.Q<Toggle>("include-testing-folders-toggle");
        }

        private void RegisterEvents()
        {
            _includeUiToolkitFoldersToggle.RegisterValueChangedCallback(ChangeIncludeUiToolkitFoldersSetting);
            _includeTestingFoldersToggle.RegisterValueChangedCallback(ChangeIncludeTestingFoldersSetting);
        }

        private void ChangeIncludeUiToolkitFoldersSetting(ChangeEvent<bool> evt) => data.includeUiToolkitFolders = evt.newValue;

        private void ChangeIncludeTestingFoldersSetting(ChangeEvent<bool> evt) => data.includeTestingFolders = evt.newValue;
    }
}