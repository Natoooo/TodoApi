using TodoApi.Models;


namespace TodoApi.ViewModels
{
  public class TokenViewModel
  {
    public int Id { get; set; }
    public int UserId { get; set; }


    public static TokenViewModel FromModel(Token tokenModel)
    {
      return new TokenViewModel
      {
        Id = tokenModel.Id,
        UserId = tokenModel.UserId
      };
    }
  }
}