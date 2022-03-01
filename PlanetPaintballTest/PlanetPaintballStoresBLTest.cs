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
        public void ShouldThrowVerifiedManagerException()
        {

            Manager manager = new Manager()
            {
                storeID = 1,
                Email = "joe@email.com",
                Password = "1234"
            };

            List<Manager> expectedListOfManagers = new List<Manager>();
            expectedListOfManagers.Add(manager);

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.GetAllManagers()).Returns(expectedListOfManagers);            

            IPlanetPaintballStoresBL planetPaintballStoresBL = new PlanetPaintballStoresBL(mockRepo.Object);

            bool isNotVerified = planetPaintballStoresBL.VerifyManager("bob@email.com", "4321", 2);
            bool isVerified = planetPaintballStoresBL.VerifyManager("joe@email.com", "1234", 1);

            Assert.Equal(false, isNotVerified);
            Assert.Equal(true, isVerified);

        }

        [Fact]
        public void ShouldMakeAnOrder()
        {

            int storeID = 1;
            decimal cost = 100.00M;

            Orders order = new Orders()
            {
                StoreID = storeID,
                orderTotalCost = cost
            };

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.MakeAnOrder(order)).Returns(order);            

            IPlanetPaintballStoresBL planetPaintballStoresBL = new PlanetPaintballStoresBL(mockRepo.Object);

            Orders order1 = planetPaintballStoresBL.MakeAnOrder(order);

            Assert.Same(order, order1);
            Assert.Equal(order.StoreID, order1.StoreID);
            Assert.Equal(order.orderTotalCost, order1.orderTotalCost);

        }

        [Fact]
        public void ShouldGetStoreProductsByAddress()
        {

            int productID = 1;
            string productName = "Pen";
            decimal productPrice = 100.00M;
            string productCategory = "Category";
            string productDescription = "Description";

            string storeAddress = "21 Paint St";

            Products product = new Products()
            {
                ID = productID,
                Name = productName,
                Price = productPrice,
                Category = productCategory,
                Description = productDescription
            };

            List<Products> expectedListOfProducts = new List<Products>();
            expectedListOfProducts.Add(product);


            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.GetProductsByStoreAddress(storeAddress)).Returns(expectedListOfProducts);            

            IPlanetPaintballStoresBL planetPaintballStoresBL = new PlanetPaintballStoresBL(mockRepo.Object);

            List<Products> actualListOfProducts = planetPaintballStoresBL.GetProductsByStoreAddress(storeAddress);

            Assert.Same(expectedListOfProducts, actualListOfProducts);

        }

    }

}