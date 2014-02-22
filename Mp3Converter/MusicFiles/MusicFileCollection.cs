using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using MusicTagEditor;
using System.Linq;

namespace Mp3Converter.MusicFiles
{
    internal class MusicFileCollection
    {
        string[] AllowedToAdd = new string[] {".WMA",".AIF",".WAV"};
        private TagLibEditor tagEditor;

        public List<MusicFile> Files { get; private set; }

        public MusicFileCollection()
        {
            Files = new List<MusicFile>();
            tagEditor = new TagLibEditor();
        }

        public void Add(MusicFile item)
        {
            if (!Files.Contains(item))
            {
                Files.Add(item);
            }
        }

        public void Clear()
        {
            Files.Clear();
        }

        public bool Contains(MusicFile item)
        {
            return Files.Contains(item);
        }

        public void AddFiles(string[] fileList, bool fromDirectory)
        {
            foreach (var file in fileList)
            {
                var ext = Path.GetExtension(file).ToUpper();

                if (AllowedInCollection(ext))
                {
                    AddToCollection(file, fromDirectory);
                }
            }
        }

        bool AllowedInCollection(string ext)
        {
            return AllowedToAdd.Any(e => e == ext);
        }

        public void AddFiles(string[] fileList)
        {
            this.AddFiles(fileList,false);
        }

        void AddToCollection(string file, bool fromDirectory)
        {
            tagEditor.LoadFileDetails(file);
            var fileName = Path.GetFileNameWithoutExtension(file);

            var musicFile = new MusicFile
                {
                    OriginalFile = file,
                    Artist = tagEditor.Artist,
                    Title = tagEditor.Title,
                    FromDirectory = fromDirectory,
                    FileName = fileName,
                    FileExtention = Path.GetExtension(file),
                    DisplayName = GetDisplayName(fileName)
                };

            Add(musicFile);
        }

        string GetDisplayName(string fileName)
        {
            const string pattern = @"\d\d\s";

            if (Regex.IsMatch(fileName, pattern))
            {
                return Regex.Replace(fileName, pattern, string.Empty);
            }

            return fileName;
        }

        public void UpdateItem(string name, string editedTitle, string editedArtist)
        {
            var updateFile = Files.Single(x => x.OriginalFile == name);

            updateFile.Title = editedTitle;
            updateFile.Artist = editedArtist;
        }

        public void Remove(string fileName)
        {
            var removeFile = Files.Single(x => x.OriginalFile == fileName);
            Files.Remove(removeFile);
        }
    }
}