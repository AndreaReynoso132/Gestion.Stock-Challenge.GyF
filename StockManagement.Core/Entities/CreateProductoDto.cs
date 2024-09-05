namespace StockManagement.WebAPI.DTOs
{
    public class CreateProductoDto
    {
        public decimal Precio { get; set; }
        public DateTime FechaCarga { get; set; }
        public string Categoria { get; set; } = string.Empty;
    }
}
