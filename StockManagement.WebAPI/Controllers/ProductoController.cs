using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using StockManagement.Core.Entities;
using StockManagement.Core.Interfaces;
using StockManagement.WebAPI.DTOs;

namespace StockManagement.WebAPI.Controllers
{
    /// <summary>
    /// Controlador para gestionar las operaciones relacionadas con los productos.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoService _productoService;

        /// <summary>
        /// Constructor del controlador de productos.
        /// </summary>
        /// <param name="productoService">Servicio de productos inyectado.</param>
        public ProductoController(IProductoService productoService)
        {
            _productoService = productoService;
        }

        /// <summary>
        /// Obtiene un producto específico por ID.
        /// </summary>
        /// <param name="id">ID del producto a obtener.</param>
        /// <returns>Detalles del producto si se encuentra; de lo contrario, NotFound.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _productoService.GetProductoByIdAsync(id);
            if (producto == null)
                return NotFound();
            return Ok(producto);
        }

        /// <summary>
        /// Obtiene todos los productos.
        /// </summary>
        /// <returns>Lista de productos.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProductos()
        {
            var productos = await _productoService.GetAllProductosAsync();
            return Ok(productos);
        }

        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="createProductoDto">Datos del producto a crear.</param>
        /// <returns>El producto creado.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProducto([FromBody] CreateProductoDto createProductoDto)
        {
            if (createProductoDto == null)
                return BadRequest("Los datos del producto no pueden ser nulos.");

            var producto = new Producto
            {
                Precio = createProductoDto.Precio,
                FechaCarga = createProductoDto.FechaCarga,
                Categoria = createProductoDto.Categoria
            };

            await _productoService.AddProductoAsync(producto);
            return CreatedAtAction(nameof(GetProducto), new { id = producto.Id }, producto);
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id">ID del producto a actualizar.</param>
        /// <param name="producto">Datos del producto actualizados.</param>
        /// <returns>Respuesta NoContent si la actualización es exitosa.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] Producto producto)
        {
            if (id != producto.Id)
                return BadRequest("El ID del producto no coincide con el ID proporcionado.");

            await _productoService.UpdateProductoAsync(producto);

            return Ok(new { message = "Producto actualizado con éxito", producto });
        }
        /// <summary>
        /// Elimina un producto existente.
        /// </summary>
        /// <param name="id">ID del producto a eliminar.</param>
        /// <returns>Respuesta NoContent si la eliminación es exitosa.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _productoService.GetProductoByIdAsync(id);
            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado." });
            }

            await _productoService.DeleteProductoAsync(id);
            return Ok(new { message = "Producto eliminado con éxito." });
        }

        /// <summary>
        /// Obtiene productos filtrados en base al presupuesto proporcionado.
        /// </summary>
        /// <param name="presupuesto">Presupuesto máximo para la combinación de productos.</param>
        /// <returns>Lista de productos filtrados según el presupuesto.</returns>
        [HttpGet("filtrar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerProductosFiltrados(int presupuesto)
        {
            var productos = await _productoService.ObtenerProductosFiltrados(presupuesto);
            if (productos == null || !productos.Any())
                return NotFound("No hay productos que cumplan con los criterios.");

            return Ok(productos);
        }
    }
}
