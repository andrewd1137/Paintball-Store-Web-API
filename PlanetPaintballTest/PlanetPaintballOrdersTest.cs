using PPModel;
using Xunit;

namespace PlanetPaintballTest
{

    public class OrdersTest
    {

        [Fact]
        public void OrdersShouldSetValidOrderID()
        {

            //Arrange
            Orders order = new Orders();
            int validID = 1;

            //Act
            order.OrderID = validID;

            //Assert
            Assert.NotNull(order.OrderID);
            Assert.Equal(validID, order.OrderID);

        }

        [Fact]
        public void OrdersShouldSetValidCustomerID()
        {

            //Arrange
            Orders order = new Orders();
            int validID = 1;

            //Act
            order.CustomerID = validID;

            //Assert
            Assert.NotNull(order.CustomerID);
            Assert.Equal(validID, order.CustomerID);

        }

        [Fact]
        public void OrdersShouldSetValidStoreID()
        {

            //Arrange
            Orders order = new Orders();
            int validID = 1;

            //Act
            order.StoreID = validID;

            //Assert
            Assert.NotNull(order.StoreID);
            Assert.Equal(validID, order.StoreID);

        }

        [Fact]
        public void OrdersShouldSetValid()
        {

            //Arrange
            Orders order = new Orders();
            decimal validTotalCost = 175.50m;

            //Act
            order.orderTotalCost = validTotalCost;

            //Assert
            Assert.NotNull(order.orderTotalCost);
            Assert.Equal(validTotalCost, order.orderTotalCost);

        }

    }

}