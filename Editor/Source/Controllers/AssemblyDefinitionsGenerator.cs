using System.IO;
using System.Linq;
using ArtCom.ProjectSetupTool.Editor.Models;
using UnityEditor;

namespace ArtCom.ProjectSetupTool.Editor.Controllers
{
    public class AssemblyDefinitionsGenerator
    {
        private const string FileExtension = ".asmdef";

        private readonly FolderStructure _folders = new();

        public void Generate(string rootNamespace, InstallPackagesInfo packages)
        {
            NamespaceValidator.Validate(rootNamespace);

            AssemblyDefinitionCollection assemblyDefinitions = new(rootNamespace, packages);

            GenerateAssemblyDefinition(assemblyDefinitions.thirdParty, _folders.ThirdParty.FullName);
            GenerateAssemblyDefinition(assemblyDefinitions.runtime, _folders.Runtime.FullName);
            GenerateAssemblyDefinition(assemblyDefinitions.editor, _folders.Editor.FullName);

            GenerateAssemblyDefinition(assemblyDefinitions.testInfrastructure, _folders.Infrastructure.FullName);
            GenerateAssemblyDefinition(assemblyDefinitions.editModeTest, _folders.EditMode.FullName);
            GenerateAssemblyDefinition(assemblyDefinitions.playModeTest, _folders.PlayMode.FullName);
        }

        private static void GenerateAssemblyDefinition(AssemblyDefinition assembly, string directoryPath)
        {
            string targetFilePath = Path.Combine(directoryPath, assembly.name + FileExtension);
            if (!Directory.Exists(directoryPath) || File.Exists(targetFilePath)) { return; }

            DeleteOldAssemblyDefinition(directoryPath);

            string json = EditorJsonUtility.ToJson(assembly, true);
            File.WriteAllText(targetFilePath, json);
        }

        private static void DeleteOldAssemblyDefinition(string directoryPath)
        {
            string[] existingAssemblyDefinitionFile = Directory.GetFiles(directoryPath, "*" + FileExtension);
            if (existingAssemblyDefinitionFile.Any())
            {
                File.Delete(existingAssemblyDefinitionFile[0]);
            }
        }
    }
}