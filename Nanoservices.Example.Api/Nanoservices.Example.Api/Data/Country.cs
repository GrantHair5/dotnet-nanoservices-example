using System.Collections.Generic;

namespace Nanoservices.Example.Api.Data
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CountriesData
    {
        public IEnumerable<Country> Countries { get; set; }

        public CountriesData()
        {
            Countries = new List<Country>
            {
                new Country
                {
                    Id = 1,
                    Name = "Canada"
                },
                new Country
                {
                    Id = 2,
                    Name = "USA"
                },
                new Country
                {
                    Id = 3,
                    Name = "Mexico"
                }
            };
        }
    }
}