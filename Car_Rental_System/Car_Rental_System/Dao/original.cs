//using Car_Rental_System.ExceptionHandlers;
//using Car_Rental_System.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Car_Rental_System.Dao
//{
//    internal class original
//    {
//        static void PaymentOperationsMenu(CarLeaseRepositoryImpl repository)
//        {
//            while (true)
//            {
//                Console.WriteLine("Payment Operations Menu:");
//                Console.WriteLine("************************");
//                Console.WriteLine("1. Record Payment");
//                Console.WriteLine("2. Exit");
//                Console.WriteLine("************************");

//                Console.Write("Enter your choice: ");
//                int choice = int.Parse(Console.ReadLine());

//                switch (choice)
//                {
//                    case 1:
//                        try
//                        {

//                            Console.Write("Enter Lease ID: ");
//                            int leaseId = int.Parse(Console.ReadLine());
//                            Console.Write("Enter Amount: ");
//                            int amount = int.Parse(Console.ReadLine());

//                            using (var context = new Car_Rental_System.Models.CrsContext())
//                            {
//                                var lease = context.Leases.FirstOrDefault(l => l.LeaseId == leaseId);

//                                if (lease != null)
//                                {
//                                    var existingPayment = context.Payments.FirstOrDefault(p => p.LeaseId == leaseId);
//                                    if (existingPayment != null)
//                                    {
//                                        existingPayment.Amount += amount;
//                                    }
//                                    else
//                                    {
//                                        context.Payments.Add(new Payment { LeaseId = leaseId, Amount = amount });
//                                    }
//                                    context.SaveChanges();
//                                    Console.WriteLine("Payment recorded successfully.");
//                                }

//                                else
//                                {
//                                    throw new LeaseNotFoundE("Lease not found.");
//                                }
//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            Console.WriteLine($"Error: {ex.Message}");
//                        }

//                        break;

//                        break;
//                    case 2:
//                        return;

//                    default:
//                        Console.WriteLine("Invalid choice. Please enter a valid option.");
//                        break;
//                }
//            }
//        }

//using Car_Rental_System.ExceptionHandlers;
//using Car_Rental_System.Models;
//using Car_Rental_System.Utils;

//public void RecordPayment(Lease lease, int amount)
//{
//    Lease existingLease = crsContext.Leases.FirstOrDefault(l => l.LeaseId == lease.LeaseId);

//    if (existingLease != null)
//    {
//        UpdatePaymentAmountInDatabase(existingLease, amount);
//    }
//    else
//    {
//        throw new LeaseNotFoundE("Lease not found.");
//    }
//}

//public Lease FindLeaseById(int leaseId)
//{
//    var lease = crsContext.Leases.FirstOrDefault(l => l.LeaseId == leaseId);
//    if (lease == null)
//    {
//        throw new LeaseNotFoundE($"Lease with ID {leaseId} not found.");
//    }
//    return lease;
//}

//private void UpdatePaymentAmountInDatabase(Lease existingLease, int amount)
//{
//    var existingPayment = crsContext.Payments.FirstOrDefault(p => p.LeaseId == existingLease.LeaseId);
//    if (existingPayment != null)
//    {
//        existingPayment.Amount += amount;
//        crsContext.SaveChanges();
//    }
//    else
//    {
//        throw new PaymentNotFoundE("Payment not found for the lease in the database.");
//    }
//}


//    }
//}
