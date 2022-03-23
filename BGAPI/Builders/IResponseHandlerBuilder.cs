using BGAPI.Handlers;

namespace BGAPI.Builders
{
    internal interface IResponseHandlerBuilder
    {
        IResponseHandler Create();
    }
}