using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Mp3Converter.Converter
{
    class LameConverter : IAudioConverter
    {
        readonly string wavFile;
        readonly string mp3File;
        readonly string bitRate;

        public LameConverter(string wavFile, string mp3File, string bitRate)
        {
            this.wavFile = wavFile;
            this.mp3File = mp3File;
            this.bitRate = bitRate;
        }

        public void Convert()
        {
            var lameProcess = new Process();
            lameProcess.StartInfo.FileName = BuildLamePath(); ;
            lameProcess.StartInfo.UseShellExecute = false;
            lameProcess.StartInfo.Arguments = BuildArguments();
            lameProcess.StartInfo.CreateNoWindow = false;
            lameProcess.Start();

            lameProcess.WaitForExit();

        }
        
        string BuildLamePath()
        {
            var currentPath = Path.GetDirectoryName(Application.ExecutablePath);
            return Path.Combine(currentPath, "Lame", "lame.exe");
        }

        string BuildArguments()
        {
            return String.Format("-b {0} \"{1}\" \"{2}\"", bitRate, wavFile, mp3File);
        }
    }
}
