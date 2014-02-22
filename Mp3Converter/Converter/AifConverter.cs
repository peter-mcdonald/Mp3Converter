using System.IO;
using MusicTagEditor;
using NAudio.Wave;

namespace Mp3Converter.Converter
{
    public class AifConverter : IAudioConverter
    {
        readonly string aifFile;
        readonly string mp3File;

        public AifConverter(string aifFile, string mp3File)
        {
            this.aifFile = aifFile;
            this.mp3File = mp3File;
        }

        public void Convert()
        {
            var tempWavFile = GetTemporaryWavFileName();
            var bitRate = new TagLibEditor().GetBitRate(aifFile);

            using (var reader = new AiffFileReader(aifFile))
            {
                WaveFileWriter.CreateWaveFile(tempWavFile, reader);
            }

            var mp3 = new LameConverter(tempWavFile, mp3File, bitRate);
            mp3.Convert();
        }

        string GetTemporaryWavFileName()
        {
            var fileName = Path.Combine(Path.GetTempPath(), @"MusicMover.wav");

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            return fileName;
        }
    }
}