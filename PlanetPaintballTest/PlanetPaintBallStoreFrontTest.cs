using PPModel;
using Xunit;

namespace PlanetPaintballTest
{
   
    public class StoreFrontTest
    {
        /// <summary>
        /// 
        /// </summary>
        /// [Fact] is a data annotation in C# 
        /// and will tell ther compiler that this method is a unit test
        [Fact]
        public void StoreFrontShouldSetValidID()
        {

            //Arrange
            StoreFront store = new StoreFront();
            int validID = 1;

            //Act
            store.ID = validID;

            //Assert
            Assert.NotNull(store.ID);
            Assert.Equal(validID, store.ID);

        }
        
        

        [Fact]
        public void StoreFrontShouldSetValidName()
        {

            //Arrange
            StoreFront store = new StoreFront();
            string validName = "Planet Paintball";

            //Act
            store.Address = validName;

            //Assert
            Assert.NotNull(store.Name);
            Assert.Equal(validName, store.Name);

        }

        [Fact]
        public void StoreFrontShouldSetValidAddress()
        {

            //Arrange
            StoreFront store = new StoreFront();
            string validAddress = "21 Paint St";

            //Act
            store.Address = validAddress;

            //Assert
            Assert.NotNull(store.Address);
            Assert.Equal(validAddress, store.Address);

        }
        
    }

}
