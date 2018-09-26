using AnyCompany.Api;
using Moq;
using Xunit;

namespace AnyCompany.Tests
{
    public class Order_Placement_Tests
    {
        [Theory]
        [AutoMoqData()]
        public void Customer_Who_Places_The_Order_Needs_To_Be_Retrieved_From_The_Database(
            int customerId,
            Order o,
            Mock<IOrderRepository> orderRepoMock,
            Mock<ICustomerRepository> customerRepoMock,
            Mock<IOrderValidator> orderValidatorMock,
            Mock<IVatApplicator> vatAplicatorMock
            )
        {
            //Arrange
            var sut = new OrderService(orderRepoMock.Object, customerRepoMock.Object, orderValidatorMock.Object, vatAplicatorMock.Object);
            //Act
            sut.PlaceOrder(o, customerId);
            //Assert
            customerRepoMock.Verify(repository => repository.Load(customerId));
        }

        [Theory]
        [AutoMoqData()]
        public void Order_Placement_Should_Fail_If_The_Order_Is_Invalid(
            int customerId,
            Order o,
            Mock<IOrderRepository> orderRepoMock,
            Mock<ICustomerRepository> customerRepoMock,
            Mock<IOrderValidator> orderValidatorMock,
            Mock<IVatApplicator> vatAplicatorMock
        )
        {
            //Arrange
            orderValidatorMock.Setup(validator => validator.IsValid(o)).Returns(false);
            var sut = new OrderService(orderRepoMock.Object, customerRepoMock.Object, orderValidatorMock.Object, vatAplicatorMock.Object);
            //Act
            var actualOrderPlacewentSuccess = sut.PlaceOrder(o, customerId);
            //Assert
            Assert.False(actualOrderPlacewentSuccess, "Order is invalid, so the placement must fail immediately.");
        }

        [Theory]
        [AutoMoqData()]
        public void Apply_Vat_Step_Should_Be_Called_Exactly_Once_If_Order_Is_Valid(
            int customerId,
            Order o,
            Mock<IOrderRepository> orderRepoMock,
            Mock<ICustomerRepository> customerRepoMock,
            Mock<IOrderValidator> orderValidatorMock,
            Mock<IVatApplicator> vatAplicatorMock
        )
        {
            //Arrange
            orderValidatorMock.Setup(validator => validator.IsValid(o)).Returns(true);
            var sut = new OrderService(orderRepoMock.Object, customerRepoMock.Object, orderValidatorMock.Object, vatAplicatorMock.Object);
            //Act
            sut.PlaceOrder(o, customerId);
            //Assert
            vatAplicatorMock.Verify(repository => repository.ApplyVat(o, It.IsAny<Customer>()), Times.Exactly(1));
        }


        [Theory]
        [AutoMoqData()]
        public void Save_Order_Step_Should_Be_Called_Exactly_Once_If_Order_Is_Valid(
            int customerId,
            Order o,
            Mock<IOrderRepository> orderRepoMock,
            Mock<ICustomerRepository> customerRepoMock,
            Mock<IOrderValidator> orderValidatorMock,
            Mock<IVatApplicator> vatAplicatorMock
        )
        {
            //Arrange
            orderValidatorMock.Setup(validator => validator.IsValid(o)).Returns(true);
            var sut = new OrderService(orderRepoMock.Object, customerRepoMock.Object, orderValidatorMock.Object, vatAplicatorMock.Object);
            //Act
            sut.PlaceOrder(o, customerId);
            //Assert
            orderRepoMock.Verify(repository => repository.Save(o), Times.Exactly(1));
        }
    }
}