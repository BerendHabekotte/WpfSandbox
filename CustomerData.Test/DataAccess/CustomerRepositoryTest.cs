using CustomerData.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CustomerData.Test
{
    [TestClass]
    public class CustomerRepositoryTest
    {
        [TestMethod]
        public void ConstructorLoadsData()
        {
            // Arrange
            var path = "Data/customers.xml";

            // Act
            var target = new CustomerRepository(path);

            // Assert
            Assert.IsTrue(target.GetCustomers().Count() > 0);
        }
    }
}
