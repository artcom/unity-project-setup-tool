using System.Text;
using ArtCom.ProjectSetupTool.Editor.Extensions;
using ArtCom.ProjectSetupTool.Editor.Models;

namespace ArtCom.ProjectSetupTool.Editor.Controllers
{
    public class FolderStructurePreview
    {
        private const string SettingsIndent = "\n│   ";
        private const string SourceIndent = "\n       ";

        private readonly StringBuilder _previewBuilder;
        private readonly FolderStructure _folders;
        private readonly string _testFoldersPreview;
        
        public string Value => _previewBuilder.ToString();

        public FolderStructurePreview(string initialPreview)
        {
            _previewBuilder = new StringBuilder(initialPreview);
            _folders = new FolderStructure();
            
            _testFoldersPreview = @$"
       │   └─ {_folders.Tests.Name}
       │          ├─ {_folders.EditMode.Name}
       │          ├─ {_folders.Infrastructure.Name}
       │          └─ {_folders.PlayMode.Name}";
        }

        public void AddUiToolkitFoldersToPreview()
        {
            // For the sake of performance and that situation being unlikely,
            // no check for the UI folders already present is made.
            
            _previewBuilder.InsertAfter($"{SettingsIndent}└─ {_folders.SettingsUI.Name}", _folders.RenderPipeline.Name);
            _previewBuilder.InsertAfter($"{SourceIndent}└─ {_folders.SourceUI.Name}", _folders.Shaders.Name);
        }
        
        public void RemoveUiToolkitFoldersFromPreview()
        {
            _previewBuilder.Replace($"{SettingsIndent}└─ {_folders.SettingsUI.Name}", string.Empty);
            _previewBuilder.Replace($"{SourceIndent}└─ {_folders.SourceUI.Name}", string.Empty);
        }

        // For the sake of performance and that situation being unlikely,
        // no check for the testing folders already present is made.
        public void AddTestingFoldersToPreview() => _previewBuilder.InsertAfter(_testFoldersPreview, _folders.Runtime.Name);

        public void RemoveTestingFoldersFromPreview() => _previewBuilder.Replace(_testFoldersPreview, string.Empty);
    }
}