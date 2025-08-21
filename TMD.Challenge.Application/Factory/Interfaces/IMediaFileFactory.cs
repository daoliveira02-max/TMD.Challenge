using TMD.Challenge.Application.Models;

namespace TMD.Challenge.Application.Factory.Interfaces
{
    public interface IMediaFileFactory
    {
        MediaFile Create(BaseFile baseFile);
    }
}
