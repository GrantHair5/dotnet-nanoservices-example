using System.Collections.Generic;
using LiteDB;

namespace Nanoservices.Example.Api.Data
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CountriesData
    {
        public CountriesData()
        {
            using var db = new LiteDatabase(@"./Countries.db");
            var col = db.GetCollection<Country>("countries");

            if (col.Count() > 0)
            {
                return;
            }

            var countries = new List<Country>
            {
                new Country
                {
                    Id = 1,
                    Name = "Scotland"
                },
                new Country
                {
                    Id = 2,
                    Name = "Brazil"
                },
                new Country
                {
                    Id = 3,
                    Name = "Chile"
                },
                new Country
                {
                    Id = 4,
                    Name = "Wales"
                },
            };

            col.InsertBulk(countries);
        }
    }
}