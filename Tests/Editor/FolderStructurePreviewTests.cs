using ArtCom.ProjectSetupTool.Editor.Controllers;
using NUnit.Framework;

namespace ArtCom.ProjectSetupTool.Tests.Editor
{
    public class FolderStructurePreviewTests
    {
        private const string SettingsUiFolderPreview = "\n│   └─ UI";
        private const string SourceUiFolderPreview = "\n       └─ UI";
        private const string TestFoldersPreview = @"
       │   └─ Tests
       │          ├─ EditMode
       │          ├─ Infrastructure
       │          └─ PlayMode";

        private const string RuntimeFolder = "Runtime";
        private const string RenderPipelineFolder = "RenderPipeline";
        private const string ShadersFolder = "\nShaders";
        private const string DebugValue = "Test";

        public class TheValueProperty
        {
            [Test]
            public void ReturnsTheCurrentPreview()
            {
                FolderStructurePreview preview = new(DebugValue);

                Assert.That(preview.Value, Is.EqualTo(DebugValue));
            }
        }

        public class TheAddUiToolkitFoldersToPreviewMethod
        {
            [Test]
            [TestCase(RenderPipelineFolder + ShadersFolder, RenderPipelineFolder + SettingsUiFolderPreview + ShadersFolder + SourceUiFolderPreview)]
            [TestCase(DebugValue, DebugValue)]
            public void UpdatesPreviewWithUiFolders_WhenNoUiFoldersArePresent(string before, string after)
            {
                FolderStructurePreview preview = new(before);

                preview.AddUiToolkitFoldersToPreview();

                Assert.That(preview.Value, Is.EqualTo(after));
            }
        }

        public class TheRemoveUiToolkitFoldersFromPreviewMethod
        {
            [Test]
            [TestCase(RenderPipelineFolder + SettingsUiFolderPreview + ShadersFolder + SourceUiFolderPreview, RenderPipelineFolder + ShadersFolder)]
            [TestCase(RenderPipelineFolder + ShadersFolder, RenderPipelineFolder + ShadersFolder)]
            public void UpdatesPreviewToNoLongerHaveUiFolders_WhenUiFoldersArePresent(string before, string after)
            {
                FolderStructurePreview preview = new(before);

                preview.RemoveUiToolkitFoldersFromPreview();

                Assert.That(preview.Value, Is.EqualTo(after));
            }
        }

        public class TheAddTestingFoldersToPreviewMethod
        {
            [Test]
            [TestCase(RuntimeFolder, RuntimeFolder + TestFoldersPreview)]
            [TestCase(DebugValue, DebugValue)]
            public void UpdatesPreviewWithTestingFolders_WhenNoTestingFolderArePresent(string before, string after)
            {
                FolderStructurePreview preview = new(before);

                preview.AddTestingFoldersToPreview();

                Assert.That(preview.Value, Is.EqualTo(after));
            }
        }

        public class TheRemoveTestingFoldersFromPreviewMethod
        {
            [Test]
            [TestCase(RuntimeFolder + TestFoldersPreview, RuntimeFolder)]
            [TestCase(RuntimeFolder, RuntimeFolder)]
            public void UpdatesPreviewToNoLongerHaveTestingFolders_WhenTestingFoldersArePresent(string before, string after)
            {
                FolderStructurePreview preview = new(before);

                preview.RemoveTestingFoldersFromPreview();

                Assert.That(preview.Value, Is.EqualTo(after));
            }
        }
    }
}