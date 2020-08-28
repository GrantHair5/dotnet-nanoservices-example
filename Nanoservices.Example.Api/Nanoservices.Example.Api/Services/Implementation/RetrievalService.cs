using System.Linq;
using System.Text.Json;
using Nanoservices.Example.Api.Data;

namespace Nanoservices.Example.Api.Services.Implementation
{
    public class RetrievalService : IRetrievalService
    {
        private readonly CountriesData _countries = new CountriesData();

        public string Retrieve(int id)
        {
            var country = _countries.Countries.FirstOrDefault(x => x.Id == id);
            var response = country != null ? JsonSerializer.Serialize(country) : "No country found";
            return response;
        }

        public CountriesData RetrieveAll()
        {
            return _countries ?? new CountriesData();
        }
    }
}