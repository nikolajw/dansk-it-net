namespace Booking
{
    public class Event<T>
    {
        public T Value  { get; set; }
        public string CorrelationId { get; set; }
    }

    public class BookingCreatedEvent : Event<BookingModel>
    {
        public BookingCreatedEvent(BookingModel value, string correlationId)
        {
            CorrelationId = correlationId;
            Value = value;
        }
    }

    public class BookingUpdatedEvent : Event<BookingModel>
    {
        public BookingUpdatedEvent(BookingModel value, string correlationId)
        {
            CorrelationId = correlationId;
            Value = value;
        }
    }

    public class BookingCancelledEvent : Event<int>
    {
        public BookingCancelledEvent(int value, string correlationId)
        {
            CorrelationId = correlationId;
            Value = value;
        }
    }
}


