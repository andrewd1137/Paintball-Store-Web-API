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

        [Fact]
        public void ShouldAddCustomer()
        {

            string customerName = "Andrew DeMarco";
            string customerEmail = "andrew@email.com";
            string customerAddress = "123 St";
            string customerPassword = "1234";

            Customer customer = new Customer()
            {
                Name = customerName,
                Email = customerEmail,
                Address = customerAddress,
                Password = customerPassword
            };

            List<Customer> expectedListOfCustomers = new List<Customer>();

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.AddCustomer(customer)).Returns(customer);            
            mockRepo.Setup(repo => repo.GetAllCustomers()).Returns(expectedListOfCustomers);

            IPlanetPaintballBL planetPaintballBL = new PlanetPaintballBL(mockRepo.Object);

            Customer customer1 = planetPaintballBL.AddCustomer(customer);

            Assert.Same(customer, customer1);
            Assert.Equal(customer.Name, customer1.Name);
            Assert.Equal(customer.Email, customer1.Email);
            Assert.Equal(customer.Address, customer1.Address);
            Assert.Equal(customer.Password, customer1.Password);
        }

        [Fact]
        public void ShouldThrowSearchCustomerException()
        {

            string customerName = "Andrew DeMarco";
            string customerEmail = "andrew@email.com";
            string customerAddress = "123 St";
            string customerPassword = "1234";

            Customer customer = new Customer()
            {
                Name = customerName,
                Email = customerEmail,
                Address = customerAddress,
                Password = customerPassword
            };

            List<Customer> expectedListOfCustomers = new List<Customer>();
            expectedListOfCustomers.Add(customer);

            Mock<IRepository> mockRepo = new Mock<IRepository>();
            
            mockRepo.Setup(repo => repo.GetAllCustomers()).Returns(expectedListOfCustomers);

            IPlanetPaintballBL planetPaintballBL = new PlanetPaintballBL(mockRepo.Object);

            Assert.Throws<System.Exception>( () => planetPaintballBL.SearchCustomer("John"));
            
        }  

        [Fact]
        public void ShouldThrowVerifidCustomerException()
        {

            string customerName = "Andrew DeMarco";
            string customerEmail = "andrew@email.com";
            string customerPassword = "1234";

            Customer customer = new Customer()
            {
                Name = customerName,
                Email = customerEmail,
                Password = customerPassword
            };

            List<Customer> expectedListOfCustomers = new List<Customer>();
            expectedListOfCustomers.Add(customer);

            Mock<IRepository> mockRepo = new Mock<IRepository>();

            mockRepo.Setup(repo => repo.GetAllCustomers()).Returns(expectedListOfCustomers);            

            IPlanetPaintballBL planetPaintballBL = new PlanetPaintballBL(mockRepo.Object);

            Assert.Throws<System.Exception>( () => planetPaintballBL.VerifyCustomer("john@email.com", "432432"));

        }      

    }

}