using TodoApi.Models;
using System.ComponentModel.DataAnnotations;


namespace TodoApi.ViewModels
{
  public class UserRegistrationViewModel
  {
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "The password must be at least 6 characters long.")]
    public string? Password { get; set; }
    [Required]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    [Display(Name = "Confirm Password")]
    public string? ConfirmPassword { get; set; }


    public static UserRegistrationViewModel FromModel(User userModel)
    {
      return new UserRegistrationViewModel
      {
        Email = userModel.Email,
        Password = userModel.PasswordHash,
        ConfirmPassword = userModel.PasswordHash
      };
    }
  }
}