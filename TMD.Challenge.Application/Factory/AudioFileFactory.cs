using MediaInfo.DotNetWrapper.Enumerations;
using TMD.Challenge.Application.Factory.Interfaces;
using TMD.Challenge.Application.Models;
using MI = MediaInfo.DotNetWrapper;

namespace TMD.Challenge.Application.Factory
{
    public class AudioFileFactory : IMediaFileFactory
    {
        public MediaFile Create(BaseFile baseFile)
        {
            using var mediaInfo = new MI.MediaInfo();
            mediaInfo.Open(baseFile.FilePath);

            var mediaFile = new MediaFile
            {
                FilePath = baseFile.FilePath,
                IsAudio = true,
                Format = mediaInfo.Get(StreamKind.General, 0, "Format") ?? "n/a",
                Duration = mediaInfo.Get(StreamKind.General, 0, "Duration/String3") ?? "n/a",
                Codec = mediaInfo.Get(StreamKind.Audio, 0, "Codec/String") ?? "n/a",
                SampleRate = mediaInfo.Get(StreamKind.Audio, 0, "SamplingRate/String"),
                BitRate = mediaInfo.Get(StreamKind.Audio, 0, "BitRate/String")
            };

            mediaInfo.Close();
            return mediaFile;
        }
    }
}
