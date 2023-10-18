using ArtCom.ProjectSetupTool.Editor.Models;
using UnityEngine.UIElements;

namespace ArtCom.ProjectSetupTool.Editor.Fixtures
{
    public class PackageInstallFixture
    {
        public readonly InstallPackagesInfo data;

        private Toggle _uniTaskToggle;
        private Toggle _memoryPackToggle;
        private Toggle _graphyToggle;

        public PackageInstallFixture(VisualElement root)
        {
            data = new InstallPackagesInfo();

            GetUiElements(root);
            RegisterEvents();
        }

        public void UnregisterEvents()
        {
            _uniTaskToggle.UnregisterValueChangedCallback(ChangeUniTaskSetting);
            _memoryPackToggle.UnregisterValueChangedCallback(ChangeMemoryPackSetting);
            _graphyToggle.UnregisterValueChangedCallback(ChangeGraphySetting);
        }

        private void GetUiElements(VisualElement root)
        {
            _uniTaskToggle = root.Q<Toggle>("unitask-toggle");
            _memoryPackToggle = root.Q<Toggle>("memorypack-toggle");
            _graphyToggle = root.Q<Toggle>("graphy-toggle");
        }

        private void RegisterEvents()
        {
            _uniTaskToggle.RegisterValueChangedCallback(ChangeUniTaskSetting);
            _memoryPackToggle.RegisterValueChangedCallback(ChangeMemoryPackSetting);
            _graphyToggle.RegisterValueChangedCallback(ChangeGraphySetting);
        }

        private void ChangeUniTaskSetting(ChangeEvent<bool> evt) => data.hasUniTask = evt.newValue;

        private void ChangeMemoryPackSetting(ChangeEvent<bool> evt) => data.hasMemoryPack = evt.newValue;

        private void ChangeGraphySetting(ChangeEvent<bool> evt) => data.hasGraphy = evt.newValue;
    }
}