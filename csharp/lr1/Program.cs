using System;
using Collections;
using Entities;

namespace VoronovLR1
{
    class Program
    {
        static void Main(string[] args)
        {
            SalaryDepartment department = new SalaryDepartment();

            // 1. Ввод информации о видах работ
            WorkType coding = new WorkType("Программирование", 1500.0);
            WorkType testing = new WorkType("Тестирование", 1000.0);
            WorkType design = new WorkType("Дизайн", 1200.0);

            department.AddWorkType(coding);
            department.AddWorkType(testing);
            department.AddWorkType(design);

            Console.WriteLine("Добавленные виды работ:");
            for (int i = 0; i < department.WorkTypes.Count; i++)
            {
                Console.WriteLine($"- {department.WorkTypes[i].Name} (Ставка: {department.WorkTypes[i].Rate} руб./ед.)");
            }
            Console.WriteLine();

            // 2. Ввод информации о работниках
            Employee emp1 = new Employee("Воронов");
            Employee emp2 = new Employee("Романов");
            Employee emp3 = new Employee("Сидоров");

            department.AddEmployee(emp1);
            department.AddEmployee(emp2);
            department.AddEmployee(emp3);

            Console.WriteLine("Добавленные работники:");
            for (int i = 0; i < department.Employees.Count; i++)
            {
                Console.WriteLine($"- {department.Employees[i].Name}");
            }
            Console.WriteLine();

            // 3. Регистрация выполненных работ
            department.AssignWork(emp1, coding, 10);
            department.AssignWork(emp1, testing, 5);
            department.AssignWork(emp2, coding, 8);
            department.AssignWork(emp2, design, 4);
            department.AssignWork(emp3, testing, 12);

            // 4. Вывод информации о работниках, выполнявших конкретную работу
            Console.WriteLine("Работники, выполнявшие работу 'Программирование':");
            MyCustomCollection<Employee> coders = department.GetEmployeesByWorkType(coding);
            for (int i = 0; i < coders.Count; i++)
            {
                Console.WriteLine($"- {coders[i].Name}");
            }
            Console.WriteLine();

            string searchName = "Иванов";
            Console.WriteLine($"Поиск зарплаты для работника: {searchName}");
            Employee foundEmp = null;
            for (int i = 0; i < department.Employees.Count; i++)
            {
                if (department.Employees[i].Name == searchName)
                {
                    foundEmp = department.Employees[i];
                    break;
                }
            }

            if (foundEmp != null)
            {
                double salary = department.CalculateEmployeeSalary(foundEmp);
                Console.WriteLine($"Зарплата работника {foundEmp.Name}: {salary} руб.");
            }
            else
            {
                Console.WriteLine("Работник не найден.");
            }
            Console.WriteLine();

            double totalPayroll = department.CalculateTotalPayroll();
            Console.WriteLine($"Общая сумма выплат всем работникам: {totalPayroll} руб.");
        }
    }
}