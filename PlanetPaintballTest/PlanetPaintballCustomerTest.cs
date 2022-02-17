using PPModel;
using Xunit;

namespace PlanetPaintballTest
{
   
    public class CustomerTest
    {
        
        // [Fact] is a data annotation in C# 
        // and will tell ther compiler that this method is a unit test
        [Fact]
        public void CustomerShouldSetValidID()
        {

            //Arrange
            Customer cust = new Customer();
            int validID = 1;

            //Act
            cust.ID = validID;

            //Assert
            Assert.NotNull(cust.ID);
            Assert.Equal(validID, cust.ID);

        }
        
        [Fact]
        public void CustomerShouldSetValidName()
        {

            //Arrange
            Customer cust = new Customer();
            string validName = "Bob";

            //Act
            cust.Name = validName;

            //Assert
            Assert.NotNull(cust.Name);
            Assert.Equal(validName, cust.Name);

        }


        [Fact]
        public void CustomerShouldSetValidAddress()
        {

            //Arrange
            Customer cust = new Customer();
            string validAddress = "21 Example Street";

            //Act
            cust.Address = validAddress;

            //Assert
            Assert.NotNull(cust.Address);
            Assert.Equal(validAddress, cust.Address);

        }

        [Fact]
        public void CustomerShouldSetValidEmail()
        {

            //Arrange
            Customer cust = new Customer();
            string validEmail = "email@example.com";

            //Act
            cust.Email = validEmail;

            //Assert
            Assert.NotNull(cust.Email);
            Assert.Equal(validEmail, cust.Email);

        }

    }

}
