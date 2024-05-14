using Car_Rental_System.Dao;
using Car_Rental_System.Models;
using NUnit.Framework;
//change values each time or else the test cases are failing
namespace NunitTestFile
{
    [TestFixture]
    public class AddTest
    {
        CrsContext crsContext = new();
        CarLeaseRepositoryImpl repository;
        public AddTest()
        {
            repository = new CarLeaseRepositoryImpl(crsContext);
        }

        [Test]
        public void CreateVehicle_Successfully()
        {
            //ARRANGE
            Vehicle expectedCar = new Vehicle
            {
                VehicleId = 16,
                Make = "Jaguar",
                Model = "C",
                Year = 2024,
                DailyRate = 50,
                Status = "Available",
                PassengerCapacity = 5,
                EngineCapacity = 2
            };

            //ACT
            repository.AddVehicle(expectedCar);
            Vehicle actualCar = repository.FindVehicleById(expectedCar.VehicleId);

            //ASSERT
            Assert.NotNull(actualCar); 
            Assert.AreEqual(expectedCar.VehicleId, actualCar.VehicleId);
            Assert.AreEqual(expectedCar.Make, actualCar.Make);
            Assert.AreEqual(expectedCar.Model, actualCar.Model);
            Assert.AreEqual(expectedCar.Year, actualCar.Year);
            Assert.AreEqual(expectedCar.DailyRate, actualCar.DailyRate);
            Assert.AreEqual(expectedCar.Status, actualCar.Status);
            Assert.AreEqual(expectedCar.PassengerCapacity, actualCar.PassengerCapacity);
            Assert.AreEqual(expectedCar.EngineCapacity, actualCar.EngineCapacity);
        }

        [Test]
        public void CreateLease_Successfully()
        {
            // ARRANGE
            int leaseID = 16;
            int customerID = 13; 
            int vehicleID = 910; 
            DateTime startDate = DateTime.Now.Date; 
            DateTime endDate = DateTime.Now.Date.AddDays(20); 
            string leaseType = "Monthly";

            // ACT
            Lease createdLease = repository.CreateLease(leaseID, customerID, vehicleID, startDate, endDate, leaseType);
            Lease actualLease = repository.FindLeaseById(createdLease.LeaseId);

            // ASSERT
            Assert.NotNull(actualLease);
            Assert.AreEqual(customerID, actualLease.CustomerId);
            Assert.AreEqual(vehicleID, actualLease.VehicleId);
            Assert.AreEqual(startDate, actualLease.StartDate);
            Assert.AreEqual(endDate, actualLease.EndDate);
            Assert.AreEqual(leaseType, actualLease.LeaseType);
        }

        [Test]
        public void RetrieveExistingLeaseById_Successfully()
        {
            // ARRANGE
            int existingLeaseId = 6; 

            // ACT
            Lease actualLease = repository.FindLeaseById(existingLeaseId);

            // ASSERT
            Assert.NotNull(actualLease);
            Assert.AreEqual(existingLeaseId, actualLease.LeaseId);
            Assert.IsFalse(string.IsNullOrEmpty(actualLease.LeaseType)); 
        }

    }
}