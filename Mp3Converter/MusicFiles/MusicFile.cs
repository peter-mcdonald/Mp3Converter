using System;

namespace Mp3Converter.MusicFiles
{
    internal class MusicFile : IEquatable<MusicFile>
    {
        public string OriginalFile { get; set; }
        public string FileName { get; set; }
        public string DisplayName { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public bool FromDirectory { get; set; }
        public string TitleAndArtist 
        {
            get
            {
                return Title.Trim() + " - " + Artist.Trim();
            }
        }

        public string FileExtention { get; set; }

        public bool Equals(MusicFile other)
        {
            return OriginalFile == other.OriginalFile;
        }
    }
}