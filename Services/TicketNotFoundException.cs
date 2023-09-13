using System.Runtime.Serialization;

namespace MovieTicketApi.Services
{
    [Serializable]
    public class TicketNotFoundException : Exception
    {
        public TicketNotFoundException()
        {
        }

        public TicketNotFoundException( string? message ) : base( message )
        {
        }

        public TicketNotFoundException( string? message, Exception? innerException ) : base( message, innerException )
        {
        }

        protected TicketNotFoundException( SerializationInfo info, StreamingContext context ) : base( info, context )
        {
        }
    }
}