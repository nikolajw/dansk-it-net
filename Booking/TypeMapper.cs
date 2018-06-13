using Booking.ExternalServices;

namespace Booking
{
    public static class TypeMapper
    {
        public static TypeRoom ToTypeRoom(this RoomType type)
        {
            if (type == RoomType.Double) return TypeRoom.Double;
            if (type == RoomType.Single) return TypeRoom.Single;
            if (type == RoomType.Suite) return TypeRoom.Suite;

            return TypeRoom.Unknown;
        }
    }
}
