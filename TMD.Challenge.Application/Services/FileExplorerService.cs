using TMD.Challenge.Application.Interfaces;
using TMD.Challenge.Application.Models;

namespace TMD.Challenge.Application.Services
{
    public class FileExplorerService : IFileExplorerService
    {
        public List<BaseFile> GetFilesFromFolder(string folderPath)
        {
            if (!Directory.Exists(folderPath))
                throw new DirectoryNotFoundException($"Folder not found: {folderPath}");

            var supportedExtensions = new[] { ".mp4", ".mp3", ".wav", ".avi", ".mkv" };

            var files = Directory.GetFiles(folderPath)
                                 .Where(f => supportedExtensions.Contains(Path.GetExtension(f).ToLower()))
                                 .ToList();

            return files.Select(file => new BaseFile
            {
                FilePath = file
            }).ToList();
        }
    }
}
