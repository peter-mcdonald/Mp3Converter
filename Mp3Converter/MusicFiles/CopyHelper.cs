using System.IO;

namespace Mp3Converter.MusicFiles
{
    internal class CopyHelper
    {
        private const string WMA_EXT = ".WMA";
        private const string MP3_EXT = ".MP3";

        readonly CopySettings settings;

        public CopyHelper(CopySettings settings)
        {
            this.settings = settings;
        }

        public void CopyMusicFile(MusicFile fileToCopy)
        {
            var outputFile = Path.Combine(settings.OutputFolder, fileToCopy.TitleAndArtist + fileToCopy.FileExtention);
            MakeWMAPreferred(fileToCopy);
            File.Copy(fileToCopy.OriginalFile, outputFile, settings.Overwrite);
        }

        private void MakeWMAPreferred(MusicFile fileToCopy)
        {
            var ext = fileToCopy.FileExtention.ToUpper();

            if (ext != WMA_EXT) return;

            var mp3File = Path.Combine(settings.OutputFolder, fileToCopy.TitleAndArtist + MP3_EXT);
            if (File.Exists(mp3File))
            {
                File.Delete(mp3File);
            }
        }
    }
}
