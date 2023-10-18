using System.IO;
using ArtCom.ProjectSetupTool.Editor.Controllers;
using ArtCom.ProjectSetupTool.Editor.Models;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using UnityEngine;
using static System.IO.Path;

namespace ArtCom.ProjectSetupTool.Tests.Editor
{
    public class AssemblyDefinitionsGeneratorTests
    {
        private const string RootNamespace = "ArtCom.ProjectName";
        private const string Extension = ".asmdef";

        private readonly FolderStructure _folders;
        private readonly AssemblyDefinitionsGenerator _generator;

        protected AssemblyDefinitionsGeneratorTests()
        {
            _folders = new FolderStructure();
            _generator = new AssemblyDefinitionsGenerator();
        }

        [TearDown]
        public void TeardownAfterTestFailure()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                DeleteFolders();
            }
        }

        public class TheGenerateMethod : AssemblyDefinitionsGeneratorTests
        {
            private readonly string _fullThirdPartyAssemblyPath;
            private readonly string _fullRuntimeAssemblyPath;
            private readonly string _fullEditorAssemblyPath;
            private readonly string _fullEditModeAssemblyPath;
            private readonly string _fullPlayModeAssemblyPath;
            private readonly string _fullInfrastructureAssemblyPath;

            public TheGenerateMethod()
            {
                string thirdPartyAssemblyName = $"{RootNamespace}.{_folders.ThirdParty.Name}{Extension}";
                string runtimeAssemblyName = $"{RootNamespace}.{_folders.Runtime.Name}{Extension}";
                string editorAssemblyName = $"{RootNamespace}.{_folders.Editor.Name}{Extension}";
                string editModeAssemblyName = $"{RootNamespace}.{_folders.Tests.Name}.{_folders.EditMode.Name}{Extension}";
                string playModeAssemblyName = $"{RootNamespace}.{_folders.Tests.Name}.{_folders.PlayMode.Name}{Extension}";
                string infrastructureAssemblyName = $"{RootNamespace}.{_folders.Tests.Name}.{_folders.Infrastructure.Name}{Extension}";

                _fullThirdPartyAssemblyPath = Combine(_folders.ThirdParty.FullName, thirdPartyAssemblyName);
                _fullRuntimeAssemblyPath = Combine(_folders.Runtime.FullName, runtimeAssemblyName);
                _fullEditorAssemblyPath = Combine(_folders.Editor.FullName, editorAssemblyName);
                _fullEditModeAssemblyPath = Combine(_folders.EditMode.FullName, editModeAssemblyName);
                _fullPlayModeAssemblyPath = Combine(_folders.PlayMode.FullName, playModeAssemblyName);
                _fullInfrastructureAssemblyPath = Combine(_folders.Infrastructure.FullName, infrastructureAssemblyName);
            }

            [Test]
            public void GeneratesAllAssemblyDefinitionFilesInTheSpecifiedDirectories()
            {
                CreateFoldersForTest();

                _generator.Generate(RootNamespace, new InstallPackagesInfo());

                Assert.That(_fullThirdPartyAssemblyPath, Does.Exist);
                Assert.That(_fullRuntimeAssemblyPath, Does.Exist);
                Assert.That(_fullEditorAssemblyPath, Does.Exist);
                Assert.That(_fullEditModeAssemblyPath, Does.Exist);
                Assert.That(_fullPlayModeAssemblyPath, Does.Exist);
                Assert.That(_fullInfrastructureAssemblyPath, Does.Exist);

                DeleteFolders();
            }

            [Test]
            public void DoesNotGenerateAssemblyDefinitionFiles_WhenTheGivenDirectoriesDoNotExist()
            {
                _generator.Generate(RootNamespace, new InstallPackagesInfo());

                Assert.That(_fullThirdPartyAssemblyPath, Does.Not.Exist);
                Assert.That(_fullRuntimeAssemblyPath, Does.Not.Exist);
                Assert.That(_fullEditorAssemblyPath, Does.Not.Exist);
                Assert.That(_fullEditModeAssemblyPath, Does.Not.Exist);
                Assert.That(_fullPlayModeAssemblyPath, Does.Not.Exist);
                Assert.That(_fullInfrastructureAssemblyPath, Does.Not.Exist);
            }

            [Test]
            public void DoesNotGenerateAssemblyDefinitionFilesAgain_WhenTheyAlreadyExist()
            {
                CreateFoldersForTest();

                _generator.Generate(RootNamespace, new InstallPackagesInfo());
                _generator.Generate(RootNamespace, new InstallPackagesInfo());

                const string wildcard = "*";
                string[] thirdPartyFiles = Directory.GetFiles(_folders.ThirdParty.FullName, wildcard + Extension);
                string[] runtimePartyFiles = Directory.GetFiles(_folders.Runtime.FullName, wildcard + Extension);
                string[] editorPartyFiles = Directory.GetFiles(_folders.Editor.FullName, wildcard + Extension);
                string[] editModePartyFiles = Directory.GetFiles(_folders.EditMode.FullName, wildcard + Extension);
                string[] playModePartyFiles = Directory.GetFiles(_folders.PlayMode.FullName, wildcard + Extension);
                string[] infrastructurePartyFiles = Directory.GetFiles(_folders.Infrastructure.FullName, wildcard + Extension);

                Assert.That(thirdPartyFiles, Has.Length.EqualTo(1));
                Assert.That(runtimePartyFiles, Has.Length.EqualTo(1));
                Assert.That(editorPartyFiles, Has.Length.EqualTo(1));
                Assert.That(editModePartyFiles, Has.Length.EqualTo(1));
                Assert.That(playModePartyFiles, Has.Length.EqualTo(1));
                Assert.That(infrastructurePartyFiles, Has.Length.EqualTo(1));

                DeleteFolders();
            }

            [Test]
            public void ReplacesOldAssemblyDefinitionFiles_WhenTheRootNamespaceChanged()
            {
                CreateFoldersForTest();

                _generator.Generate(RootNamespace, new InstallPackagesInfo());
                _generator.Generate("AnotherNamespace", new InstallPackagesInfo());

                const string wildcard = "*";
                string[] thirdPartyFiles = Directory.GetFiles(_folders.ThirdParty.FullName, wildcard + Extension);
                string[] runtimePartyFiles = Directory.GetFiles(_folders.Runtime.FullName, wildcard + Extension);
                string[] editorPartyFiles = Directory.GetFiles(_folders.Editor.FullName, wildcard + Extension);
                string[] editModePartyFiles = Directory.GetFiles(_folders.EditMode.FullName, wildcard + Extension);
                string[] playModePartyFiles = Directory.GetFiles(_folders.PlayMode.FullName, wildcard + Extension);
                string[] infrastructurePartyFiles = Directory.GetFiles(_folders.Infrastructure.FullName, wildcard + Extension);

                Assert.That(thirdPartyFiles, Has.Length.EqualTo(1).And.Not.Contains(_fullThirdPartyAssemblyPath));
                Assert.That(runtimePartyFiles, Has.Length.EqualTo(1).And.Not.Contains(_fullRuntimeAssemblyPath));
                Assert.That(editorPartyFiles, Has.Length.EqualTo(1).And.Not.Contains(_fullEditorAssemblyPath));
                Assert.That(editModePartyFiles, Has.Length.EqualTo(1).And.Not.Contains(_fullEditModeAssemblyPath));
                Assert.That(playModePartyFiles, Has.Length.EqualTo(1).And.Not.Contains(_fullPlayModeAssemblyPath));
                Assert.That(infrastructurePartyFiles, Has.Length.EqualTo(1).And.Not.Contains(_fullInfrastructureAssemblyPath));

                DeleteFolders();
            }

            [Test]
            public void AddsUniTaskReferenceIntoAssemblies_WhenUniTaskPackageIsInstalled()
            {
                CreateFoldersForTest();
                InstallPackagesInfo packages = new() { hasUniTask = true };

                _generator.Generate(RootNamespace, packages);

                AssemblyDefinition runtimeAssembly = GetAssemblyDefinition(_fullRuntimeAssemblyPath);
                AssemblyDefinition testInfrastructureAssembly = GetAssemblyDefinition(_fullInfrastructureAssemblyPath);
                AssemblyDefinition editModeAssembly = GetAssemblyDefinition(_fullEditModeAssemblyPath);
                AssemblyDefinition playModeAssembly = GetAssemblyDefinition(_fullPlayModeAssemblyPath);

                const string uniTaskReference = "UniTask";
                Assert.That(runtimeAssembly.references, Does.Contain(uniTaskReference));
                Assert.That(testInfrastructureAssembly.references, Does.Contain(uniTaskReference));
                Assert.That(editModeAssembly.references, Does.Contain(uniTaskReference));
                Assert.That(playModeAssembly.references, Does.Contain(uniTaskReference));

                DeleteFolders();
            }

            [Test]
            public void AddsMemoryPackReferenceIntoAssemblies_WhenMemoryPackPackageIsInstalled()
            {
                CreateFoldersForTest();
                InstallPackagesInfo packages = new() { hasMemoryPack = true };

                _generator.Generate(RootNamespace, packages);

                AssemblyDefinition runtimeAssembly = GetAssemblyDefinition(_fullRuntimeAssemblyPath);
                Assert.That(runtimeAssembly.references, Does.Contain("MemoryPack"));

                DeleteFolders();
            }

            [Test]
            public void ThrowsArgumentException_WhenRootNamespaceIsEmpty()
            {
                void GenerateMethod() => _generator.Generate(string.Empty, new InstallPackagesInfo());

                Assert.That(GenerateMethod, Throws.ArgumentException.And.Message.EqualTo(
                        "Cannot generate assemblies with empty root namespace. Please specify root namespace and run setup again."));
            }

            [Test]
            [TestCase("<")]
            [TestCase(">")]
            [TestCase("something with whitespaces")]
            public void ThrowsArgumentException_WhenRootNamespaceContainsInvalidCharacters(string character)
            {
                void GenerateMethod() => _generator.Generate(character, new InstallPackagesInfo());

                Assert.That(GenerateMethod, Throws.ArgumentException.And.Message.EqualTo(
                        "Root namespace contains invalid character/s (<, >) or whitespaces. Please remove those characters and run setup again."));
            }
        }

        private void CreateFoldersForTest()
        {
            _folders.ThirdParty.Create();
            _folders.Source.Create();

            _folders.Scripts.Create();
            _folders.Runtime.Create();
            _folders.Editor.Create();

            _folders.Tests.Create();
            _folders.EditMode.Create();
            _folders.Infrastructure.Create();
            _folders.PlayMode.Create();
        }

        private void DeleteFolders()
        {
            _folders.ThirdParty.Delete(true);
            _folders.Source.Delete(true);
        }

        private static AssemblyDefinition GetAssemblyDefinition(string path)
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<AssemblyDefinition>(json);
        }
    }
}