using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using Microsoft.Data.SqlClient;
using Car_Rental_System.Models;

namespace Car_Rental_System.Utils
{
    public class CrsContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Lease> Leases { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ReadConnectionStringFromFile("dbconfig.txt");
            if (connectionString != null)
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        private static string ReadConnectionStringFromFile(string fileName)
        {
            try
            {
                string[] lines = File.ReadAllLines(fileName);
                if (lines.Length >= 4)
                {
                    return $"Data Source={lines[0]};Initial Catalog={lines[1]};Integrated Security={lines[2]};Trust Server Certificate={lines[3]};";
                }
                else
                {
                    throw new Exception("Invalid format for connection string file");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading connection string: {ex.Message}");
                return null;
            }
        }
    }
}
