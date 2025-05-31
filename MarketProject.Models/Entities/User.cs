using Microsoft.AspNetCore.Identity;

namespace MarketProject.Models.Entities;

public sealed class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}