using StockManagement.Core.Entities;


namespace StockManagement.Core.Interfaces

{
    public interface IProductoService
    {
        Task<Producto> GetProductoByIdAsync(int id);
        Task<IEnumerable<Producto>> GetAllProductosAsync();
        Task AddProductoAsync(Producto producto);
        Task UpdateProductoAsync(Producto producto);
        Task DeleteProductoAsync(int id);
        Task<IEnumerable<Producto>> ObtenerProductosFiltrados(int presupuesto);
    }
}