using PPModel;
using Xunit;

namespace PlanetPaintballTest
{
   
    public class ProductsTest
    {
        
        // [Fact] is a data annotation in C# 
        // and will tell ther compiler that this method is a unit test
        [Fact]
        public void ProductShouldSetValidProductID()
        {

            //Arrange
            Products product = new Products();
            int validProductID = 1;

            //Act
            product.ID = validProductID;

            //Assert
            Assert.NotNull(product.ID);
            Assert.Equal(validProductID, product.ID);

        }
        
        [Fact]
        public void ProductShouldSetValidName()
        {

            //Arrange
            Products product = new Products();
            string validProductName = "Book";

            //Act
            product.Name = validProductName;

            //Assert
            Assert.NotNull(product.Name);
            Assert.Equal(validProductName, product.Name);


        }


        [Fact]
        public void ProductShouldSetValidPrice()
        {

            //Arrange
            Products product = new Products();
            decimal validProductPrice = 1.00M;

            //Act
            product.Price = validProductPrice;

            //Assert
            Assert.NotNull(product.Price);
            Assert.Equal(validProductPrice, product.Price);


        }

        [Fact]
        public void ProductShouldSetValidCategory()
        {

            //Arrange
            Products product = new Products();
            string validProductCategory = "Non-Fiction";

            //Act
            product.Category = validProductCategory;

            //Assert
            Assert.NotNull(product.Category);
            Assert.Equal(validProductCategory, product.Category);

        }

        [Fact]
        public void ProductShouldSetValidDescription()
        {

            //Arrange
            Products product = new Products();
            string validProductDescription = "A book on cats";

            //Act
            product.Description = validProductDescription;

            //Assert
            Assert.NotNull(product.Description);
            Assert.Equal(validProductDescription, product.Description);
            
        }

        [Fact]
        public void ProductShouldSetValidQuantity()
        {

            //Arrange
            Products product = new Products();
            int validProductQuantity = 10;

            //Act
            product.quantity = validProductQuantity;

            //Assert
            Assert.NotNull(product.quantity);
            Assert.Equal(validProductQuantity, product.quantity);
            
        }

    }

}
