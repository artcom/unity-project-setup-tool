using ArtCom.ProjectSetupTool.Editor.Extensions;
using ArtCom.ProjectSetupTool.Editor.Models;

namespace ArtCom.ProjectSetupTool.Editor.Controllers
{
    public class FolderStructureGenerator
    {
        private readonly FolderStructure _folders = new();

        public void Generate(FolderStructureOptions options)
        {
            GenerateRootFolders();

            GenerateAudioSubfolders();
            GenerateGraphicsSubfolders();
            GenerateSettingsSubfolders(options.includeUiToolkitFolders);
            GenerateSourceSubfolders(options);
        }

        private void GenerateRootFolders()
        {
            _folders.ThirdParty.Create();
            _folders.Audio.Create();
            _folders.Graphics.Create();
            _folders.Plugins.CreateWithKeepFile();
            _folders.Prefabs.CreateWithKeepFile();
            _folders.Resources.CreateWithKeepFile();
            _folders.Scenes.CreateWithKeepFile();
            _folders.Settings.Create();
            _folders.Source.Create();
        }

        private void GenerateAudioSubfolders()
        {
            _folders.Music.CreateWithKeepFile();
            _folders.Sound.CreateWithKeepFile();
        }

        private void GenerateGraphicsSubfolders()
        {
            _folders.Animations.CreateWithKeepFile();
            _folders.Fonts.CreateWithKeepFile();
            _folders.Materials.CreateWithKeepFile();
            _folders.Models.CreateWithKeepFile();
            _folders.ShaderGraphs.CreateWithKeepFile();
            _folders.Sprites.Create();
            _folders.Textures.CreateWithKeepFile();
            _folders.VFX.CreateWithKeepFile();
        }

        private void GenerateSettingsSubfolders(bool withUiToolkitFolders)
        {
            _folders.Input.CreateWithKeepFile();
            _folders.RenderPipeline.CreateWithKeepFile();

            if (withUiToolkitFolders)
            {
                _folders.SettingsUI.CreateWithKeepFile();
            }
        }

        private void GenerateSourceSubfolders(FolderStructureOptions options)
        {
            _folders.Scripts.Create();
            _folders.Shaders.CreateWithKeepFile();

            if (options.includeUiToolkitFolders)
            {
                _folders.SourceUI.CreateWithKeepFile();
            }

            GenerateScriptsSubfolders(options.includeTestingFolders);
        }

        private void GenerateScriptsSubfolders(bool withTestFolders)
        {
            _folders.Runtime.Create();
            _folders.Editor.Create();

            if (withTestFolders)
            {
                GenerateTestsSubfolders();
            }
        }

        private void GenerateTestsSubfolders()
        {
            _folders.Tests.Create();
            _folders.EditMode.Create();
            _folders.Infrastructure.Create();
            _folders.PlayMode.Create();
        }
    }
}