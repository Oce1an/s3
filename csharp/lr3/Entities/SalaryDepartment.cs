using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class SalaryDepartment
    {
        // Для списка работ отдела использовать Dictionary<string, T>
        public Dictionary<string, WorkType> WorkTypes { get; set; }

        // Для списка работников использовать List<T>
        public List<Employee> Employees { get; set; }

        // События из лабораторной работы №2
        public event EventHandler<PayrollList>? ListChanged;
        public event EventHandler<WorkPerformed>? WorkPerformed;

        public SalaryDepartment()
        {
            WorkTypes = new Dictionary<string, WorkType>();
            Employees = new List<Employee>();
        }

        public void AddWorkType(WorkType workType)
        {
            WorkTypes[workType.Name] = workType;
            OnListChanged(new PayrollList("Добавлен", "Вид работы", workType.Name));
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
            OnListChanged(new PayrollList("Добавлен", "Работник", employee.Name));
        }

        public void AssignWork(Employee employee, WorkType workType, double quantity)
        {
            employee.PerformedWorks.Add(new WorkRecord(workType, quantity));
            OnWorkPerformed(new WorkPerformed(employee.Name, workType.Name, quantity));
        }

        protected virtual void OnListChanged(PayrollList e)
        {
            ListChanged?.Invoke(this, e);
        }

        protected virtual void OnWorkPerformed(WorkPerformed e)
        {
            WorkPerformed?.Invoke(this, e);
        }

        // ==================== МЕТОДЫ LINQ (ВАРИАНТ 6) ====================

        // 1. Получение списка названий всех тарифов работ, отсортированного по стоимости
        public List<string> GetSortedWorkTypeNames()
        {
            return WorkTypes.Values
                .OrderBy(w => w.Rate)
                .Select(w => w.Name)
                .ToList();
        }

        // Вспомогательный метод для расчета зарплаты одного сотрудника по текущим тарифам
        public double CalculateEmployeeSalary(Employee employee)
        {
            return employee.PerformedWorks
                .Sum(record => record.WorkType.Rate * record.Quantity);
        }

        // 2. Получение общей стоимости всех выполненных работ (общий фонд выплат)
        public double GetTotalPayroll()
        {
            return Employees.Sum(emp => CalculateEmployeeSalary(emp));
        }

        // 3. Получение общей стоимости всех выполненных работ сотрудника в соответствии с тарифами
        public double GetEmployeeSalaryByName(string name)
        {
            var employee = Employees.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            return employee != null ? CalculateEmployeeSalary(employee) : 0.0;
        }

        // 4. Получение имени работника, заработавшего максимальную сумму. 
        // Если таких клиентов несколько, получить имя первого в списке
        public string GetMaxEarnerName()
        {
            if (!Employees.Any()) return "Нет сотрудников";

            return Employees
                .Select(emp => new { emp.Name, Salary = CalculateEmployeeSalary(emp) })
                .OrderByDescending(e => e.Salary)
                .Select(e => e.Name)
                .First();
        }

        // 5. Получение количества работников, получивших больше определенной суммы (использовать Aggregate)
        public int GetCountOfEmployeesEarningMoreThan(double threshold)
        {
            return Employees.Aggregate(0, (count, emp) =>
                CalculateEmployeeSalary(emp) > threshold ? count + 1 : count);
        }

        // 6. Получение работником списка сумм, полученных по каждому виду работ (использовать GroupBy)
        public Dictionary<string, double> GetEmployeeEarningsByWorkType(Employee employee)
        {
            return employee.PerformedWorks
                .GroupBy(record => record.WorkType.Name)
                .ToDictionary(
                    group => group.Key,
                    group => group.Sum(record => record.WorkType.Rate * record.Quantity)
                );
        }
    }
}
