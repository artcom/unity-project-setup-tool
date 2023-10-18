using System.IO;
using ArtCom.ProjectSetupTool.Editor.Controllers;
using ArtCom.ProjectSetupTool.Editor.Models;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using UnityEngine;

namespace ArtCom.ProjectSetupTool.Tests.Editor
{
    public class GitInitializerTests
    {
        private readonly GitInitializer _initializer;

        private readonly string _gitFolder;
        private readonly string _lfsFolder;
        private readonly string _gitIgnore;
        private readonly string _gitAttributes;

        public GitInitializerTests()
        {
            _initializer = new GitInitializer();

            string projectFolder = Path.GetDirectoryName(Application.dataPath);

            _gitFolder = Path.Combine(projectFolder!, ".git");
            _lfsFolder = Path.Combine(_gitFolder, "lfs");
            _gitIgnore = Path.Combine(projectFolder, ".gitignore");
            _gitAttributes = Path.Combine(projectFolder, ".gitattributes");
        }

        [TearDown]
        public void TeardownAfterTestFailure()
        {
            if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Failed) { return; }

            RemoveGitRepository();
            File.Delete(_gitAttributes);
        }

        private void RemoveGitRepository()
        {
            DirectoryInfo directory = new(_gitFolder);
            foreach (FileSystemInfo file in directory.GetFileSystemInfos("*", SearchOption.AllDirectories))
            {
                file.Attributes = FileAttributes.Normal;
            }

            directory.Delete(true);
            File.Delete(_gitIgnore);
        }

        public class TheInitializeMethod : GitInitializerTests
        {
            [Test]
            public void CreatesGitRepositoryWithGitIgnoreAndHooks_WhenGitUsedOptionIsTrue()
            {
                GitSetupOptions options = new() { isGitUsed = true };

                _initializer.Initialize(options);

                Assert.That(_gitFolder, Does.Exist);
                Assert.That(_lfsFolder, Does.Not.Exist);
                Assert.That(_gitIgnore, Does.Exist);
                Assert.That(_gitAttributes, Does.Not.Exist);

                RemoveGitRepository();
            }

            [Test]
            public void DoesNotCreateGitRepository_WhenGitUsedOptionIsFalse()
            {
                GitSetupOptions options = new();

                _initializer.Initialize(options);

                Assert.That(_gitFolder, Does.Not.Exist);
                Assert.That(_gitIgnore, Does.Not.Exist);
                Assert.That(_gitAttributes, Does.Not.Exist);
            }

            [Test]
            public void CreatesGitRepositoryWithGitLfs_WhenLfsOptionIsTrue()
            {
                GitSetupOptions options = new()
                {
                    isGitUsed = true,
                    isLfsUsed = true
                };

                _initializer.Initialize(options);

                Assert.That(_lfsFolder, Does.Exist);
                Assert.That(_gitAttributes, Does.Exist);

                RemoveGitRepository();
                File.Delete(_gitAttributes);
            }
        }
    }
}