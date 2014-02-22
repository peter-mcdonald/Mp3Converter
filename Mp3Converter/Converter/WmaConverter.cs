using System.IO;
using MusicTagEditor;
using NAudio.Wave;

namespace Mp3Converter.Converter
{
    public class WmaConverter : IAudioConverter
    {
        readonly string wmaFile;
        readonly string mp3File;

        public WmaConverter(string wmaFile, string mp3File)
        {
            this.wmaFile = wmaFile;
            this.mp3File = mp3File;
        }

        public void Convert()
        {
            var tempWavFile = GetTemporaryWavFileName();
            var bitRate = new TagLibEditor().GetBitRate(wmaFile);

            using (var reader = new MediaFoundationReader(wmaFile))
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