using AnyCompany.PlacingOrders;
using Xunit;

namespace AnyCompany.Tests
{
    public class VAT_Application_Tests
    {
        [Theory]
        [InlineData("uk", 0.2)]
        [InlineData("UK", 0.2)]
        [InlineData("Uk", 0.2)]
        [InlineData("uK", 0.2)]
        [InlineData("", 0)]
        [InlineData("US", 0)]
        public void Apply_Correct_Vat(string country, double expectedVat)
        {
            //Arrange
            var sut = new VatApplicator();
            var order = new Order();
            //Act
            sut.ApplyVat(order, new Customer() { Country = country });
            //Assert
            Assert.Equal(expectedVat, order.VAT);
        }
    }
}
