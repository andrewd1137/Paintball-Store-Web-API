using System.Collections.Generic;
using Moq;
using PPBL;
using PPDL;
using PPModel;
using Xunit;

namespace PlanetPaintballTest
{

    public class PlanetPaintballBLTest
    {

        [Fact]
        public void ShouldGetAllCustomers()
        {

            string customerName = "Andrew DeMarco";
            string customerEmail = "andrew@email.com";

            Customer customer = new Customer()
            {
                Name = customerName,
                Email = customerEmail
            };

            List<Customer> expectedListOfCustomers = new List<Customer>();
            expectedListOfCustomers.Add(customer);

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.GetAllCustomers()).Returns(expectedListOfCustomers);            

            IPlanetPaintballBL planetPaintballBL = new PlanetPaintballBL(mockRepo.Object);

            List<Customer> actualListOfCustomers = planetPaintballBL.GetCustomers();

            Assert.Same(expectedListOfCustomers, actualListOfCustomers);
            Assert.Equal(customerName, actualListOfCustomers[0].Name);
            Assert.Equal(customerEmail, actualListOfCustomers[0].Email);

        }

        // [Fact]
        // public void ShouldThrowEmailException()
        // {

        //     string customerName = "Andrew DeMarco";
        //     string customerEmail = "andrew@email.com";
        //     string customerAddress = "123 St";
        //     string customerPassword = "1234";

        //     Customer customer = new Customer()
        //     {
        //         Name = customerName,
        //         Email = customerEmail,
        //         Address = customerAddress,
        //         Password = customerPassword
        //     };

        //     //should throw exception here since email should be unique
        //     Customer customer2 = new Customer()
        //     {
        //         Name = customerName,
        //         Email = customerEmail,
        //         Address = customerAddress,
        //         Password = customerPassword
        //     };

        //     Mock<IRepository> mockRepo = new Mock<IRepository>();

        //     mockRepo.Setup(repo => repo.AddCustomer(customer)).Verifiable();            

        //     IPlanetPaintballBL planetPaintballBL = new PlanetPaintballBL(mockRepo.Object);

        // }

    }

}