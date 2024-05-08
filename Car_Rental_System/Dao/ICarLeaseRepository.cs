using Car_Rental_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Car_Rental_System.Dao
{
   public interface ICarLeaseRepository
   {
        public void AddVehicle(Vehicle vehicle);
        public void RemoveVehicle(int vehicleID);
        public List<Vehicle> ListAvailableVehicles();
        public List<Vehicle> ListRentedVehicles();
        public Vehicle FindVehicleById(int vehicleID);
        
        //Customer
        public void AddCustomer(Customer customer);
        public void RemoveCustomer(int customerID);
        public List<Customer> ListCustomers();
        public Customer FindCustomerById(int customerID);
        
        //Lease
        public Lease CreateLease(int customerID, int vehicleID, DateTime startDate, DateTime endDate, string leaseType);
        public Lease ReturnVehicle(int leaseID);
        public List<Lease> ListActiveLeases();
        public List<Lease> ListLeaseHistory();
        
        //Payment
        public void RecordPayment(Lease lease, int amount);
    }
}
