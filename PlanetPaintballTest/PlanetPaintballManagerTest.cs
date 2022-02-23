using PPModel;
using Xunit;

namespace PlanetPaintballTest
{

    public class ManagerTest
    {

        [Fact]
        public void ManagerShouldSetValidManagerID()
        {

            //Arrange
            Manager manager = new Manager();
            int validID = 1;

            //Act
            manager.ID = validID;

            //Assert
            Assert.NotNull(manager.ID);
            Assert.Equal(validID, manager.ID);

        }

        [Fact]
        public void ManagerShouldSetValidName()
        {

            //Arrange
            Manager manager = new Manager();
            string validName = "John John";

            //Act
            manager.Name = validName;

            //Assert
            Assert.NotNull(manager.Name);
            Assert.Equal(validName, manager.Name);

        }

        [Fact]
        public void ManagerShouldSetValidStoreID()
        {

            //Arrange
            Manager manager = new Manager();
            int validID = 1;

            //Act
            manager.storeID = validID;

            //Assert
            Assert.NotNull(manager.storeID);
            Assert.Equal(validID, manager.storeID);

        }

        [Fact]
        public void ManagerShouldSetValidAddress()
        {

            //Arrange
            Manager manager = new Manager();
            string validAddress = "23 St";

            //Act
            manager.Address = validAddress;

            //Assert
            Assert.NotNull(manager.Address);
            Assert.Equal(validAddress, manager.Address);

        }

        [Fact]
        public void ManagerShouldSetValidEmail()
        {

            //Arrange
            Manager manager = new Manager();
            string validEmail = "john@email.com";

            //Act
            manager.Email = validEmail;

            //Assert
            Assert.NotNull(manager.Email);
            Assert.Equal(validEmail, manager.Email);

        }

        [Fact]
        public void ManagerShouldSetValidPassword()
        {

            //Arrange
            Manager manager = new Manager();
            string validPassword = "13fajk";

            //Act
            manager.Password = validPassword;

            //Assert
            Assert.NotNull(manager.Password);
            Assert.Equal(validPassword, manager.Password);

        }

    }

}