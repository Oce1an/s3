using System;
using Collections;
using Entities;
using Exceptions;

namespace Voronov_LR2
{
    class Program
    {
        static void Main(string[] args)
        {
            SalaryDepartment department = new SalaryDepartment();

            Journal journal = new Journal();
            department.ListChanged += journal.LogEvent;

            department.WorkPerformed += (object? sender, WorkPerformed e) =>
            {
                Console.WriteLine($"  {e}");
            };

            WorkType coding = new WorkType("Программирование", 15.0);
            WorkType testing = new WorkType("Тестирование", 10.0);
            WorkType design = new WorkType("Дизайн", 12.0);

            department.AddWorkType(coding);
            department.AddWorkType(testing);
            department.AddWorkType(design);

            Employee emp1 = new Employee("Воронов1");
            Employee emp2 = new Employee("Воронов2");
            Employee emp3 = new Employee("Воронов3");

            department.AddEmployee(emp1);
            department.AddEmployee(emp2);
            department.AddEmployee(emp3);
            Console.WriteLine();

            Console.WriteLine("--- Регистрация выполненных работ ---");
            department.AssignWork(emp1, coding, 3);
            department.AssignWork(emp1, testing, 5);
            department.AssignWork(emp2, coding, 4);
            department.AssignWork(emp2, design, 4);
            department.AssignWork(emp3, testing, 12);
            Console.WriteLine();

            journal.PrintLog();
            Console.WriteLine();

            Console.WriteLine("Работники, выполнявшие 'Программирование':");
            MyCustomCollection<Employee> coders = department.GetEmployeesByWorkType(coding);
            foreach (Employee emp in coders)
            {
                Console.WriteLine($"  - {emp.Name}");
            }
            Console.WriteLine();

            string searchName1 = "Воронов1";
            foreach (Employee emp in department.Employees)
            {
                if (emp.Name == searchName1)
                {
                    double salary = department.CalculateEmployeeSalary(emp);
                    Console.WriteLine($"Зарплата работника {searchName1} за день: {salary}");
                    break;
                }
            }
            Console.WriteLine();

            string searchName2 = "Воронов2";
            foreach (Employee emp in department.Employees)
            {
                if (emp.Name == searchName2)
                {
                    double salary = department.CalculateEmployeeSalary(emp);
                    Console.WriteLine($"Зарплата работника {searchName2} за день: {salary}");
                    break;
                }
            }
            Console.WriteLine();

            string searchName3 = "Воронов3";
            foreach (Employee emp in department.Employees)
            {
                if (emp.Name == searchName3)
                {
                    double salary = department.CalculateEmployeeSalary(emp);
                    Console.WriteLine($"Зарплата работника {searchName3} за день: {salary}");
                    break;
                }
            }
            Console.WriteLine();

            double totalPayroll = department.CalculateTotalPayroll();
            Console.WriteLine($"Общая сумма выплат: {totalPayroll}");
            Console.WriteLine();

            Console.WriteLine("--- Демонстрация обработки исключений ---");

            try
            {
                Console.WriteLine("\nПопытка обращения к индексу 100");
                var item = department.Employees[100];
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"  Перехвачено IndexOutOfRangeException: {ex.Message}");
            }

            try
            {
                Console.WriteLine("\nПопытка удалить несуществующий вид работы");
                WorkType fakeWork = new WorkType("Несуществующая работа", 0);
                department.WorkTypes.Remove(fakeWork);
            }
            catch (ItemNotFoundException ex)
            {
                Console.WriteLine($"  Перехвачено ItemNotFoundException: {ex.Message}");
            }

            try
            {
                Console.WriteLine("\nПопытка обращения к индексу -1");
                var item = department.Employees[-1];
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"  Перехвачено IndexOutOfRangeException: {ex.Message}");
            }
        }
    }
}