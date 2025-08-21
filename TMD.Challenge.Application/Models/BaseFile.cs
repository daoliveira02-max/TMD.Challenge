namespace TMD.Challenge.Application.Models
{
    public class BaseFile
    {
        public string FilePath { get; set; }
        public string FileName => Path.GetFileName(FilePath);
    }
}
