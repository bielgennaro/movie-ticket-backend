namespace MovieTicketApi.Models
{
    public class Session
    {
        public Session()
        {
        }

        public Session(DateTime dateTime, string room)
        {
            DateTime = dateTime;
            Room = room;
        }

        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Room { get; set; }

        public int MovieId { get; set; }  // Chave estrangeira
        public Movie Movie { get; set; }  // Propriedade de navegação para o filme
    }
}

