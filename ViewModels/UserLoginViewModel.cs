using TodoApi.Models;
using System.ComponentModel.DataAnnotations;


namespace TodoApi.ViewModels
{
  public class UserLoginViewModel
  {
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "The password must be at least 6 characters long.")]
    public string? Password { get; set; }


    public static UserLoginViewModel FromModel(User userModel)
    {
      return new UserLoginViewModel
      {
        Email = userModel.Email,
        Password = userModel.PasswordHash
      };
    }
  }
}