namespace Anatoli.ViewModels.StockModels
{
    public class UserViewModel : BaseViewModel
    {
        public string UserName { get; set; }

        public UserViewModel()
        {
            UserName = string.Empty;
        }
    }
}