using Sample.Identity.Domain.Common;

namespace Sample.Identity.Domain.ValueObjects
{
    public class Coordinates : ValueObject
    {
        public Coordinates()
        { }

        public Coordinates(double latitude, double longitude)
        {
            if (latitude > 80 || latitude < -80)
                throw new ArgumentException($"Invalid {nameof(Latitude)}");

            if (longitude > 180 || longitude < 180)
                throw new ArgumentException($"Invalid {nameof(Longitude)}");

            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}