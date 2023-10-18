using System.Text;

namespace ArtCom.ProjectSetupTool.Editor.Extensions
{
    public static class StringBuilderExtensions
    {
        public static int IndexOf(this StringBuilder sb, string value, int startIndex = 0)
        {
            int length = value.Length;
            int maxSearchLength = sb.Length - length + 1;

            for (int i = startIndex; i < maxSearchLength; ++i)
            {
                if (sb[i] != value[0]) { continue; }

                int index = 1;
                while (index < length && sb[i + index] == value[index])
                {
                    ++index;
                }

                if (index == length)
                {
                    return i;
                }
            }

            return -1;
        }

        public static StringBuilder InsertAfter(this StringBuilder sb, string content, string character)
        {
            int endIndexOfCharacter = sb.IndexOf(character) + character.Length;
            if (endIndexOfCharacter <= sb.Length)
            {
                sb.Insert(endIndexOfCharacter, content);
            }

            return sb;
        }
    }
}