using System;
using Car_Rental_System.Dao;
using Car_Rental_System.ExceptionHandlers;
using Car_Rental_System.Models;
using Car_Rental_System.Utils;
using Microsoft.EntityFrameworkCore;

namespace Car_Rental_System
{
    class Program
    {
        static void Main(string[] args)
        {
            Car_Rental_System.Models.CrsContext crsContext = new Car_Rental_System.Models.CrsContext(); 
            CarLeaseRepositoryImpl repository = new CarLeaseRepositoryImpl(crsContext); 

            // Main menu 
            while (true)
            {
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Vehicle Operations");
                Console.WriteLine("2. Customer Operations");
                Console.WriteLine("3. Lease Operations");
                Console.WriteLine("4. Payment Operations");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        VehicleOperationsMenu(repository);
                        break;
                    case 2:
                        CustomerOperationsMenu(repository);
                        break;
                    case 3:
                        LeaseOperationsMenu(repository);
                        break;
                    case 4:
                        PaymentOperationsMenu(repository);
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }

        static void VehicleOperationsMenu(CarLeaseRepositoryImpl repository)
        {
            while (true)
            {
                Console.WriteLine("\nVehicle Operations Menu:");
                Console.WriteLine("1. Add Vehicle");
                Console.WriteLine("2. Remove Vehicle");
                Console.WriteLine("3. List Available Vehicles");
                Console.WriteLine("4. List Rented Vehicles");
                Console.WriteLine("5. Find Vehicle by ID");
                Console.WriteLine("6. Back to Main Menu");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                            Console.Write("\nEnter Vehicle details\n");
                            Console.WriteLine();

                            Console.Write("Enter VehicleId: ");
                            int vehicleID = int.Parse(Console.ReadLine());

                            Console.Write("Enter make: ");
                            string make = Console.ReadLine();

                            Console.Write("Enter model: ");
                            string model = Console.ReadLine();

                            Console.Write("Enter year(yyyy): ");
                            int year = int.Parse(Console.ReadLine());

                            Console.Write("Enter Daily Rate: ");
                            int rate = int.Parse(Console.ReadLine());

                            Console.Write("Enter Status: ");
                            string stat = Console.ReadLine();

                            Console.Write("Enter Passenger Capacity: ");
                            int capacity = int.Parse(Console.ReadLine());

                            Console.Write("Enter Engine Capacity: ");
                            int e_capacity = int.Parse(Console.ReadLine());

                            Vehicle vehicle = new Vehicle
                            {
                                VehicleId = vehicleID,
                                Make = make,
                                Model = model,
                                Year = year,
                                DailyRate = rate,
                                Status = stat,
                                PassengerCapacity = capacity,
                                EngineCapacity = e_capacity
                            };
                            repository.AddVehicle(vehicle);
                            Console.WriteLine("\nVehicle added successfully.");
                        break;

                    case 2:
                        try
                            { 
                                Console.Write("Enter Vehicle ID to remove: ");
                                int vehicleIDToRemove = int.Parse(Console.ReadLine());

                                repository.RemoveVehicle(vehicleIDToRemove);
                                Console.WriteLine("Vehicle removed successfully.");
                            }
                            catch (VehicleNotFoundE ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        break;
                    
                    case 3:
                        var vehicles = repository.ListAvailableVehicles();
                        foreach (var car in vehicles)
                        {
                            Console.WriteLine($"Vehcile ID: {car.VehicleId}, Make: {car.Make}, Model: {car.Model}, Year: {car.Year}, Daily Rate: {car.DailyRate}, Status: {car.Status}, Passenger Capacity: {car.PassengerCapacity}, Engine Capacity: {car.EngineCapacity}");
                        }
                        break;

                    case 4:
                        var rentedVehicles = repository.ListRentedVehicles();
                        if (rentedVehicles.Count == 0)
                        {
                            Console.WriteLine("No vehicles are currently rented.");
                        }
                        else
                        {
                            Console.WriteLine("Rented Vehicles:");
                            foreach (var listvehicle in rentedVehicles)
                            {
                                Console.WriteLine($"Vehicle ID: {listvehicle.VehicleId}, Make: {listvehicle.Make}, Model: {listvehicle.Model}, Year: {listvehicle.Year}, Daily Rate: {listvehicle.DailyRate}, Status: {listvehicle.Status}, Passenger Capacity: {listvehicle.PassengerCapacity}, Engine Capacity: {listvehicle.EngineCapacity}");
                            }
                        }
                        break;

                    case 5:
                        Console.Write("Enter Vehicle ID to find: ");
                        int vehicleIDTofind = int.Parse(Console.ReadLine());

                        var foundvehicle = repository.FindVehicleById(vehicleIDTofind);
                        if (foundvehicle != null)
                        {
                            Console.WriteLine($"Vehicle found: Vehicle ID: {foundvehicle.VehicleId}, Make: {foundvehicle.Make}, Model: {foundvehicle.Model}, Year: {foundvehicle.Year}, Daily Rate: {foundvehicle.DailyRate}, Status: {foundvehicle.Status}, Passenger Capacity: {foundvehicle.PassengerCapacity}, Engine Capacity: {foundvehicle.EngineCapacity}");
                        }
                        else
                        {
                            Console.WriteLine($"Vehicle with ID {vehicleIDTofind} not found");
                        }
                        break;

                    case 6:
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }

        static void CustomerOperationsMenu(CarLeaseRepositoryImpl repository)
        {
            while (true)
            {
                Console.WriteLine("Customer Operations Menu:");
                Console.WriteLine("1. Add Customer");
                Console.WriteLine("2. Remove Customer");
                Console.WriteLine("3. List Customers");
                Console.WriteLine("4. Find Customer by ID");
                Console.WriteLine("5. Back to Main Menu");

                Console.Write("\nEnter your choice: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                case 1:
                      Console.Write("\nEnter customer details:\n");

                      Console.Write("Enter Customer ID: ");
                      int customerId = int.Parse(Console.ReadLine());

                      Console.Write("Enter First Name: ");
                      string firstName = Console.ReadLine();

                      Console.Write("Enter Last Name: ");
                      string lastName = Console.ReadLine();

                      Console.Write("Enter Email: ");
                      string email = Console.ReadLine();

                      Console.Write("Enter Phone Number: ");
                      string phoneNumber = Console.ReadLine();

                      Customer customer = new Customer
                      {
                           CustomerId = customerId,
                           FirstName = firstName,
                           LastName = lastName,
                           Email = email,
                           PhoneNumber = phoneNumber 
                      };
                      repository.AddCustomer(customer);
                      Console.WriteLine("Customer added successfully.");
                      break;
                case 2:
                     try
                     {
                         Console.Write("Enter Customer ID to remove: ");
                         int customerIdToRemove = int.Parse(Console.ReadLine());

                         repository.RemoveCustomer(customerIdToRemove);
                         Console.WriteLine("Customer removed successfully.");
                     }
                     catch (CustomerNotFoundE ex)
                     {
                         Console.WriteLine($"Error: {ex.Message}");
                     }
                     break;
                case 3:
                     var customers = repository.ListCustomers();
                     foreach (var customerList in customers)
                     {
                          Console.WriteLine($"Customer ID: {customerList.CustomerId}, Name: {customerList.FirstName} {customerList.LastName}, Email: {customerList.Email}, Phone Number: {customerList.PhoneNumber}");
                     }
                     break;
                case 4:
                        Console.Write("Enter Customer ID to find: ");
                        int customerIdToFind = int.Parse(Console.ReadLine());

                        var customer1 = repository.FindCustomerById(customerIdToFind);
                        if (customer1 != null)
                        {
                            Console.WriteLine($"Customer found: ID: {customer1.CustomerId}, Name: {customer1.FirstName} {customer1.LastName}, Email: {customer1.Email}, Phone Number: {customer1.PhoneNumber}");
                        }
                        else
                        {
                            Console.WriteLine("Customer not found.");
                        }
                        break;
                case 5:
                        return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }

        static void LeaseOperationsMenu(CarLeaseRepositoryImpl repository)
        {
            while (true)
            {
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Create Lease");
                Console.WriteLine("2. Return Car");
                Console.WriteLine("3. List Active Leases");
                Console.WriteLine("4. List Lease History");
                Console.WriteLine("5. Return to Main Menu");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.WriteLine("Enter lease details:");

                            Console.Write("Customer ID: ");
                            int customerId = int.Parse(Console.ReadLine());
                                
                            Console.Write("Vehicle ID: ");
                            int vehicleId = int.Parse(Console.ReadLine());
                                
                            Console.Write("Start Date (yyyy-MM-dd): ");
                            DateTime startDate = DateTime.Parse(Console.ReadLine());
                                
                            Console.Write("End Date (yyyy-MM-dd): ");
                            DateTime endDate = DateTime.Parse(Console.ReadLine());
                                
                            Console.Write("Lease Type (DailyLease/MonthlyLease): ");
                            string leaseType = Console.ReadLine();

                            Lease lease = repository.CreateLease
                            (customerId, vehicleId, startDate, endDate, leaseType);
                            Console.WriteLine("Lease created successfully.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case 2:
                        try
                        {
                            Console.Write("Enter Lease ID to return Vehicle Details: ");
                            int leaseId = int.Parse(Console.ReadLine());

                            Lease returnedLease = repository.ReturnVehicle(leaseId);
                            Console.WriteLine($"Car returned for Lease ID: {returnedLease.LeaseId}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case 3:
                        try
                        {
                            List<Lease> activeLeases = repository.ListActiveLeases();
                            Console.WriteLine("Active Leases:");
                            foreach (var lease in activeLeases)
                            {
                                Console.WriteLine($"Lease ID: {lease.LeaseId}, Customer ID: {lease.CustomerId}, Vehicle ID: {lease.VehicleId}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case 4:
                        try
                        {
                            List<Lease> leaseHistory = repository.ListLeaseHistory();
                            Console.WriteLine("Lease History:");
                            foreach (var lease in leaseHistory)
                            {
                                Console.WriteLine($"Lease ID: {lease.LeaseId}, Customer ID: {lease.CustomerId}, Vehicle ID: {lease.VehicleId}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }

        static void PaymentOperationsMenu(CarLeaseRepositoryImpl repository)
        {
            while (true)
            {
                Console.WriteLine("Main Menu:");
                Console.WriteLine("1. Record Payment");
                Console.WriteLine("2. Exit");

                Console.Write("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        try
                        {
                            Console.Write("Enter Lease ID: ");
                            int leaseId = int.Parse(Console.ReadLine());
                            Console.Write("Enter Amount: ");
                            int amount = int.Parse(Console.ReadLine());

                            using (var context = new Car_Rental_System.Models.CrsContext())
                            {
                                var lease = context.Leases.FirstOrDefault(l => l.LeaseId == leaseId);

                                //Lease lease = GetLeaseById(leaseId);
                                if (lease != null)
                                {
                                    var existingPayment = context.Payments.FirstOrDefault(p => p.LeaseId == leaseId);
                                    if (existingPayment != null)
                                    {
                                        existingPayment.Amount += amount;
                                    }
                                    else
                                    {
                                        context.Payments.Add(new Payment { LeaseId = leaseId, Amount = amount });
                                    }
                                    context.SaveChanges();
                                    Console.WriteLine("Payment recorded successfully.");
                                }
                                //UpdatePaymentAmountInDatabase(lease, amount);
                                //Console.WriteLine("Payment recorded successfully.");
                                else
                                {
                                    throw new LeaseNotFoundE("Lease not found.");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                        
                        //static Lease GetLeaseById(int leaseId)
                        //{
                        //    using (var context = new Car_Rental_System.Models.CrsContext())
                        //    {
                        //        return context.Leases.FirstOrDefault(l => l.LeaseId == leaseId);
                        //    }
                        //}

                        //void UpdatePaymentAmountInDatabase(Lease lease, int amount)
                        //{
                        //    using (var dbContext = new Car_Rental_System.Models.CrsContext())
                        //    {
                        //        var existingPayment = dbContext.Payments.FirstOrDefault(p => p.LeaseId == lease.LeaseId);
                        //        if (existingPayment != null)
                        //        {
                        //            existingPayment.Amount += amount;
                        //            dbContext.SaveChanges();
                        //        }
                        //        else
                        //        {
                        //            throw new PaymentNotFoundE("Payment not found for the lease in the database.");
                        //        }
                        //    }
                        //}
                        break;

                    case 2:
                        return;

                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }
            }
        }
    }
}
