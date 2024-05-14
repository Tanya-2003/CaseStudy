using Car_Rental_System.Dao;
using Car_Rental_System.Models;
using Car_Rental_System.ExceptionHandlers;
using NUnit.Framework;

namespace NunitTestFile
{
    [TestFixture]
    public class ExceptionTest
    {
        
        CrsContext crsContext = new();
        CarLeaseRepositoryImpl repository;
        public ExceptionTest()
        {
            repository = new CarLeaseRepositoryImpl(crsContext);
        }

        [Test]
            public void CustomerNotFound_ThrowsException()
            {
                // ACT & ASSERT
                Assert.Throws<CustomerNotFoundE>(() => repository.FindCustomerById(999));
            }

            [Test]
            public void VehicleNotFound_ThrowsException()
            {
                // ACT & ASSERT
                Assert.Throws<VehicleNotFoundE>(() => repository.FindVehicleById(999));
            }

            [Test]
            public void LeaseNotFound_ThrowsException()
            {
                // ACT & ASSERT
                Assert.Throws<LeaseNotFoundE>(() => repository.FindLeaseById(999));
        }
    }
}


