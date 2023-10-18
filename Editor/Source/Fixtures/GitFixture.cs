using ArtCom.ProjectSetupTool.Editor.Models;
using UnityEngine.UIElements;

namespace ArtCom.ProjectSetupTool.Editor.Fixtures
{
    public class GitFixture
    {
        public readonly GitSetupOptions data;
        
        private Toggle _createRepositoryToggle;
        private Toggle _useLfsToggle;
        private TextField _remoteUrlField;

        public GitFixture(VisualElement root)
        {
            data = new GitSetupOptions();
            
            GetUiElements(root);
            RegisterEvents();
        }
        
        public void UnregisterEvents()
        {
            _createRepositoryToggle.UnregisterValueChangedCallback(ChangeCreateRepositorySetting);
            _remoteUrlField.UnregisterValueChangedCallback(ChangeRemoteUrl);
        }

        private void GetUiElements(VisualElement root)
        {
            _createRepositoryToggle = root.Q<Toggle>("create-repository-toggle");
            _useLfsToggle = root.Q<Toggle>("use-lfs-toggle");
            _remoteUrlField = root.Q<TextField>("remote-url-field");
            
            _useLfsToggle.SetEnabled(false);
            _remoteUrlField.SetEnabled(false);
        }

        private void RegisterEvents()
        {
            _createRepositoryToggle.RegisterValueChangedCallback(ChangeCreateRepositorySetting);
            _useLfsToggle.RegisterValueChangedCallback(ChangeLfsSetting);
            _remoteUrlField.RegisterValueChangedCallback(ChangeRemoteUrl);
        }
        
        private void ChangeCreateRepositorySetting(ChangeEvent<bool> evt)
        {
            data.isGitUsed = evt.newValue;
            
            _useLfsToggle.SetEnabled(evt.newValue);
            _remoteUrlField.SetEnabled(evt.newValue);
        }

        private void ChangeLfsSetting(ChangeEvent<bool> evt) => data.isLfsUsed = evt.newValue;

        private void ChangeRemoteUrl(ChangeEvent<string> evt) => data.remoteUrl = evt.newValue;
    }
}