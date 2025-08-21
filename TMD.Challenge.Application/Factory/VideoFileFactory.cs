using MediaInfo.DotNetWrapper.Enumerations;
using TMD.Challenge.Application.Factory.Interfaces;
using TMD.Challenge.Application.Models;
using MI = MediaInfo.DotNetWrapper;

namespace TMD.Challenge.Application.Factory
{
    public class VideoFileFactory : IMediaFileFactory
    {
        public MediaFile Create(BaseFile baseFile)
        {
            using var mediaInfo = new MI.MediaInfo();
            mediaInfo.Open(baseFile.FilePath);

            var mediaFile = new MediaFile
            {
                FilePath = baseFile.FilePath,
                IsVideo = true,
                Format = mediaInfo.Get(StreamKind.General, 0, "Format") ?? "n/a",
                Duration = mediaInfo.Get(StreamKind.General, 0, "Duration/String3") ?? "n/a",
                Codec = mediaInfo.Get(StreamKind.Video, 0, "Codec/String") ?? "n/a",
                Resolution = $"{mediaInfo.Get(StreamKind.Video, 0, "Width")} x {mediaInfo.Get(StreamKind.Video, 0, "Height")}",
                FrameRate = mediaInfo.Get(StreamKind.Video, 0, "FrameRate/String")
            };

            mediaInfo.Close();
            return mediaFile;
        }
    }
}
