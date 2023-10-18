# Setup Tool for Unity Projects

<img src="/Documentation/Window_Screenshot.png">

This adds a custom editor window which allows to to automate and customize the typical setup
of a Unity project. It does the following:
* Creates a folder structure
* Generates assembly definitions for runtime, editor and testing scripts
* Adjusts common project settings
* Gives options to install some useful packages (new input system included by default)
* Sets up a git repository

## Getting started
Minimum supported version: 2021.3.0f1

Install the tool via the package manager by going to `Add package from git URL` and pasting
the following url:

`https://github.com/artcom/unity-project-setup-tool.git`

Then you can open the window by going to: `Tools > Project Setup`

Once you've made your adjustments, click on the `Execute Setup` button to perform the setup.

After the setup is complete, you can remove this package.

CAUTION: Do not run GitInitializer tests after git was set up, because the repository will
be deleted in the process.

### For Developers
Clone this project inside the `Packages` folder of a Unity project if you want to make any
changes.

## Folder Structure
The setup creates the following folders under `Assets`:
```
Assets
├── 3rdParty
├── Audio
│   ├── Music
│   └── Sound
├── Graphics
│   ├── Animations
│   ├── Fonts
│   ├── Materials
│   ├── Models
│   ├── ShaderGraphs
│   ├── Sprites
│   ├── Textures
│   └── VFX
├── Plugins
├── Prefabs
├── Resources
├── Scenes
├── Settings
│   ├── Input
│   ├── RenderPipeline
│   └── UI
└── Source
    ├── Scripts
    │   ├── Editor
    │   ├── Runtime
    │   └── Tests
    │       ├── EditMode
    │       ├── Infrastructure
    │       └── PlayMode
    ├── Shaders
    └── UI
```
The `UI` folders are designated for UI toolkit and can be included or excluded.
The `Tests` folders can be included or excluded as well.

On top of that, each folder that doesn't have a file inside is created with a `.keep`
file to make sure that it's getting checked in with git.

## Assembly Definitions
In order to generate assembly definition files, you have to specify the root namespace.
Failing to do so will skip the generation process and you will have to run the setup again.

Note that the assembly definition references are done via string names. It is recommended
to click on the `Use GUIDs` checkbox in the assembly definition files to avoid getting
errors if for whatever reason you need to rename them.

## Project Settings
The tool provides options to set some common project settings. They are adjusted to fit
recommendations, but you can change them if you like.

## Package Install Options
The Inputsystem package gets installed by default. On top of that, you have the option to
install the following packages:
* UniTask
* MemoryPack
* Graphy

MemoryPack also downloads `System.Runtime.CompilerServices.Unsafe.dll` to the `Plugins`
folder to make sure that it works.

All necessary references are automatically added to the assembly definitions.

## Git
You can choose to create a git repository when running the setup. You can also specify, if
you want to use git LFS and link to a remote repository. The setup will then run all the
commands, create an initial commit and push it up to the remote.

The changes to the package manifest due to installed packages is not included in the initial
commit, so you will have to commit that separately.

Note that you need to have git installed and properly configured, else that part of the
setup will fail.
