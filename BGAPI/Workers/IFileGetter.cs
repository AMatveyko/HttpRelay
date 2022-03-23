using BGAPI.Entities;

namespace BGAPI.Workers
{
    public interface IFileGetter
    {
        FileData Get();
    }
}