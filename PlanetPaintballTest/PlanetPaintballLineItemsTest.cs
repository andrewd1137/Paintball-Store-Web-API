using PPModel;
using Xunit;

namespace PlanetPaintballTest
{
   
    public class LineItemsTest
    {
        
        // [Fact] is a data annotation in C# 
        // and will tell ther compiler that this method is a unit test
        [Fact]
        public void LineItemsShouldSetValidOrderID()
        {

            //Arrange
            LineItems lineItem = new LineItems();
            int validOrderId = 1;

            //Act
            lineItem.OrderID = validOrderId;

            //Assert
            Assert.NotNull(lineItem.OrderID);
            Assert.Equal(validOrderId, lineItem.OrderID);

        }
        
        [Fact]
        public void LineItemShouldSetValidProductID()
        {

            //Arrange
            LineItems lineItem = new LineItems();
            int validProductID= 1;

            //Act
            lineItem.ProductID = validProductID;

            //Assert
            Assert.NotNull(lineItem.ProductID);
            Assert.Equal(validProductID, lineItem.ProductID);

        }


        [Fact]
        public void LineItemSHouldSetValidProductQuantity()
        {

            //Arrange
            LineItems lineItem = new LineItems();
            int validProductQuantity= 1;

            //Act
            lineItem.ProductQuantity = validProductQuantity;

            //Assert
            Assert.NotNull(lineItem.ProductID);
            Assert.Equal(validProductQuantity, lineItem.ProductQuantity);

        }

    }

}
