using System;
using System.Linq;

namespace ArtCom.ProjectSetupTool.Editor.Controllers
{
    public static class NamespaceValidator
    {
        public static void Validate(string rootNamespace)
        {
            if (string.IsNullOrEmpty(rootNamespace))
            {
                throw new ArgumentException(
                    "Cannot generate assemblies with empty root namespace. Please specify root namespace and run setup again.");
            }

            if (HasInvalidCharacter(rootNamespace))
            {
                throw new ArgumentException(
                    "Root namespace contains invalid character/s (<, >) or whitespaces. Please remove those characters and run setup again.");
            }
        }

        public static bool HasInvalidCharacter(string rootNamespace) => rootNamespace.Contains("<") ||
                                                                        rootNamespace.Contains(">") ||
                                                                        rootNamespace.Any(char.IsWhiteSpace);
    }
}