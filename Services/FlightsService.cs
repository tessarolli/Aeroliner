using Aeroliner.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Aeroliner.Services;

public class FlightsService
{
    private readonly IMongoCollection<Flight> _flightsCollection;

    public FlightsService(IOptions<MongoStoreDatabaseSettings> mongoStoreDatabaseSettings)
    {
        var mongoClient = new MongoClient(mongoStoreDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(mongoStoreDatabaseSettings.Value.DatabaseName);

        _flightsCollection = mongoDatabase.GetCollection<Flight>("Flights");
    }

    public async Task<List<Flight>> GetAsync() =>
        await _flightsCollection.Find(x => x.DepartureTime >= DateTime.Now && DateTime.Now <= x.ArrivalTime).ToListAsync();

    public async Task<Flight?> GetAsync(Guid id) =>
        await _flightsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Flight newflight) =>
        await _flightsCollection.InsertOneAsync(newflight);

    public async Task UpdateAsync(Guid id, Flight updatedflight) =>
        await _flightsCollection.ReplaceOneAsync(x => x.Id == id, updatedflight);

    public async Task RemoveAsync(Guid id) =>
        await _flightsCollection.DeleteOneAsync(x => x.Id == id);
}

