using ArtCom.ProjectSetupTool.Editor.Controllers;
using ArtCom.ProjectSetupTool.Editor.Models;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using UnityEditor;

namespace ArtCom.ProjectSetupTool.Tests.Editor
{
    public class ProjectSettingsInitializerTests
    {
        private const string PackageNamespace = "ArtCom.ProjectSetupTool";

        private readonly ProjectSettingsInitializer _projectSettingsInitializer = new();

        [TearDown]
        public void TeardownAfterTestFailure()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) { return; }

            ResetProjectSettings();
        }

        public class TheInitializeMethod : ProjectSettingsInitializerTests
        {
            [Test]
            public void SetsProjectSettingsToTheGivenValues()
            {
                const string expectedNamespace = "CompanyName.ProjectName";
                ProjectSettings settings = new()
                {
                    rootNamespace = expectedNamespace,
                    isPrefabAutoSaveDisabled = true,
                    isParallelAssetImportEnabled = true,
                    isEnterPlayModeOptionsEnabled = true,
                    spritePackerMode = SpritePackerMode.SpriteAtlasV2,
                    gameObjectNamingScheme = EditorSettings.NamingScheme.Underscore,
                    gameObjectNamingDigits = 2
                };
                ResetProjectSettings();

                _projectSettingsInitializer.Initialize(settings);

                Assert.That(EditorSettings.projectGenerationRootNamespace, Is.EqualTo(expectedNamespace));
                Assert.That(EditorSettings.prefabModeAllowAutoSave, Is.False);
                Assert.That(EditorSettings.refreshImportMode, Is.EqualTo(AssetDatabase.RefreshImportMode.OutOfProcessPerQueue));
                Assert.That(EditorSettings.enterPlayModeOptionsEnabled, Is.True);
                Assert.That(EditorSettings.spritePackerMode, Is.EqualTo(SpritePackerMode.SpriteAtlasV2));
                Assert.That(EditorSettings.gameObjectNamingScheme, Is.EqualTo(EditorSettings.NamingScheme.Underscore));
                Assert.That(EditorSettings.gameObjectNamingDigits, Is.EqualTo(2));

                ResetProjectSettings();
            }

            [Test]
            [TestCase("<")]
            [TestCase(">")]
            [TestCase("something with whitespaces")]
            public void DoesNotChangeRootNamespace_WhenInputContainsIllegalCharacter(string character)
            {
                ProjectSettings settings = ProjectSettings.Default();
                settings.rootNamespace = character;

                _projectSettingsInitializer.Initialize(settings);

                Assert.That(EditorSettings.projectGenerationRootNamespace, Is.EqualTo(PackageNamespace));

                ResetProjectSettings();
            }
        }

        private static void ResetProjectSettings()
        {
            EditorSettings.projectGenerationRootNamespace = PackageNamespace;
            EditorSettings.prefabModeAllowAutoSave = true;
            EditorSettings.refreshImportMode = AssetDatabase.RefreshImportMode.InProcess;
            EditorSettings.enterPlayModeOptionsEnabled = false;
            EditorSettings.spritePackerMode = SpritePackerMode.Disabled;
            EditorSettings.gameObjectNamingScheme = EditorSettings.NamingScheme.SpaceParenthesis;
            EditorSettings.gameObjectNamingDigits = 1;
        }
    }
}