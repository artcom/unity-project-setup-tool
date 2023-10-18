using System;
using System.Collections.Generic;

namespace ArtCom.ProjectSetupTool.Editor.Models
{
    [Serializable]
    public class AssemblyDefinition
    {
        public string name;
        public string rootNamespace;
        public List<string> references = new();
        public string[] includePlatforms;
        public bool overrideReferences;
        public List<string> precompiledReferences = new();
        public bool autoReferenced;
        public string[] defineConstraints;
    }
}