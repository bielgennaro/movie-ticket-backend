using MovieTicketApi.Validation;

namespace MovieTicketApi.Models.Entities;

public sealed class Session
{
    public Session()
    {
    }

    public Session( string room, double price, int movieId, int availableTickets )
    {
        this.Room = room;
        this.Price = price;
        this.MovieId = movieId;
        this.AvailableTickets = availableTickets;
    }

    public Session( DateTime dateTime, string room, double price, int availableTickets, int movieId )
    {
        this.DateTime = dateTime;
        this.ValidateDomain( room, availableTickets, price, movieId );
    }

    public int Id { get; private set; }

    public string Room { get; set; }

    public int AvailableTickets { get; set; }

    public double Price { get; set; }

    public DateTime DateTime { get; set; } = DateTime.UtcNow;

    public int MovieId { get; set; }

    public Movie Movie { get; private set; }

    private void ValidateDomain( string room, int availableTickets, double price, int movieId )
    {
        EntityValidationException.When( string.IsNullOrEmpty( room ), "Invalid room. Room is required" );

        EntityValidationException.When( room.Length < 3, "Invalid room, too short, minimum 3 characters" );

        EntityValidationException.When( room.Length > 255, "Invalid room, too long, maximum 255 characters" );

        EntityValidationException.When( availableTickets < 0, "Invalid available tickets. Available tickets must be greater than 0" );

        EntityValidationException.When( price < 0.0, "Invalid price. Price must be greater than 0" );

        EntityValidationException.When( movieId < 0, "Invalid movie id. Movie id must be greater than 0" );

        this.Room = room;
        this.AvailableTickets = availableTickets;
        this.Price = price;
        this.MovieId = movieId;
    }
}
