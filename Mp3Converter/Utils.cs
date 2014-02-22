using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Mp3Converter
{
    class Utils
    {
        public static void ShowBusyMouse()
        {
            Application.UseWaitCursor = true;
        }

        public static void ShowNormalMouse()
        {
            Application.UseWaitCursor = false;
        }

        public static string CleanFileName(string fileName)
        {
            const string pattern = "[\"\\/*:?<>|]";

            if (Regex.IsMatch(fileName, pattern))
            {
                return Regex.Replace(fileName, pattern, string.Empty);
            }

            return fileName;
        }

        public static string GetMp3FileName(string fileName)
        {
            var file = Path.GetFileNameWithoutExtension(fileName);
            var filePath = fileName.Replace(Path.GetFileName(fileName),string.Empty);
            return Path.Combine(filePath, file + ".mp3");
        }
    }
}
