namespace SportsStore.Models
{
    public class Cart
    {
        private readonly List<CartLine> lines = new List<CartLine>();

        public IReadOnlyList<CartLine> Lines
        {
            get { return this.lines; }
        }

        public virtual void AddItem(Product product, int quantity)
        {
#pragma warning disable S6602 // "Find" method should be used instead of the "FirstOrDefault" extension
            CartLine? line = this.lines.FirstOrDefault(p => p.Product.ProductId == product.ProductId);
#pragma warning restore S6602 // "Find" method should be used instead of the "FirstOrDefault" extension

            if (line is null)
            {
                this.lines.Add(new CartLine
                {
                    Product = product,
                    Quantity = quantity,
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product)
            => this.lines.RemoveAll(l => l.Product.ProductId == product.ProductId);

        public virtual decimal ComputeTotalValue()
            => this.lines.Sum(e => e.Product.Price * e.Quantity);

        public virtual void Clear() => this.lines.Clear();
    }
}
