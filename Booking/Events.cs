namespace Booking
{
    public class Event<T>
    {
        public T Value  { get; set; }
    }

    public class BookingCreatedEvent : Event<BookingModel>
    {
        public BookingCreatedEvent(BookingModel value)
        {
            Value = value;
        }
    }

    public class BookingUpdatedEvent : Event<BookingModel>
    {
        public BookingUpdatedEvent(BookingModel value)
        {
            Value = value;
        }
    }

    public class BookingCancelledEvent : Event<int>
    {
        public BookingCancelledEvent(int value)
        {
            Value = value;
        }
    }
}


