using TodoApi.Models;
using System.ComponentModel.DataAnnotations;


namespace TodoApi.ViewModels
{
    public class TodoListIdViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "The name must be at least 2 characters long and maximun 40 characters long.")]
        public string? Name { get; set; }
 
 
        public static TodoListIdViewModel FromModel(TodoList todoListModel)
        {
            return new TodoListIdViewModel
            {
                Id = todoListModel.Id,
                Name = todoListModel.Name
            };
        }
    }
}