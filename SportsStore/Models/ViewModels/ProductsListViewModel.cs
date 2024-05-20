namespace SportsStore.Models.ViewModels
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; } = null!;

        public PagingInfo PagingInfo { get; set; } = null!;

        public string? CurrentCategory { get; set; }
    }
}
