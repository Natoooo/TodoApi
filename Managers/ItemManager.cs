using TodoApi.Models;


namespace TodoApi.Managers
{
    public class ItemManager
    {
        private readonly ApiDbContext _context;

        public ItemManager(ApiDbContext context)
        {
            _context = context;
        }

        public void InitContext()
        {
            if (_context.Item.Count() == 0)
            {
                _context.Item.Add(new Item { Content = "", IsComplete = true, TodoListId = 1});
                _context.Item.Add(new Item { Content = "", IsComplete = false, TodoListId = 1});
                _context.Item.Add(new Item { Content = "", IsComplete = false, TodoListId = 1 });
                _context.Item.Add(new Item { Content = "", IsComplete = true, TodoListId = 1 });

                _context.SaveChanges();
            }
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _context.Item.ToList();
        }

        public Item? GetItemById(int id)
        {
            var item = _context.Item.Find(id);

            if (item == null)
            return null;

            return item;
        }

        public Item? CreateItem(Item item)
        {
            _context.Item.Add(item);
            _context.SaveChanges();
            
            return item;
        }

        public Item? UpdateItem(int id, Item modifiedItem)
        {
            var item = _context.Item.Find(id);
            
            if (item == null)
                return null;
            
            item.Content = modifiedItem.Content;
            item.IsComplete = modifiedItem.IsComplete;
            
            _context.SaveChanges();
            
           return item;
        }

        public void DeleteItem(int id)
        {
            var item = _context.Item.Find(id);

            if (item == null)
                throw(new Exception("Item doesn't exist"));

            _context.Item.Remove(item);
            _context.SaveChanges();
        }
    }
}