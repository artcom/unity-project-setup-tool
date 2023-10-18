using System.IO;
using static System.IO.Path;
using static UnityEngine.Application;

namespace ArtCom.ProjectSetupTool.Editor.Models
{
    public class FolderStructure
    {
        private const string UiToolkitFolderName = "UI";

        public readonly DirectoryInfo ThirdParty;

        public readonly DirectoryInfo Audio;
            public readonly DirectoryInfo Music;
            public readonly DirectoryInfo Sound;

        public readonly DirectoryInfo Graphics;
            public readonly DirectoryInfo Animations;
            public readonly DirectoryInfo Fonts;
            public readonly DirectoryInfo Materials;
            public readonly DirectoryInfo Models;
            public readonly DirectoryInfo ShaderGraphs;
            public readonly DirectoryInfo Sprites;
            public readonly DirectoryInfo Textures;
            public readonly DirectoryInfo VFX;

        public readonly DirectoryInfo Plugins;
        public readonly DirectoryInfo Prefabs;
        public readonly DirectoryInfo Resources;
        public readonly DirectoryInfo Scenes;

        public readonly DirectoryInfo Settings;
            public readonly DirectoryInfo Input;
            public readonly DirectoryInfo RenderPipeline;
            public readonly DirectoryInfo SettingsUI;

        public readonly DirectoryInfo Source;
            public readonly DirectoryInfo Scripts;
                public readonly DirectoryInfo Editor;
                public readonly DirectoryInfo Runtime;

                public readonly DirectoryInfo Tests;
                    public readonly DirectoryInfo EditMode;
                    public readonly DirectoryInfo Infrastructure;
                    public readonly DirectoryInfo PlayMode;

            public readonly DirectoryInfo Shaders;
            public readonly DirectoryInfo SourceUI;

        public FolderStructure()
        {
            ThirdParty = new DirectoryInfo(Combine(dataPath, "3rdParty"));

            Audio = new DirectoryInfo(Combine(dataPath, nameof(Audio)));
            Music = new DirectoryInfo(Combine(Audio.FullName, nameof(Music)));
            Sound = new DirectoryInfo(Combine(Audio.FullName, nameof(Sound)));

            Graphics = new DirectoryInfo(Combine(dataPath, nameof(Graphics)));
            Animations = new DirectoryInfo(Combine(Graphics.FullName, nameof(Animations)));
            Fonts = new DirectoryInfo(Combine(Graphics.FullName, nameof(Fonts)));
            Materials = new DirectoryInfo(Combine(Graphics.FullName, nameof(Materials)));
            Models = new DirectoryInfo(Combine(Graphics.FullName, nameof(Models)));
            ShaderGraphs = new DirectoryInfo(Combine(Graphics.FullName, nameof(ShaderGraphs)));
            Sprites = new DirectoryInfo(Combine(Graphics.FullName, nameof(Sprites)));
            Textures = new DirectoryInfo(Combine(Graphics.FullName, nameof(Textures)));
            VFX = new DirectoryInfo(Combine(Graphics.FullName, nameof(VFX)));

            Plugins = new DirectoryInfo(Combine(dataPath, nameof(Plugins)));
            Prefabs = new DirectoryInfo(Combine(dataPath, nameof(Prefabs)));
            Resources = new DirectoryInfo(Combine(dataPath, nameof(Resources)));
            Scenes = new DirectoryInfo(Combine(dataPath, nameof(Scenes)));

            Settings = new DirectoryInfo(Combine(dataPath, nameof(Settings)));
            Input = new DirectoryInfo(Combine(Settings.FullName, nameof(Input)));
            RenderPipeline = new DirectoryInfo(Combine(Settings.FullName, nameof(RenderPipeline)));
            SettingsUI = new DirectoryInfo(Combine(Settings.FullName, UiToolkitFolderName));

            Source = new DirectoryInfo(Combine(dataPath, nameof(Source)));

            Scripts = new DirectoryInfo(Combine(Source.FullName, nameof(Scripts)));
            Runtime = new DirectoryInfo(Combine(Scripts.FullName, nameof(Runtime)));
            Editor = new DirectoryInfo(Combine(Scripts.FullName, nameof(Editor)));

            Tests = new DirectoryInfo(Combine(Scripts.FullName, nameof(Tests)));
            EditMode = new DirectoryInfo(Combine(Tests.FullName, nameof(EditMode)));
            Infrastructure = new DirectoryInfo(Combine(Tests.FullName, nameof(Infrastructure)));
            PlayMode = new DirectoryInfo(Combine(Tests.FullName, nameof(PlayMode)));

            Shaders = new DirectoryInfo(Combine(Source.FullName, nameof(Shaders)));
            SourceUI = new DirectoryInfo(Combine(Source.FullName, UiToolkitFolderName));
        }
    }
}