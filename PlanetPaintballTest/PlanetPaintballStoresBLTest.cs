using System.Collections.Generic;
using Moq;
using PPBL;
using PPDL;
using PPModel;
using Xunit;

namespace PlanetPaintballTest
{

    public class PlanetPaintballStoresBLTest
    {

        [Fact]
        public void ShouldGetAllStores()
        {

            int testStoreID = 1;
            string testStoreName = "Planet Paintball";

            StoreFront store = new StoreFront()
            {
                ID = testStoreID,
                Name = testStoreName
            };

            List<StoreFront> expectedListOfStores = new List<StoreFront>();
            expectedListOfStores.Add(store);

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.GetStoreFronts()).Returns(expectedListOfStores);            

            IPlanetPaintballStoresBL planetPaintballStoresBL = new PlanetPaintballStoresBL(mockRepo.Object);

            List<StoreFront> actualListOfStores = planetPaintballStoresBL.ViewAllStores();

            Assert.Same(expectedListOfStores, actualListOfStores);
            Assert.Equal(testStoreID, actualListOfStores[0].ID);
            Assert.Equal(testStoreName, actualListOfStores[0].Name);

        }


        [Fact]
        public void ShouldGetAllManagers()
        {
            
            int testManagerId = 1;
            string testManagerName = "John John";

            Manager manager = new Manager()
            {
                ID = testManagerId,
                Name = testManagerName
            };

            List<Manager> expectedListOfManagers = new List<Manager>();
            expectedListOfManagers.Add(manager);

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.GetAllManagers()).Returns(expectedListOfManagers);            

            IPlanetPaintballStoresBL planetPaintballStoresBL = new PlanetPaintballStoresBL(mockRepo.Object);

            List<Manager> actualListOfManagers = planetPaintballStoresBL.GetManagers();

            Assert.Same(expectedListOfManagers, actualListOfManagers);
            Assert.Equal(testManagerId, actualListOfManagers[0].ID);
            Assert.Equal(testManagerName, actualListOfManagers[0].Name);

        }

        [Fact]
        public void ShouldGetAllStoreProducts()
        {
            
            int testStoreID = 1;
            string testStoreAddress = "21 Paint St";

            int testProductID = 1;
            string testProductName = "Book";

            StoreFront store = new StoreFront()
            {
                ID = testStoreID,
                Address = testStoreAddress
            };

            Products product = new Products()
            {
                ID = testProductID,
                Name = testProductName
            };

            List<Products> expectedListOfStoreProducts = new List<Products>();
            expectedListOfStoreProducts.Add(product);

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.GetProductsByStoreAddress(testStoreAddress)).Returns(expectedListOfStoreProducts);            

            IPlanetPaintballStoresBL planetPaintballStoresBL = new PlanetPaintballStoresBL(mockRepo.Object);

            List<Products> actualListOfProducts = planetPaintballStoresBL.GetProductsByStoreAddress(testStoreAddress);

            Assert.Same(expectedListOfStoreProducts, actualListOfProducts);
            Assert.Equal(testProductID, actualListOfProducts[0].ID);
            Assert.Equal(testProductName, actualListOfProducts[0].Name);

        }

        [Fact]
        public void ShouldGetAllLineItems()
        {
            
            int testOrderId = 1;
            int testProductID = 1;
            int testProductQuantity = 2;

            LineItems lineItem = new LineItems()
            {
                OrderID = testOrderId,
                ProductID = testProductID,
                ProductQuantity = testProductQuantity
            };


            List<LineItems> expectedListOfLineItems = new List<LineItems>();
            expectedListOfLineItems.Add(lineItem);

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.GetAllLineItems()).Returns(expectedListOfLineItems);            

            IPlanetPaintballStoresBL planetPaintballStoresBL = new PlanetPaintballStoresBL(mockRepo.Object);

            List<LineItems> actualListOfLineItems = planetPaintballStoresBL.GetLineItems();

            Assert.Same(expectedListOfLineItems, actualListOfLineItems);
            Assert.Equal(testOrderId, actualListOfLineItems[0].OrderID);
            Assert.Equal(testProductID, actualListOfLineItems[0].ProductID);
            Assert.Equal(testProductQuantity, actualListOfLineItems[0].ProductQuantity);

        }

        [Fact]
        public void ShouldUpdateStoreInventory()
        {
            int testStoreId = 1;
            int testProductID = 1;
            int testProductQuantity = 2;

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.UpdateInventory(testStoreId, testProductID, testProductQuantity));            

            IPlanetPaintballStoresBL planetPaintballStoresBL = new PlanetPaintballStoresBL(mockRepo.Object);

        }


    }

}