using System;
using System.Collections.Generic;
using Entities;

namespace Voronov_LR3
{
    class Program
    {
        static void Main(string[] args)
        {
            SalaryDepartment department = new SalaryDepartment();
            Journal journal = new Journal();

            // Подписка журнала на изменения списков
            department.ListChanged += journal.LogEvent;

            // Подписка Program на выполнение работ через лямбду
            department.WorkPerformed += (object? sender, WorkPerformed e) =>
            {
                Console.WriteLine($"{e}");
            };

            // Инициализация видов работ
            WorkType coding = new WorkType("Программирование", 15.0);
            WorkType testing = new WorkType("Тестирование", 10.0);
            WorkType design = new WorkType("Дизайн", 12.0);

            department.AddWorkType(coding);
            department.AddWorkType(testing);
            department.AddWorkType(design);

            // Инициализация сотрудников
            Employee emp1 = new Employee("Воронов 1");
            Employee emp2 = new Employee("Воронов 2");
            Employee emp3 = new Employee("Воронов 3");

            department.AddEmployee(emp1);
            department.AddEmployee(emp2);
            department.AddEmployee(emp3);

            Console.WriteLine("\n--- Регистрация выполненных работ ---");
            department.AssignWork(emp1, coding, 8);  // 15 * 8 = 120
            department.AssignWork(emp1, testing, 4); // 10 * 4 = 40  -> Всего: 160

            department.AssignWork(emp2, coding, 10); // 15 * 10 = 150
            department.AssignWork(emp2, design, 5);  // 12 * 5 = 60   -> Всего: 210

            department.AssignWork(emp3, testing, 6);  // 10 * 6 = 60   -> Всего: 60
            Console.WriteLine();

            journal.PrintLog();
            Console.WriteLine();

            Console.WriteLine("--- РЕЗУЛЬТАТЫ ЗАПРОСОВ LINQ ---");

            Console.WriteLine("\n1. Список тарифов работ, отсортированный по стоимости:");
            List<string> sortedWorks = department.GetSortedWorkTypeNames();
            foreach (var name in sortedWorks)
            {
                Console.WriteLine($"  - {name} ( {department.WorkTypes[name].Rate}р/час)");
            }

            double totalPayroll = department.GetTotalPayroll();
            Console.WriteLine($"\n2. Общая сумма выплат всем работникам предприятия: {totalPayroll}р");

            string searchName1 = "Воронов 1";
            double salary1 = department.GetEmployeeSalaryByName(searchName1);
            Console.WriteLine($"\n3. Зарплата сотрудника {searchName1}: {salary1}р");

            string searchName2 = "Воронов 2";
            double salary2 = department.GetEmployeeSalaryByName(searchName2);
            Console.WriteLine($"\n   Зарплата сотрудника {searchName2}: {salary2}р");

            string searchName3 = "Воронов 3";
            double salary3 = department.GetEmployeeSalaryByName(searchName3);
            Console.WriteLine($"\n   Зарплата сотрудника {searchName3}: {salary3}р");

            string maxEarner = department.GetMaxEarnerName();
            Console.WriteLine($"\n4. Работник с максимальной зарплатой: {maxEarner}");

            double threshold = 100.0;
            int countHighEarners = department.GetCountOfEmployeesEarningMoreThan(threshold);
            Console.WriteLine($"\n5. Количество работников с доходом > {threshold}: {countHighEarners}");

            Console.WriteLine($"\n6. Распределение сумм по видам работ для {emp2.Name}:");
            var breakDown = department.GetEmployeeEarningsByWorkType(emp2);
            foreach (var pair in breakDown)
            {
                Console.WriteLine($"  - Вид работы: {pair.Key} | Получено: {pair.Value}");
            }
        }
    }
}
