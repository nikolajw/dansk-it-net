using System.Threading;
using System.Threading.Tasks;
using Booking.Booking;
using MediatR;

namespace Booking.Availability
{
    public class BookingRequestEventHandler : INotificationHandler<BookingRequestedEvent>
    {
        private readonly IBookingWriter writer;
        private readonly IMediator mediator;

        public BookingRequestEventHandler(IBookingWriter writer, IMediator mediator)
        {
            this.writer = writer;
            this.mediator = mediator;
        }

        public async Task Handle(BookingRequestedEvent bookingRequest, CancellationToken cancellationToken)
        {
            if (!writer.TryCreate(bookingRequest.Request, bookingRequest.BookingRequestId))
            {
                await mediator.Publish(new BookingRequestNotPossible(bookingRequest.BookingRequestId), cancellationToken);
            }

            await mediator.Publish(new BookingCreatedEvent(bookingRequest.BookingRequestId), cancellationToken);
        }
    }
}