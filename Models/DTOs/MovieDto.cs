using System.ComponentModel.DataAnnotations;

namespace MovieTicketApi.Models.Dto;

public class MovieDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O gênero do filme é obrigatório.")]
    public string Genre { get; set; }

    [StringLength(255, ErrorMessage = "A sinopse deve conter no máximo 255 caracteres.")]
    public string Synopsis { get; set; }

    [Required(ErrorMessage = "O diretor do filme é obrigatório.")]
    public string Director { get; set; }

    [Required(ErrorMessage = "A URL do banner é obrigatória.")]
    public string BannerUrl { get; set; }
}