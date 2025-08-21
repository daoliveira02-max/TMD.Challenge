using TMD.Challenge.Application.Models;

namespace TMD.Challenge.Application.Interfaces
{
    public interface IFileExplorerService
    {
        List<BaseFile> GetFilesFromFolder(string folderPath);
    }
}
