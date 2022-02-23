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

    }

}