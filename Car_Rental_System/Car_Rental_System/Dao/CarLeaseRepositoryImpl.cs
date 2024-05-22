using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Car_Rental_System.Dao;
using Car_Rental_System.Models;
using Car_Rental_System.ExceptionHandlers;
using Car_Rental_System.Utils;
using Microsoft.EntityFrameworkCore;

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
            return crsContext.Vehicles.Where(v => v.Status == "Rented" || v.Status =="notAvailable").ToList();
        }

        public Vehicle FindVehicleById(int vehicleID)
        {
            var vehicle = crsContext.Vehicles.FirstOrDefault(v => v.VehicleId == vehicleID);
            if (vehicle == null)
            {
                throw new VehicleNotFoundE($"Vehicle with ID {vehicleID} not found.");
            }
            return vehicle;
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
            var customer = crsContext.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer == null)
            {
                throw new CustomerNotFoundE($"Customer with ID {customerId} not found.");
            }
            return customer;
        }

        //Lease Methods Impl
        public Lease CreateLease(int leaseID,int customerID, int vehicleID, DateTime startDate, DateTime endDate, string leaseType)
        {
            //check if vehicle available
            bool isVehicleAvailable = crsContext.Leases
                                                .Where(l => l.VehicleId == vehicleID && l.StartDate <= endDate && l.EndDate >= startDate)
                                                .FirstOrDefault() == null;

            if (!isVehicleAvailable)
            {
                throw new Exception("The vehicle is not available for the requested lease period.");
            }

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
                    LeaseId = leaseID,
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

        public (Lease lease, Vehicle vehicle) ReturnVehicle(int leaseID)
        {
            //left join
            Lease leaseToRetrieve = crsContext.Leases
                .Include(l => l.Vehicle)
                .FirstOrDefault(l => l.LeaseId == leaseID);
            if (leaseToRetrieve != null)
            {
                return (leaseToRetrieve, leaseToRetrieve.Vehicle);
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
                Vehicle vehicle = crsContext.Vehicles.FirstOrDefault(v => v.VehicleId == existingLease.VehicleId);
                if (vehicle != null)
                {
                    if (existingLease.StartDate == null || existingLease.EndDate == null)
                    {
                        throw new Exception("Start date or end date for the lease is not set.");
                    }

                    DateTime startDate = existingLease.StartDate.Value;  
                    DateTime endDate = existingLease.EndDate.Value;

                    if (vehicle.DailyRate == null)
                    {
                        throw new Exception("Daily rate for the vehicle is not set.");
                    }
                    int dailyRate = vehicle.DailyRate.Value;

                    int totalDays = (endDate - startDate).Days;
                    int totalAmountDue = totalDays * dailyRate;

                    UpdatePaymentAmountInDatabase(existingLease, totalAmountDue, amount);
                }
                else
                {
                    throw new Exception("Vehicle not found for the lease.");
                }
            }
            else
            {
                throw new LeaseNotFoundE("Lease not found.");
            }

        }

        public Lease FindLeaseById(int leaseId)
        {
            var lease = crsContext.Leases.FirstOrDefault(l => l.LeaseId == leaseId);
            if (lease == null)
            {
                throw new LeaseNotFoundE($"Lease with ID {leaseId} not found.");
            }
            return lease;
        }

        private void UpdatePaymentAmountInDatabase(Lease existingLease, int totalAmountDue, int paymentAmount)
        {
            var existingPayment = crsContext.Payments.FirstOrDefault(p => p.LeaseId == existingLease.LeaseId);
            if (existingPayment != null)
            {
                if (existingPayment.Amount == null)
                {
                    throw new Exception("Existing payment amount is null.");
                }

                double currentAmount = existingPayment.Amount.Value; 
                double newBalance = currentAmount - paymentAmount;
                existingPayment.Amount = (int)newBalance; 
                crsContext.SaveChanges();
                Console.WriteLine($"Payment recorded. Remaining balance: {newBalance}");
            }
            else
            {
                int initialBalance = totalAmountDue - paymentAmount;
                crsContext.Payments.Add(new Payment { LeaseId = existingLease.LeaseId, Amount = initialBalance });
                crsContext.SaveChanges();
                Console.WriteLine($"Payment recorded. Remaining balance: {initialBalance}");
            }
        }

    }
}
