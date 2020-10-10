namespace Nanoservices.Example.Api.Services
{
    public interface IDataWriteService
    {
        void Add<T>(T objectToAdd);
    }
}