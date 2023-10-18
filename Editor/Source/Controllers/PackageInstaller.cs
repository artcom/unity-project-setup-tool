using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ArtCom.ProjectSetupTool.Editor.Models;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.Networking;

namespace ArtCom.ProjectSetupTool.Editor.Controllers
{
    public class PackageInstaller
    {
        private readonly FolderStructure _folders = new();

        public void Install(InstallPackagesInfo packagesInfo)
        {
            List<string> packagesToAdd = new() { RecommendedPackages.inputSystem };
            if (packagesInfo.hasUniTask)
            {
                packagesToAdd.Add(RecommendedPackages.uniTask);
            }

            if (packagesInfo.hasMemoryPack)
            {
                DownloadRequiredDllForMemoryPack();
                packagesToAdd.Add(RecommendedPackages.memoryPack);
            }

            if (packagesInfo.hasGraphy)
            {
                packagesToAdd.Add(RecommendedPackages.graphy);
            }

            AddPackages(packagesToAdd.ToArray());
        }

        private async void DownloadRequiredDllForMemoryPack()
        {
            if (!_folders.Plugins.Exists) { return; }

            const string dllName = "System.Runtime.CompilerServices.Unsafe.dll";
            string url = $"https://github.com/Cysharp/MemoryPack/raw/main/src/MemoryPack.Unity/Assets/Plugins/{dllName}";
            string path = Path.Combine(Application.dataPath, _folders.Plugins.Name, dllName);

            if (File.Exists(path)) { return; }

            using UnityWebRequest request = UnityWebRequest.Get(url);
            request.downloadHandler = new DownloadHandlerFile(path);

            UnityWebRequestAsyncOperation operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            HandleWebRequestResult(request);
        }

        private static void HandleWebRequestResult(UnityWebRequest request)
        {
            if (request.result == UnityWebRequest.Result.Success)
            {
                AssetDatabase.Refresh();
            }
            else
            {
                Debug.LogError(request.error);
            }
        }

        private static async void AddPackages(string[] packages)
        {
            AddAndRemoveRequest request = Client.AddAndRemove(packages);
            while (!request.IsCompleted)
            {
                await Task.Yield();
            }

            if (request.Status != StatusCode.Success)
            {
                Debug.LogError($"{request.Error.errorCode}: {request.Error.message}");
            }
        }
    }
}