using ArtCom.ProjectSetupTool.Editor.Controllers;
using ArtCom.ProjectSetupTool.Editor.Models;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace ArtCom.ProjectSetupTool.Tests.Editor
{
    public class FolderStructureGeneratorTests
    {
        private readonly FolderStructure _folders;
        private readonly FolderStructureGenerator _generator;

        protected FolderStructureGeneratorTests()
        {
            _folders = new FolderStructure();
            _generator = new FolderStructureGenerator();
        }

        [TearDown]
        public void TeardownAfterTestFailure()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                DeleteFolders();
            }
        }

        private void DeleteFolders()
        {
            _folders.ThirdParty.Delete(true);
            _folders.Audio.Delete(true);
            _folders.Graphics.Delete(true);
            _folders.Plugins.Delete(true);
            _folders.Prefabs.Delete(true);
            _folders.Resources.Delete(true);
            _folders.Scenes.Delete(true);
            _folders.Settings.Delete(true);
            _folders.Source.Delete(true);
        }

        public class TheGenerateMethod : FolderStructureGeneratorTests
        {
            [Test]
            public void GeneratesAllFoldersInTheSpecifiedHierarchy()
            {
                FolderStructureOptions options = new();

                _generator.Generate(options);

                Assert.That(_folders.ThirdParty, Does.Exist);
                Assert.That(_folders.Audio, Does.Exist);
                Assert.That(_folders.Music, Does.Exist);
                Assert.That(_folders.Sound, Does.Exist);
                Assert.That(_folders.Graphics, Does.Exist);
                Assert.That(_folders.Animations, Does.Exist);
                Assert.That(_folders.Fonts, Does.Exist);
                Assert.That(_folders.Materials, Does.Exist);
                Assert.That(_folders.Models, Does.Exist);
                Assert.That(_folders.ShaderGraphs, Does.Exist);
                Assert.That(_folders.Sprites, Does.Exist);
                Assert.That(_folders.Textures, Does.Exist);
                Assert.That(_folders.VFX, Does.Exist);
                Assert.That(_folders.Plugins, Does.Exist);
                Assert.That(_folders.Prefabs, Does.Exist);
                Assert.That(_folders.Resources, Does.Exist);
                Assert.That(_folders.Scenes, Does.Exist);
                Assert.That(_folders.Settings, Does.Exist);
                Assert.That(_folders.Input, Does.Exist);
                Assert.That(_folders.RenderPipeline, Does.Exist);
                Assert.That(_folders.SettingsUI, Does.Not.Exist);
                Assert.That(_folders.Source, Does.Exist);
                Assert.That(_folders.Scripts, Does.Exist);
                Assert.That(_folders.Editor, Does.Exist);
                Assert.That(_folders.Runtime, Does.Exist);
                Assert.That(_folders.Tests, Does.Not.Exist);
                Assert.That(_folders.EditMode, Does.Not.Exist);
                Assert.That(_folders.Infrastructure, Does.Not.Exist);
                Assert.That(_folders.PlayMode, Does.Not.Exist);
                Assert.That(_folders.Shaders, Does.Exist);
                Assert.That(_folders.SourceUI, Does.Not.Exist);

                DeleteFolders();
            }

            [Test]
            public void GeneratesUiToolkitFolders_WhenIncludeUiToolkitFoldersOptionIsTrue()
            {
                FolderStructureOptions options = new() { includeUiToolkitFolders = true };

                _generator.Generate(options);

                Assert.That(_folders.SettingsUI, Does.Exist);
                Assert.That(_folders.SourceUI, Does.Exist);

                DeleteFolders();
            }

            [Test]
            public void GeneratesTestingFolders_WhenIncludeTestingFoldersOptionIsTrue()
            {
                FolderStructureOptions options = new() { includeTestingFolders = true };

                _generator.Generate(options);

                Assert.That(_folders.Tests, Does.Exist);
                Assert.That(_folders.EditMode, Does.Exist);
                Assert.That(_folders.Infrastructure, Does.Exist);
                Assert.That(_folders.PlayMode, Does.Exist);

                DeleteFolders();
            }
        }
    }
}