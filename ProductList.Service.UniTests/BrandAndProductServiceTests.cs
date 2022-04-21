using ProtoType.Model;
using ProtoType.Model.DatabaseEntity;
using ProtoType.Repository.Interfaces;
using ProtoType.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Illustration.Service.UnitTests;

namespace ProtoType.Service.UnitTests
{
    [TestFixture]
    public class BrandAndProductServiceTests
    {
        private Mock<IBrandAndProductRepository> _brandAndProductRepository;
        private BrandAndProductService _brandAndProductService;
        private Mock<IContextAccessorService> _contextAccessorService;                
        private Guid _mockedTenantId;
        private string _mockedInstanceId;
        private Mock<IProtoTypeContext> _iProtoTypeContext;        
        private Mock<IIdentity> _identity;

        [SetUp]
        public void Setup()
        {
            _brandAndProductRepository = new();
            _contextAccessorService = new();            
            _identity = new();

            _mockedInstanceId = "localhost";
            _mockedTenantId = Guid.NewGuid();            

            var options = new DbContextOptionsBuilder<IProtoTypeContext>().UseInMemoryDatabase(databaseName: "ProtoType").Options;
            _iProtoTypeContext = new(options);

            _brandAndProductService = new(                
                _brandAndProductRepository.Object,                 
                _iProtoTypeContext.Object                
                );            
        }

        [Test]
        public async Task AddBrandAndProductAsyncTest()
        {
            // Arrange
            List<Model.Models.BrandAndProduct> brandAndProducts = new()
            {
                new()
                {
                    Id = 0,
                    BrandId = 1,
                    BrandName = "Mock Brand Name",
                    CredentialId = 1,
                    ProductName = "Mock Product Name"
                }
            };
            _brandAndProductRepository.Setup(r => r.SaveBrandAndProductAsync(It.IsAny<BrandAndProduct>())).Returns(Task.FromResult(1));

            // Act
            var insertedIds = await _brandAndProductService.AddBrandAndProductAsync(brandAndProducts);

            // Assert
            Assert.IsNotNull(insertedIds);
        }

        [Test]
        public void DeleteBrandAndProductByIdTest()
        {
            // Arrange
            var getByIdOutput = new BrandAndProduct() { Id = 1 };
            _brandAndProductRepository.Setup(s => s.GetById(1)).Returns(getByIdOutput);
            _brandAndProductRepository.Setup(r => r.Remove(getByIdOutput)).Verifiable();

            // Act and verify if method is called or not.
            _brandAndProductService.DeleteBrandAndProductById(1);
        }

        [Test]
        public void DeleteBrandAndProductByIdInvalidOperationExceptionTest()
        {
            // Arrange
            var getByIdOutput = new BrandAndProduct() { Id = 1 };
            _brandAndProductRepository.Setup(s => s.GetById(0)).Returns((BrandAndProduct)null);

            // Act
            var result = _brandAndProductService.DeleteBrandAndProductById(1);

            //Assert
            Assert.IsTrue(result == null);
        }

        [Test]
        public async Task GetBrandAndProductTestAsync()
        {
            // Arrange
            List<BrandAndProduct> repositoryOutput = new()
            {
                new()
                {
                    Id = 1,
                    BrandId = 1,
                    BrandName = "Mock Brand Name",
                    CredentialId = 1,
                    ProductName = "Mock Product Name"
                }
            };

            _iProtoTypeContext.Object.BrandAndProducts = ServiceTestsHelper.GetQueryableMockDbSet(repositoryOutput);

            // Act
            var brandAndProductId = await _brandAndProductService.GetBrandAndProductsAsync();            
        }

        [Test]
        public async Task UpdateBrandAndProductTest()
        {
            //Arrange
            Model.Models.BrandAndProduct brandAndProductModel = new Model.Models.BrandAndProduct
            {
                Id = 1,
                BrandId = 1,
                BrandName = "Mock Brand Name",
                CredentialId = 1,
                ProductName = "Mock Product Name"
            };

            var brandAndProduct = new BrandAndProduct()
            {
                Id = 1,
                BrandId = 1,
                BrandName = "Mock Brand Name",
                CredentialId = 1,
                ProductName = "Mock Product Name"
            };
            _brandAndProductRepository.Setup(s => s.GetById(1)).Returns(brandAndProduct);

            //Act
            var result = await _brandAndProductService.UpdateBrandAndProductAsync(brandAndProduct.Id, brandAndProductModel);

            // Assert
            Assert.AreEqual(brandAndProduct.Id, result);
        }
    }
}
