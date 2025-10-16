using Moq;
using Project.Domain.Entities.Product;
using Project.Services.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Project.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepo;
        private readonly Mock<IProductPriceRepository> _mockPriceRepo;
        private readonly Mock<IProductStockRepository> _mockStockRepo;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _mockProductRepo = new Mock<IProductRepository>();
            _mockPriceRepo = new Mock<IProductPriceRepository>();
            _mockStockRepo = new Mock<IProductStockRepository>();
            _service = new ProductService(_mockProductRepo.Object, _mockPriceRepo.Object, _mockStockRepo.Object);
        }

        [Fact]
        public void Add_ShouldCreateProductAndInitializePriceAndStock()
        {
           
            var product = _service.Add("Product1", "Description1", "Brand1");

            
            Assert.NotNull(product);
            Assert.Equal("Product1", product.Name);
            Assert.Equal("Description1", product.Description);
            Assert.Equal("Brand1", product.Brand);

            
            _mockProductRepo.Verify(r => r.Add(It.Is<Product>(p => p.Id == product.Id)), Times.Once);
            _mockPriceRepo.Verify(r => r.Add(It.Is<ProductPrice>(pp => pp.ProductId == product.Id && pp.CurrentPoints == 0)), Times.Once);
            _mockStockRepo.Verify(r => r.Add(It.Is<ProductStock>(ps => ps.ProductId == product.Id && ps.AvailableStock == 0)), Times.Once);
        }

        [Fact]
        public void UpdateDetails_ShouldUpdateProductWhenExists()
        {
            var productId = Guid.NewGuid();
            var existingProduct = new Product("OldName", "OldDesc", "OldBrand");

            _mockProductRepo.Setup(r => r.GetById(productId)).Returns(existingProduct);

            
            _service.UpdateDetails(productId, "NewName", "NewDesc", "NewBrand");

            
            Assert.Equal("NewName", existingProduct.Name);
            Assert.Equal("NewDesc", existingProduct.Description);
            Assert.Equal("NewBrand", existingProduct.Brand);

            _mockProductRepo.Verify(r => r.Update(existingProduct), Times.Once);
        }

        [Fact]
        public void UpdateDetails_ShouldNotThrowIfProductDoesNotExist()
        {
            _mockProductRepo.Setup(r => r.GetById(It.IsAny<Guid>())).Returns((Product?)null);

            var exception = Record.Exception(() => _service.UpdateDetails(Guid.NewGuid(), "Name", "Desc", "Brand"));

            Assert.Null(exception);
        }

        [Fact]
        public void Deactivate_ShouldCallDeactivateAndUpdate()
        {
            var productId = Guid.NewGuid();
            var product = new Product("Name", "Desc", "Brand");
            _mockProductRepo.Setup(r => r.GetById(productId)).Returns(product);

            _service.Deactivate(productId);

            Assert.False(product.IsActive); 
            _mockProductRepo.Verify(r => r.Update(product), Times.Once);
        }

        [Fact]
        public void Activate_ShouldCallActivateAndUpdate()
        {
            var productId = Guid.NewGuid();
            var product = new Product("Name", "Desc", "Brand");
            product.Deactivate();
            _mockProductRepo.Setup(r => r.GetById(productId)).Returns(product);

            _service.Activate(productId);

            Assert.True(product.IsActive);
            _mockProductRepo.Verify(r => r.Update(product), Times.Once);
        }
    }
}
