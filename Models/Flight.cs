using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Aeroliner.Models
{

    public class Flight
    {
        private Guid _id;
        private string _flightNumber = "";
        private string _departureAirport = "";
        private string _destinationAirport = "";
        private DateTime _departureTime;
        private DateTime _arrivalTime;
        private decimal _price;

        [BsonId]
        public Guid Id { get => _id; set => _id = value; }

        public string FlightNumber { get => _flightNumber; set => _flightNumber = value; }

        public string DepartureAirport { get => _departureAirport; set => _departureAirport = value; }

        public string DestinationAirport { get => _destinationAirport; set => _destinationAirport = value; }

        public DateTime DepartureTime { get => _departureTime; set => _departureTime = value; }

        public DateTime ArrivalTime { get => _arrivalTime; set => _arrivalTime = value; }

        public decimal Price { get => _price; set => _price = value; }

        public Flight()
        {

        }

        public Flight(Guid? id = null)
        {
            Id = id ?? new Guid();
        }

        public Flight(Guid? id, string flightNumber, string departureAirport, string destinationAirport, DateTime departureTime, DateTime arrivalTime, decimal price)
        {
            Id = id ?? new Guid();
            FlightNumber = flightNumber;
            DepartureAirport = departureAirport;
            DestinationAirport = destinationAirport;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            Price = price;
        }

        /// <summary>
        /// Check if the flight is available at the given departure time
        /// </summary>
        /// <param name="departureTime"></param>
        /// <returns></returns>
        public bool IsAvailable(DateTime departureTime) =>
            departureTime >= DepartureTime && departureTime <= ArrivalTime;

    }
}
