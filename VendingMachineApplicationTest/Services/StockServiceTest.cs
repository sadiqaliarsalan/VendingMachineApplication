using NUnit.Framework;
using Moq;
using VendingMachineApplication.Services;
using VendingMachineApplication.Interfaces;

namespace VendingMachineApplicationTest.Services
{
    [TestFixture]
    public class StockServiceTest
    {
        private Mock<IVendingMachine> _mockVendingMachine;
        private StockService _stockService;

        [SetUp]
        public void Setup()
        {
            _mockVendingMachine = new Mock<IVendingMachine>();
            _stockService = new StockService(_mockVendingMachine.Object);
        }

        [Test]
        public void RefillStock_WithValidProductAndQuantity_ShouldIncreaseStock_Test()
        {
            // arrange
            int productId = 101;
            int quantity = 20;
            _mockVendingMachine.Setup(m => m.ProductExists(productId)).Returns(true);

            // act
            _stockService.RefillStock(productId, quantity);

            // assert
            _mockVendingMachine.Verify(m => m.IncreaseStock(productId, quantity), Times.Once);
        }

        [Test]
        public void RefillStock_WithNonexistentProduct_ShouldNotIncreaseStock_Test()
        {
            // arrange
            int productId = 999;
            int quantity = 20;
            _mockVendingMachine.Setup(m => m.ProductExists(productId)).Returns(false);

            // act
            _stockService.RefillStock(productId, quantity);

            // assert
            _mockVendingMachine.Verify(m => m.IncreaseStock(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void RefillStock_WithNegativeQuantity_ShouldNotIncreaseStock_Test()
        {
            // arrange
            int productId = 101;
            int quantity = -10;
            _mockVendingMachine.Setup(m => m.ProductExists(productId)).Returns(true);

            // act
            _stockService.RefillStock(productId, quantity);

            // assert
            _mockVendingMachine.Verify(m => m.IncreaseStock(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
        }
    }
}
