using BGAPI.Entities;

namespace BGAPI.Workers
{
    internal interface IDownloader
    {
        FileData Download(string url);
    }
}