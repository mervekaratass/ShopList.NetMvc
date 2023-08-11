using X.PagedList;

namespace ShoppingList.ViewModels
{
    public class UserListViewModel
    {
        public int UserID { get; set; }
        public List<ListViewModel> ListViewModel { get; set; }
     
    }
}
