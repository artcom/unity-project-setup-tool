using System.Collections.Generic;

namespace ArtCom.ProjectSetupTool.Editor.Models
{
    public readonly struct AssemblyDefinitionCollection
    {
        private const string EditorPlatform = "Editor";
        private const string EngineTestRunnerReference = "UnityEngine.TestRunner";
        private const string EditorTestRunnerReference = "UnityEditor.TestRunner";
        private const string NunitDllReference = "nunit.framework.dll";
        private const string IncludeTestsConstraint = "UNITY_INCLUDE_TESTS";
        private const string UniTaskReference = "UniTask";

        public readonly AssemblyDefinition runtime;
        public readonly AssemblyDefinition editor;
        public readonly AssemblyDefinition thirdParty;

        public readonly AssemblyDefinition editModeTest;
        public readonly AssemblyDefinition playModeTest;
        public readonly AssemblyDefinition testInfrastructure;

        public AssemblyDefinitionCollection(string rootNamespace, InstallPackagesInfo packages)
        {
            FolderStructure folders = new();

            thirdParty = new AssemblyDefinition
            {
                name = rootNamespace + $".{folders.ThirdParty.Name}",
                rootNamespace = rootNamespace,
                autoReferenced = true
            };

            runtime = new AssemblyDefinition
            {
                name = rootNamespace + $".{folders.Runtime.Name}",
                rootNamespace = rootNamespace,
                references = new List<string>
                {
                    "Unity.InputSystem",
                    "Unity.TextMeshPro",
                    thirdParty.name
                },
                autoReferenced = true
            };

            editor = new AssemblyDefinition
            {
                name = rootNamespace + $".{folders.Editor.Name}",
                rootNamespace = rootNamespace,
                includePlatforms = new [] { EditorPlatform },
                references = new List<string> { thirdParty.name },
                autoReferenced = true
            };

            testInfrastructure = new AssemblyDefinition
            {
                name = rootNamespace + $".{folders.Tests.Name}.{folders.Infrastructure.Name}",
                rootNamespace = rootNamespace,
                autoReferenced = true,
                defineConstraints = new [] { IncludeTestsConstraint }
            };

            editModeTest = new AssemblyDefinition
            {
                name = rootNamespace + $".{folders.Tests.Name}.{folders.EditMode.Name}",
                rootNamespace = rootNamespace,
                references = new List<string>
                {
                    EngineTestRunnerReference,
                    EditorTestRunnerReference,
                    runtime.name,
                    testInfrastructure.name
                },
                includePlatforms = new [] { EditorPlatform },
                overrideReferences = true,
                precompiledReferences = new List<string> { NunitDllReference },
                defineConstraints = new [] { IncludeTestsConstraint }
            };

            playModeTest = new AssemblyDefinition
            {
                name = rootNamespace + $".{folders.Tests.Name}.{folders.PlayMode.Name}",
                rootNamespace = rootNamespace,
                references = new List<string>
                {
                    EngineTestRunnerReference,
                    EditorTestRunnerReference,
                    runtime.name,
                    testInfrastructure.name
                },
                overrideReferences = true,
                precompiledReferences = new List<string> { NunitDllReference },
                defineConstraints = new [] { IncludeTestsConstraint }
            };

            AddAdditionalReferences(packages);
        }

        private void AddAdditionalReferences(InstallPackagesInfo packages)
        {
            if (packages.hasUniTask)
            {
                runtime.references.Add(UniTaskReference);
                testInfrastructure.references.Add(UniTaskReference);
                editModeTest.references.Add(UniTaskReference);
                playModeTest.references.Add(UniTaskReference);
            }

            if (packages.hasMemoryPack)
            {
                runtime.references.Add("MemoryPack");
            }
        }
    }
}