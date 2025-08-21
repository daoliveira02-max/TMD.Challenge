namespace TMD.Challenge.Application.Models
{
    public class FavoriteFile
    {
        public string FilePath { get; set; }
        public string FileName => Path.GetFileName(FilePath);
        public DateTime AddedAt { get; set; } = DateTime.Now;
    }
}
