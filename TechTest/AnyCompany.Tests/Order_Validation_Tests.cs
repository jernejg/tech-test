using AnyCompany.PlacingOrders;
using Xunit;

namespace AnyCompany.Tests
{
    public class Order_Validation_Tests
    {
        [Theory]
        [InlineData(-0.01, false)]
        [InlineData(-0.02, false)]
        [InlineData(0, false)]
        [InlineData(0.01, true)]
        [InlineData(0.02, true)]
        public void Order_Is_Not_Valid_If_Price_Is_Below_Lower_Boundary(double val, bool expectedResult)
        {
            //Arrange
            var sut = new OrderValidator();
            //Act
            var actualResult = sut.IsValid(new Order() {Amount = val});
            //Assert
            Assert.Equal(expectedResult, actualResult);
        }
    }
}