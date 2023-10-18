using System;
using System.Diagnostics;
using System.IO;
using ArtCom.ProjectSetupTool.Editor.Models;
using UnityEngine;

namespace ArtCom.ProjectSetupTool.Editor.Controllers
{
    public class GitInitializer
    {
        public void Initialize(GitSetupOptions options)
        {
            if (!options.isGitUsed || IsGitAlreadyInitialized()) { return; }

            ProcessStartInfo processInfo = DefineShellScriptProcess(options);

            using Process process = Process.Start(processInfo);
            process?.WaitForExit();
        }

        private static bool IsGitAlreadyInitialized()
        {
            string projectFolder = Path.GetDirectoryName(Application.dataPath);
            string gitPath = Path.Combine(projectFolder!, ".git");

            return Directory.Exists(gitPath);
        }

        private static ProcessStartInfo DefineShellScriptProcess(GitSetupOptions options)
        {
            string scriptLocation = Path.Combine("Packages", "com.artcom.unity-project-setup-tool", "Editor", "git");
            ProcessStartInfo processInfo = new()
            {
                FileName = GetShell(),
                WorkingDirectory = Path.GetFullPath(scriptLocation),
                ArgumentList = {"git_setup.sh", options.UseLfsArgument, options.RemoteUrlArgument},
                UseShellExecute = false
            };

            if (Application.platform != RuntimePlatform.OSXEditor) { return processInfo; }

            // Necessary for the process to find git lfs
            string path = processInfo.EnvironmentVariables["PATH"];
            processInfo.EnvironmentVariables["PATH"] = $"{path}:/usr/local/bin";
            return processInfo;
        }

        private static string GetShell()
        {
            if (Application.platform != RuntimePlatform.WindowsEditor) { return "/bin/bash"; }

            const string gitBashApplication = "git-bash.exe";
            string programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string regularGitBash = Path.Combine(programFiles, "Git", gitBashApplication);
            string sourceTreeGitBash = Path.Combine(localAppData, "Atlassian", "SourceTree", "git_local", gitBashApplication);

            return File.Exists(sourceTreeGitBash) ? sourceTreeGitBash : regularGitBash;

        }
    }
}