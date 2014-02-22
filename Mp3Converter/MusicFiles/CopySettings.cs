using System.Collections.Generic;

namespace Mp3Converter.MusicFiles
{
    internal class CopySettings
    {
        public string OutputFolder { get; set; }
        public bool Overwrite { get; set; }
        public List<MusicFile> Files { get; set; }
    }
}