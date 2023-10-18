using System;
using System.Linq;
using ArtCom.ProjectSetupTool.Editor.Models;
using UnityEditor;
using UnityEngine.UIElements;

namespace ArtCom.ProjectSetupTool.Editor.Fixtures
{
    public class ProjectSettingsFixture
    {
        public readonly ProjectSettings data;
        
        private TextField _rootNamespaceField;
        private Toggle _disablePrefabAutoSaveToggle;
        private Toggle _enableParallelAssetImportToggle;
        private Toggle _enableEnterPlayModeOptionsToggle;
        private DropdownField _spritePackerModeDropdown;
        private DropdownField _gameObjectNamingDropdown;
        private SliderInt _gameObjectNamingDigitsSlider;

        public ProjectSettingsFixture(VisualElement root)
        {
            data = ProjectSettings.Default();
            
            GetUiElements(root);
            InitializeUiValues();
            RegisterEvents();
        }
        
        public void UnregisterEvents()
        {
            _rootNamespaceField.UnregisterValueChangedCallback(ChangeRootNamespaceSetting);
            _disablePrefabAutoSaveToggle.UnregisterValueChangedCallback(ChangePrefabAutoSaveSetting);
            _enableParallelAssetImportToggle.UnregisterValueChangedCallback(ChangeParallelAssetImportSetting);
            _enableEnterPlayModeOptionsToggle.UnregisterValueChangedCallback(ChangeEnterPlayModeOptionSetting);
            _spritePackerModeDropdown.UnregisterValueChangedCallback(ChangeSpritePackerModeSetting);
            _gameObjectNamingDropdown.UnregisterValueChangedCallback(ChangeGameObjectNamingSetting);
            _gameObjectNamingDigitsSlider.UnregisterValueChangedCallback(ChangeGameObjectNamingDigitsSetting);
        }

        private void GetUiElements(VisualElement root)
        {
            _rootNamespaceField = root.Q<TextField>("root-namespace-field");
            _disablePrefabAutoSaveToggle = root.Q<Toggle>("disable-prefab-auto-save-toggle");
            _enableParallelAssetImportToggle = root.Q<Toggle>("enable-parallel-import-toggle");
            _enableEnterPlayModeOptionsToggle = root.Q<Toggle>("enable-playmode-options-toggle");
            _spritePackerModeDropdown = root.Q<DropdownField>("sprite-packer-mode-dropdown");
            _gameObjectNamingDropdown = root.Q<DropdownField>("game-object-naming-dropdown");
            _gameObjectNamingDigitsSlider = root.Q<SliderInt>("game-object-digits-slider");
        }
        
        private void InitializeUiValues()
        {
            _rootNamespaceField.value = data.rootNamespace;
            _disablePrefabAutoSaveToggle.value = data.isPrefabAutoSaveDisabled;
            _enableParallelAssetImportToggle.value = data.isParallelAssetImportEnabled;
            _enableEnterPlayModeOptionsToggle.value = data.isEnterPlayModeOptionsEnabled;
            _spritePackerModeDropdown.choices = Enum.GetNames(typeof(SpritePackerMode)).ToList();
            _spritePackerModeDropdown.value = data.spritePackerMode.ToString();
            _gameObjectNamingDropdown.choices = Enum.GetNames(typeof(EditorSettings.NamingScheme)).ToList();
            _gameObjectNamingDropdown.value = data.gameObjectNamingScheme.ToString();
            _gameObjectNamingDigitsSlider.value = data.gameObjectNamingDigits;
        }
        
        private void RegisterEvents()
        {
            _rootNamespaceField.RegisterValueChangedCallback(ChangeRootNamespaceSetting);
            _disablePrefabAutoSaveToggle.RegisterValueChangedCallback(ChangePrefabAutoSaveSetting);
            _enableParallelAssetImportToggle.RegisterValueChangedCallback(ChangeParallelAssetImportSetting);
            _enableEnterPlayModeOptionsToggle.RegisterValueChangedCallback(ChangeEnterPlayModeOptionSetting);
            _spritePackerModeDropdown.RegisterValueChangedCallback(ChangeSpritePackerModeSetting);
            _gameObjectNamingDropdown.RegisterValueChangedCallback(ChangeGameObjectNamingSetting);
            _gameObjectNamingDigitsSlider.RegisterValueChangedCallback(ChangeGameObjectNamingDigitsSetting);
        }

        private void ChangeRootNamespaceSetting(ChangeEvent<string> evt) => data.rootNamespace = evt.newValue;

        private void ChangePrefabAutoSaveSetting(ChangeEvent<bool> evt) => data.isPrefabAutoSaveDisabled = evt.newValue;

        private void ChangeParallelAssetImportSetting(ChangeEvent<bool> evt) => data.isParallelAssetImportEnabled = evt.newValue;

        private void ChangeEnterPlayModeOptionSetting(ChangeEvent<bool> evt) => data.isEnterPlayModeOptionsEnabled = evt.newValue;

        private void ChangeSpritePackerModeSetting(ChangeEvent<string> evt) => data.spritePackerMode = Enum.Parse<SpritePackerMode>(evt.newValue);

        private void ChangeGameObjectNamingSetting(ChangeEvent<string> evt) => data.gameObjectNamingScheme = Enum.Parse<EditorSettings.NamingScheme>(evt.newValue);

        private void ChangeGameObjectNamingDigitsSetting(ChangeEvent<int> evt) => data.gameObjectNamingDigits = evt.newValue;
    }
}