namespace TMD.Challenge.Application.Models
{
    public class MediaFile : BaseFile
    {
        // Common
        public string Format { get; set; }
        public string Duration { get; set; }
        public string Codec { get; set; }
        public bool IsVideo { get; set; }
        public bool IsAudio { get; set; }

        // Video-specific
        public string Resolution { get; set; }
        public string? FrameRate { get; set; }

        // Audio-specific
        public string? SampleRate { get; set; }
        public string? BitRate { get; set; }
    }
}
