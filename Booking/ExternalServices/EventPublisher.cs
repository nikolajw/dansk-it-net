using System.Diagnostics;
using Newtonsoft.Json;

namespace Booking.ExternalServices
{
    public class EventPublisherClient
    {
        public void Publish<T>(Event<T> evnt)
        {
            var json = JsonConvert.SerializeObject(evnt);
            Debug.WriteLine($"EventPublisherService Called: {json}");
        }
    }
}
