using TodoApi.Models;


namespace TodoApi.ViewModels
{
  public class ItemViewModel
  {
    public int Id { get; set; }
    public string? Content { get; set; }
    public Boolean IsComplete { get; set; }
    public int TodoListId { get; set; }


    public static ItemViewModel FromModel(Item itemModel)
    {
      return new ItemViewModel
      {
        Id = itemModel.Id,
        Content = itemModel.Content,
        IsComplete = itemModel.IsComplete,
        TodoListId = itemModel.TodoListId,
      };
    }
  }
}