using StockManagement.Core.Entities;
using StockManagement.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Core.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<Producto> GetProductoByIdAsync(int id)
        {
            return await _productoRepository.GetProductoByIdAsync(id);
        }

        public async Task<IEnumerable<Producto>> GetAllProductosAsync()
        {
            return await _productoRepository.GetAllProductosAsync();
        }

        public async Task AddProductoAsync(Producto producto)
        {
            await _productoRepository.AddProductoAsync(producto);
        }

        public async Task UpdateProductoAsync(Producto producto)
        {
            await _productoRepository.UpdateProductoAsync(producto);
        }

        public async Task DeleteProductoAsync(int id)
        {
            await _productoRepository.DeleteProductoAsync(id);
        }

        public async Task<IEnumerable<Producto>> ObtenerProductosFiltrados(int presupuesto)
        {
            if (presupuesto < 1 || presupuesto > 1000000)
            {
                throw new ArgumentOutOfRangeException(nameof(presupuesto), "El presupuesto debe estar entre 1 y 1,000,000.");
            }

            var productos = await _productoRepository.GetAllProductosAsync();

            var productosFiltrados = productos
                .GroupBy(p => p.Categoria)
                .Select(g => g.OrderByDescending(p => p.Precio).FirstOrDefault())
                .ToList();

            if (productosFiltrados.Count < 2)
            {
                return new List<Producto>(); 
            }

            Producto producto1 = null;
            Producto producto2 = null;
            decimal menorDiferencia = decimal.MaxValue;

            for (int i = 0; i < productosFiltrados.Count; i++)
            {
                for (int j = i + 1; j < productosFiltrados.Count; j++)
                {
                    var p1 = productosFiltrados[i];
                    var p2 = productosFiltrados[j];

                    if (p1.Categoria != p2.Categoria)
                    {
                        var totalPrecio = p1.Precio + p2.Precio;

                        if (totalPrecio <= presupuesto)
                        {
                            var diferencia = presupuesto - totalPrecio;
                            if (diferencia < menorDiferencia)
                            {
                                producto1 = p1;
                                producto2 = p2;
                                menorDiferencia = diferencia;
                            }
                        }
                    }
                }
            }

            if (producto1 != null && producto2 != null)
            {
                return new List<Producto> { producto1, producto2 };
            }

            return new List<Producto>();
        }




    }
}
