//using Car_Rental_System.ExceptionHandlers;
//using Car_Rental_System.Models;
//using Car_Rental_System.Utils;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Car_Rental_System.Dao
//{
//    internal class negativeAns
//    {
//        public void RecordPayment(Lease lease, int amount)
//        {
//            Lease existingLease = crsContext.Leases.FirstOrDefault(l => l.LeaseId == lease.LeaseId);

//            if (existingLease != null)
//            {
//                var vehicle = crsContext.Vehicles.FirstOrDefault(v => v.VehicleId == existingLease.VehicleId);
//                if (vehicle == null)
//                {
//                    throw new Exception("Vehicle not found.");
//                }

//                if (existingLease.StartDate == null || existingLease.EndDate == null)
//                {
//                    throw new Exception("Lease dates are not properly set.");
//                }

//                var timeSpan = existingLease.EndDate.Value - existingLease.StartDate.Value;
//                int numberOfDays = timeSpan.Days;

//                if (vehicle.DailyRate == null)
//                {
//                    throw new Exception("Daily rate for the vehicle is not set.");
//                }

//                int totalAmount = numberOfDays * vehicle.DailyRate.Value;
//                //int pendingAmount = totalAmount - amount;

//                // Get existing payment record or create a new one if it doesn't exist
//                var existingPayment = crsContext.Payments.FirstOrDefault(p => p.LeaseId == existingLease.LeaseId);
//                int paidAmount = existingPayment?.Amount ?? 0;

//                // Calculate pending amount before the new payment
//                int pendingAmountBeforePayment = totalAmount - paidAmount;

//                // Subtract the given amount from the pending amount
//                int pendingAmountAfterPayment = pendingAmountBeforePayment - amount;

//                UpdatePaymentAmountInDatabase(existingLease, amount);
//                crsContext.SaveChanges();

//                Console.WriteLine($"Amount paid: {amount}");
//                Console.WriteLine($"Pending amount: {pendingAmountAfterPayment}");
//            }
//            else
//            {
//                throw new LeaseNotFoundE("Lease not found.");
//            }
//        }

//        private void UpdatePaymentAmountInDatabase(Lease existingLease, int amount)
//        {
//            var existingPayment = crsContext.Payments.FirstOrDefault(p => p.LeaseId == existingLease.LeaseId);
//            if (existingPayment != null)
//            {
//                existingPayment.Amount += amount;
//                crsContext.SaveChanges();
//            }
//            else
//            {
//                crsContext.Payments.Add(new Payment { LeaseId = existingLease.LeaseId, Amount = amount });
//                crsContext.SaveChanges();
//                //throw new PaymentNotFoundE("Payment not found for the lease in the database.");
//            }
//        }

//    }
//}
