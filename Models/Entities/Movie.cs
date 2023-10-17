using System.ComponentModel.DataAnnotations;

using MovieTicketApi.Validation;

namespace MovieTicketApi.Models.Entities;

public sealed class Movie
{
    public Movie()
    {
    }

    public Movie( string title, string gender, string synopsis, string director, string bannerUrl )
    {
        this.ValidateDomain( title, gender, synopsis, director, bannerUrl );
    }

    public int Id { get; private set; }

    public string Title { get; set; }

    public string Gender { get; set; }

    public string Synopsis { get; set; }

    public string Director { get; set; }

    [Url]
    public string BannerUrl { get; set; }

    private void ValidateDomain( string title, string gender, string synopsis, string director, string bannerUrl )
    {
        EntityValidationException.When( string.IsNullOrEmpty( title ), "Invalid title. Title is required" );

        EntityValidationException.When( title.Length < 3, "Invalid title, too short, minimum 3 characters" );

        EntityValidationException.When( title.Length > 255, "Invalid title, too short, minimum 3 characters" );

        EntityValidationException.When( string.IsNullOrEmpty( gender ), "Invalid movie gender. Movie gender is required" );

        EntityValidationException.When( gender.Length < 3, "Invalid gender, too short, minimum 3 characters" );

        EntityValidationException.When( gender.Length > 255, "Invalid gender, too long, maximum 255 characters" );

        EntityValidationException.When( string.IsNullOrEmpty( synopsis ), "Invalid movie synopsis. Movie synopsis is required" );

        EntityValidationException.When( synopsis.Length < 3, "Invalid synopsis, too short, minimum 3 characters" );

        EntityValidationException.When( synopsis.Length > 255, "Invalid synopsis, too long, maximum 255 characters" );

        EntityValidationException.When( string.IsNullOrEmpty( director ), "Invalid movie director. Movie director is required" );

        EntityValidationException.When( director.Length < 3, "Invalid director, too short, minimum 3 characters" );

        EntityValidationException.When( director.Length > 255, "Invalid director, too long, maximum 255 characters" );

        EntityValidationException.When( string.IsNullOrEmpty( bannerUrl ), "Invalid movie banner url. Movie banner url is required" );

        EntityValidationException.When( bannerUrl.Length < 3, "Invalid banner url, too short, minimum 3 characters" );

        EntityValidationException.When( bannerUrl.Length > 255, "Invalid banner url, too long, maximum 255 characters" );

        this.Title = title;
        this.Gender = gender;
        this.Synopsis = synopsis;
        this.Director = director;
        this.BannerUrl = bannerUrl;
    }
}