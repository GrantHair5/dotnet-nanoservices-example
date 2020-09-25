using System.Collections.Generic;
using System.Linq;
using LiteDB;
using Nanoservices.Example.Api.Data;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Nanoservices.Example.Api.Services.Implementation
{
    public class RetrievalService : IRetrievalService
    {
        public string Retrieve(int id)
        {
            using var db = new LiteDatabase(@"./Countries.db");
            var col = db.GetCollection<Country>("countries");
            var country = col.Find(x => x.Id == id);
            var response = country != null ? JsonSerializer.Serialize(country) : "No country found";
            return response;
        }

        public IEnumerable<Country> RetrieveAll()
        {
            using var db = new LiteDatabase(@"./Countries.db");
            var col = db.GetCollection<Country>("countries");

            var countries = col.FindAll();
            return countries;
        }
    }
}