using MongoDB.Driver;

using Cup.Domain.Core.AggregatesModel.ClubAggregate;

namespace Cup.Infrastructure.Data
{
    public class CupContext
    {
        private readonly IMongoDatabase _database = null;

        public CupContext()
        {
            string connectionString = "mongodb://localhost:27017/CricketManager";

            MongoUrlBuilder connection = new MongoUrlBuilder(connectionString);
            MongoClient client = new MongoClient(connectionString);

            _database = client.GetDatabase(connection.DatabaseName);
        }

        public IMongoCollection<Domain.Core.AggregatesModel.CupAggregate.Cup> Cups
        {
            get
            {
                return _database.GetCollection<Domain.Core.AggregatesModel.CupAggregate.Cup>("Cups");
            }
        }

        public IMongoCollection<Club> Clubs
        {
            get
            {
                return _database.GetCollection<Club>("Club_Collection");
            }
        }
    }
}
