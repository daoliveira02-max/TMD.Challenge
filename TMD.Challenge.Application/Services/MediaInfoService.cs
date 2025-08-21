using MI = MediaInfo.DotNetWrapper;
using MediaInfo.DotNetWrapper.Enumerations;
using TMD.Challenge.Application.Models;
using TMD.Challenge.Application.Factory.Interfaces;

namespace TMD.Challenge.Application.Services
{
    public class MediaInfoService
    {
        private readonly IMediaFileFactory _videoFactory;
        private readonly IMediaFileFactory _audioFactory;

        public MediaInfoService(IMediaFileFactory videoFactory, IMediaFileFactory audioFactory)
        {
            _videoFactory = videoFactory;
            _audioFactory = audioFactory;
        }

        public MediaFile CreateMediaFile(BaseFile baseFile)
        {
            using var mediaInfo = new MI.MediaInfo();
            mediaInfo.Open(baseFile.FilePath);

            var hasVideo = !string.IsNullOrWhiteSpace(mediaInfo.Get(StreamKind.Video, 0, "Format"));
            var hasAudio = !string.IsNullOrWhiteSpace(mediaInfo.Get(StreamKind.Audio, 0, "Format"));

            mediaInfo.Close();

            if (hasVideo) return _videoFactory.Create(baseFile);
            if (hasAudio) return _audioFactory.Create(baseFile);

            return new MediaFile
            {
                FilePath = baseFile.FilePath,
                Format = "Unknown"
            };
        }
    }
}
