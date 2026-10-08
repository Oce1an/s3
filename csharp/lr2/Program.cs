<<<<<<< HEAD
﻿Console.WriteLine("Hello, World!");
=======
using System;
using GGGGGG_NNN_Lab2.Collections;
using GGGGGG_NNN_Lab2.Entities;

namespace GGGGGG_NNN_Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Lab 2: Payroll Department System");

            // 1. Initialize System
            var payroll = new PayrollDepartment();
            var journal = new Journal();

            // 2. Subscribe Events
            // Journal subscribes to changes in tariffs/employees
            payroll.TariffsOrEmployeesChanged += journal.LogEvent;

            // Program subscribes to work performed via Lambda
            payroll.WorkPerformed += (sender, e) =>
            {
                Console.WriteLine($"[Program Handler] Action: {e.Message}");
            };

            try
            {
                // 3. Add Data
                Console.WriteLine("\n--- Adding Work Types ---");
                payroll.AddWorkType(new WorkType("Programming", 50.0m));
                payroll.AddWorkType(new WorkType("Testing", 30.0m));
                payroll.AddWorkType(new WorkType("Design", 40.0m));

                Console.WriteLine("\n--- Adding Employees ---");
                var emp1 = new Employee("Ivanov", "Ivan");
                var emp2 = new Employee("Petrov", "Petr");
                payroll.AddEmployee(emp1);
                payroll.AddEmployee(emp2);

                // 4. Register Work
                Console.WriteLine("\n--- Registering Work ---");
                payroll.AssignWork("Ivanov", new WorkRecord("Programming", 8, DateTime.Now));
                payroll.AssignWork("Ivanov", new WorkRecord("Testing", 2, DateTime.Now));
                payroll.AssignWork("Petrov", new WorkRecord("Design", 5, DateTime.Now));

                // 5. Calculate Salaries
                Console.WriteLine("\n--- Calculating Salaries ---");
                decimal salaryIvanov = payroll.CalculateEmployeeSalary("Ivanov");
                Console.WriteLine($"Salary for Ivanov: {salaryIvanov}");

                decimal salaryPetrov = payroll.CalculateEmployeeSalary("Petrov");
                Console.WriteLine($"Salary for Petrov: {salaryPetrov}");

                decimal totalPayouts = payroll.CalculateTotalPayouts();
                Console.WriteLine($"Total Payouts: {totalPayouts}");

                // 6. Demonstrate Exception Handling
                Console.WriteLine("\n--- Exception Handling Demo ---");

                // Test IndexOutOfRangeException
                try
                {
                    var invalidItem = payroll.WorkTypes[100];
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine($"Caught IndexOutOfRangeException: {ex.Message}");
                }

                // Test Custom Exception (Item not found)
                try
                {
                    var nonExistentWork = new WorkType("Cleaning", 10);
                    payroll.WorkTypes.Remove(nonExistentWork);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Caught Custom Exception: {ex.Message}");
                }

                // 7. Print Journal
                Console.WriteLine("\n--- Journal Output ---");
                journal.PrintLogs();

                // 8. Demonstrate Foreach (IEnumerable)
                Console.WriteLine("\n--- Iterating Employees via Foreach ---");
                foreach (var emp in payroll.Employees)
                {
                    Console.WriteLine($"Employee: {emp.Surname} {emp.FirstName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            Console.ReadKey();
        }
    }
}
>>>>>>> 0ce0b3c0fcd791052b20deb47a465015d48c1e96
