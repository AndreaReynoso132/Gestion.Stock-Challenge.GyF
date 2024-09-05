namespace StockManagement.Core.Entities
{
    public class Producto
    {
        public int Id { get; set; }
        public decimal Precio { get; set; }
        public DateTime FechaCarga { get; set; }
        public string Categoria { get; set; }
    }
}
