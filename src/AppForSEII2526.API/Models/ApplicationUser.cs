using Microsoft.AspNetCore.Identity;

namespace AppForSEII2526.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class

public class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
    }
    public ApplicationUser(string id, string nombre, string apellido1, string apellido2, string userName)
    {
        Id = id;
        Nombre = nombre;
        Apellido1 = apellido1;
        Apellido2 = apellido2;
        UserName = userName;
        Email = userName;
    }

    [Required]
    [Display(Name = "Nombre")]
    public string Nombre
    {
        get;
        set;
    }

    [Required]
    [Display(Name = "Apellido1")]
    public string Apellido1
    {
        get;
        set;
    }
    [Display(Name = "Apellido2")]
    public string? Apellido2
    {
        get;
        set;
    }
}