using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Rental_System.Dao;
using Car_Rental_System.Models;
using Car_Rental_System.ExceptionHandlers;
using Car_Rental_System.Utils;

namespace Car_Rental_System.Dao
{
    public class CarLeaseRepositoryImpl : ICarLeaseRepository
    {
        private Car_Rental_System.Models.CrsContext crsContext;

        public CarLeaseRepositoryImpl(Car_Rental_System.Models.CrsContext crContext)
        {
            this.crsContext = crContext;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            crsContext.Vehicles.Add(vehicle); 
            crsContext.SaveChanges();
        }
        public void RemoveVehicle(int vehicleID)
        {
            var vehicleToRemove = crsContext.Vehicles.FirstOrDefault(v => v.VehicleId == vehicleID);
            if (vehicleToRemove != null)
            {
                crsContext.Vehicles.Remove(vehicleToRemove);
                crsContext.SaveChanges();
                Console.WriteLine("Vehicle removed successfully.");
            }
            else
            {
                throw new VehicleNotFoundE($"Vehicle with ID {vehicleID} not found.");
            }
        }
        public List<Vehicle> ListAvailableVehicles()
        {
            return crsContext.Vehicles.Where(v => v.Status == "Available").ToList();
        }

        public List<Vehicle> ListRentedVehicles()
        {
            return crsContext.Vehicles.Where(v => v.Status == "Rented").ToList();
        }

        public Vehicle FindVehicleById(int vehicleID)
        {
            return crsContext.Vehicles.FirstOrDefault(v => v.VehicleId == vehicleID);
        }


        // Customer Methods Impl
        public void AddCustomer(Customer customer)
        {
            crsContext.Customers.Add(customer);
            crsContext.SaveChanges();
        }

        public void RemoveCustomer(int customerId)
        {
            Customer customerToRemove = crsContext.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customerToRemove != null)
            {
                crsContext.Customers.Remove(customerToRemove);
                crsContext.SaveChanges();
            }
            else
            {
                // Optionally, handle the case where customerToRemove is null
                Console.WriteLine($"Customer with ID {customerId} not found.");
            }
        }

        public List<Customer> ListCustomers()
        {
            return crsContext.Customers.ToList();
        }

        public Customer FindCustomerById(int customerId)
        {
            return crsContext.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        //Lease Methods Impl
        public Lease CreateLease(int customerID, int vehicleID, DateTime startDate, DateTime endDate, string leaseType)
        {
            // Assuming vehicle and customer exist
            Customer customer = crsContext.Customers.FirstOrDefault(c => c.CustomerId == customerID);
            Vehicle vehicle = crsContext.Vehicles.FirstOrDefault(v => v.VehicleId == vehicleID);

            if (vehicle == null)
            {
                throw new VehicleNotFoundE($"Vehicle with ID {vehicleID} not found.");
            }
            else if (customer != null && vehicle != null)
            {
                Lease newLease = new Lease
                {
                    CustomerId = customerID,
                    VehicleId = vehicleID,
                    StartDate = startDate,
                    EndDate = endDate,
                    LeaseType = leaseType
                };

                crsContext.Leases.Add(newLease);
                crsContext.SaveChanges();
                return newLease;
            }
            else
            {
                throw new Exception("Customer or Vehicle not found.");
            }
        }

        private double CalculateLeaseAmount(DateTime startDate, DateTime endDate, string leaseType)
        {
            double amount = 0;

            if (leaseType == "DailyLease")
            {
                int days = (endDate - startDate).Days;
                amount = days * 100;
            }
            else if (leaseType == "MonthlyLease")
            {
                int months = ((endDate.Year - startDate.Year) * 12) + endDate.Month - startDate.Month;

                DateTime endOfLastMonth = new DateTime(startDate.Year, startDate.Month, DateTime.DaysInMonth(startDate.Year, startDate.Month));
                int remainingDays = (endDate - endOfLastMonth).Days;

                amount = (months * 1000) + (remainingDays * 100);
            }
            else
            {
                throw new ArgumentException("Invalid lease type.");
            }

            return amount;
        }

        public Lease ReturnVehicle(int leaseID)
        {
            Lease leaseToReturn = crsContext.Leases.FirstOrDefault(l => l.LeaseId == leaseID);
            if (leaseToReturn != null)
            {
                crsContext.Leases.Remove(leaseToReturn);
                crsContext.SaveChanges();
                return leaseToReturn;
            }
            else
            {
                throw new LeaseNotFoundE($"Lease with ID {leaseID} not found.");
            }
        }

        public List<Lease> ListActiveLeases()
        {
            return crsContext.Leases.Where(l => l.EndDate >= DateTime.Now).ToList();
        }

        public List<Lease> ListLeaseHistory()
        {
            return crsContext.Leases.Where(l => l.EndDate < DateTime.Now).ToList();
        }

        //Payment Methods Impl
        public void RecordPayment(Lease lease, int amount)
        {
            Lease existingLease = crsContext.Leases.FirstOrDefault(l => l.LeaseId == lease.LeaseId);

            if (existingLease != null)
            {
                UpdatePaymentAmountInDatabase(existingLease, amount);
            }
            else
            {
                throw new LeaseNotFoundE("Lease not found.");
            }
        }

        private Lease FindLeaseById(int leaseId)
        {
            return crsContext.Leases.FirstOrDefault(l => l.LeaseId == leaseId);
        }

        private void UpdatePaymentAmountInDatabase(Lease existingLease, int amount)
        {
            var existingPayment = crsContext.Payments.FirstOrDefault(p => p.LeaseId == existingLease.LeaseId);
            if (existingPayment != null)
            {
                existingPayment.Amount += amount;
                crsContext.SaveChanges();
            }
            else
            {
                throw new PaymentNotFoundE("Payment not found for the lease in the database.");
            }
        }

    }
}
