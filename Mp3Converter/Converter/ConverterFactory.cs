using System;

namespace Mp3Converter.Converter
{
    class ConverterFactory
    {
        readonly string sourceFile;
        readonly string targetFile;
        readonly string bitRate;

        public ConverterFactory(string sourceFile, string targetFile, string bitRate)
        {
            this.sourceFile = sourceFile;
            this.targetFile = targetFile;
            this.bitRate = bitRate;
        }

        public IAudioConverter GetConverter(string extention)
        {
            switch (extention.ToUpper())
            {
                case ".WMA":
                    return new WmaConverter(sourceFile, targetFile);

                case ".WAV":
                    return new LameConverter(sourceFile,targetFile, bitRate);

                case ".AIF":
                    return new AifConverter(sourceFile, targetFile);
            }

            throw new ArgumentException("Extention type not supported");
        }
    }
}
