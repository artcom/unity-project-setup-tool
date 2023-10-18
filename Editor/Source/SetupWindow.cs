using ArtCom.ProjectSetupTool.Editor.Controllers;
using ArtCom.ProjectSetupTool.Editor.Fixtures;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArtCom.ProjectSetupTool.Editor
{
    public class SetupWindow : EditorWindow
    {
        [SerializeField] private VisualTreeAsset _visualTreeAsset;

        private static SetupWindow _window;
        
        private FolderStructurePreview _folderStructurePreview;
        private FolderStructureGenerator _folderStructureGenerator;
        private AssemblyDefinitionsGenerator _assemblyDefinitionsGenerator;
        private ProjectSettingsInitializer _projectSettingsInitializer;
        private PackageInstaller _packageInstaller;
        private GitInitializer _gitInitializer;
        
        private Toggle _includeUiToolkitFoldersToggle;
        private Toggle _includeTestingFoldersToggle;
        private Label _folderStructurePreviewLabel;
        
        private Button _executeButton;
        private Button _cancelButton;

        private FolderStructureFixture _folderStructureFixture;
        private ProjectSettingsFixture _projectSettingsFixture;
        private PackageInstallFixture _packageFixture;
        private GitFixture _gitFixture;

        [MenuItem("Tools/Project Setup")]
        private static void ShowWindow()
        {
            _window = GetWindow<SetupWindow>();
            _window.titleContent = new GUIContent("Project Setup");
        }

        public void CreateGUI()
        {
            InitializeUiElements();
            InitializeControllers();
            RegisterEvents();
        }

        private void InitializeUiElements()
        {
            VisualElement uxml = _visualTreeAsset.Instantiate();
            rootVisualElement.Add(uxml);
            
            _includeUiToolkitFoldersToggle = rootVisualElement.Q<Toggle>("include-ui-toolkit-folders-toggle");
            _includeTestingFoldersToggle = rootVisualElement.Q<Toggle>("include-testing-folders-toggle");
            _folderStructurePreviewLabel = rootVisualElement.Q<Label>("folder-structure__preview");
            _executeButton = rootVisualElement.Q<Button>("buttons__execute");
            _cancelButton = rootVisualElement.Q<Button>("buttons__cancel");

            _folderStructureFixture = new FolderStructureFixture(rootVisualElement);
            _projectSettingsFixture = new ProjectSettingsFixture(rootVisualElement);
            _packageFixture = new PackageInstallFixture(rootVisualElement);
            _gitFixture = new GitFixture(rootVisualElement);
        }
        
        private void InitializeControllers()
        {
            _folderStructurePreview = new FolderStructurePreview(_folderStructurePreviewLabel.text);
            _folderStructureGenerator = new FolderStructureGenerator();
            _assemblyDefinitionsGenerator = new AssemblyDefinitionsGenerator();
            _projectSettingsInitializer = new ProjectSettingsInitializer();
            _packageInstaller = new PackageInstaller();
            _gitInitializer = new GitInitializer();
        }
        
        private void RegisterEvents()
        {
            _includeUiToolkitFoldersToggle.RegisterValueChangedCallback(UpdateUiToolkitFolderStructurePreview);
            _includeTestingFoldersToggle.RegisterValueChangedCallback(UpdateTestingFolderStructurePreview);

            _executeButton.clicked += ExecuteSetup;
            _cancelButton.clicked += CloseSetup;
        }

        private void UnregisterEvents()
        {
            _includeUiToolkitFoldersToggle.UnregisterValueChangedCallback(UpdateUiToolkitFolderStructurePreview);
            _includeTestingFoldersToggle.UnregisterValueChangedCallback(UpdateTestingFolderStructurePreview);
            
            _folderStructureFixture.UnregisterEvents();
            _projectSettingsFixture.UnregisterEvents();
            _packageFixture.UnregisterEvents();
            _gitFixture.UnregisterEvents();
            
            _executeButton.clicked -= ExecuteSetup;
            _cancelButton.clicked -= CloseSetup;
        }
        
        private void UpdateUiToolkitFolderStructurePreview(ChangeEvent<bool> evt)
        {
            if (evt.newValue)
            {
                _folderStructurePreview.AddUiToolkitFoldersToPreview();
            }
            else
            {
                _folderStructurePreview.RemoveUiToolkitFoldersFromPreview();
            }

            _folderStructurePreviewLabel.text = _folderStructurePreview.Value;
        }
        
        private void UpdateTestingFolderStructurePreview(ChangeEvent<bool> evt)
        {
            if (evt.newValue)
            {
                _folderStructurePreview.AddTestingFoldersToPreview();
            }
            else
            {
                _folderStructurePreview.RemoveTestingFoldersFromPreview();
            }

            _folderStructurePreviewLabel.text = _folderStructurePreview.Value;
        }
        
        private void ExecuteSetup()
        {
            _folderStructureGenerator.Generate(_folderStructureFixture.data);
            _assemblyDefinitionsGenerator.Generate(_projectSettingsFixture.data.rootNamespace, _packageFixture.data);
            AssetDatabase.Refresh();

            _projectSettingsInitializer.Initialize(_projectSettingsFixture.data);
            _packageInstaller.Install(_packageFixture.data);
            _gitInitializer.Initialize(_gitFixture.data);
            
            CloseSetup();
        }

        private void CloseSetup()
        {
            UnregisterEvents();
            _window.Close();
        }
    }
}
