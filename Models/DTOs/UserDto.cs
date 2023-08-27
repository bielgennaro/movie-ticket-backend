namespace MovieTicketApi.Models.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; }
    public bool IsAdmin { get; set; }
}