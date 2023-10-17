namespace MovieTicketApi.Validation
{
    public class EntityValidationException : Exception
    {
        public EntityValidationException( string error ) : base( error ) { }

        public static void When( bool hasError, string error )
        {
            if( hasError )
            {
                throw new EntityValidationException( error );
            }
        }
    }
}
