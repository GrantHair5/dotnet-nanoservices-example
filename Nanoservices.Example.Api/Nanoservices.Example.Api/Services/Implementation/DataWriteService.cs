using LiteDB;

namespace Nanoservices.Example.Api.Services.Implementation
{
    public class DataWriteService : IDataWriteService
    {
        public void Add<T>(T objectToAdd)
        {
            using var db = new LiteDatabase(@"./Countries.db");
            var col = db.GetCollection<T>("countries");
            col.Insert(objectToAdd);
        }
    }
}