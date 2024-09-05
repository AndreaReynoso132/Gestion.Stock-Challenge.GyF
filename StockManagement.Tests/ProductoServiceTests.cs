using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockManagement.Core.Entities;
using StockManagement.Core.Interfaces;
using StockManagement.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockManagement.Tests
{
    [TestClass] 
    public class ProductoServiceTests
    {
        private Mock<IProductoRepository> _productoRepositoryMock; 
        private ProductoService _productoService; 

        [TestInitialize]
        public void Setup()
        {
            
            _productoRepositoryMock = new Mock<IProductoRepository>();

           
            _productoService = new ProductoService(_productoRepositoryMock.Object);
        }

        [TestMethod]
        public async Task ObtenerProductosFiltrados_SimpleTest_RetornaProductosCorrectos()
        {
            var productos = new List<Producto>
            {
                new Producto { Id = 1, Precio = 50, FechaCarga = DateTime.Now, Categoria = "Pintureria" },
                new Producto { Id = 2, Precio = 60, FechaCarga = DateTime.Now, Categoria = "Industrial" }
            };

            _productoRepositoryMock.Setup(repo => repo.GetAllProductosAsync()).ReturnsAsync(productos);

            int presupuesto = 120; 

            var resultado = await _productoService.ObtenerProductosFiltrados(presupuesto);

            Assert.IsNotNull(resultado);
            Assert.AreEqual(2, resultado.Count()); 
            Assert.IsTrue(resultado.Any(p => p.Id == 1)); 
            Assert.IsTrue(resultado.Any(p => p.Id == 2)); 
        }
    }
}
