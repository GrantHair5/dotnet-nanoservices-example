using Nanoservices.Example.Api.Data;

namespace Nanoservices.Example.Api.Services
{
    public interface IRetrievalService
    {
        string Retrieve(int id);

        CountriesData RetrieveAll();
    }
}