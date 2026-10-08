using System;
using Collections;
using Contracts;

namespace Entities
{
    public class SalaryDepartment : IPayrollSystem
    {
        public MyCustomCollection<WorkType> WorkTypes { get; set; }
        public MyCustomCollection<Employee> Employees { get; set; }

        public event EventHandler<PayrollList>? ListChanged;
        public event EventHandler<WorkPerformed>? WorkPerformed;

        public SalaryDepartment()
        {
            WorkTypes = new MyCustomCollection<WorkType>();
            Employees = new MyCustomCollection<Employee>();
        }

        public void AddWorkType(WorkType workType)
        {
            WorkTypes.Add(workType);
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

        public MyCustomCollection<Employee> GetEmployeesByWorkType(WorkType workType)
        {
            MyCustomCollection<Employee> result = new MyCustomCollection<Employee>();
            foreach (Employee emp in Employees)
            {
                foreach (WorkRecord record in emp.PerformedWorks)
                {
                    if (record.WorkType.Name == workType.Name)
                    {
                        result.Add(emp);
                        break;
                    }
                }
            }
            return result;
        }

        public double CalculateEmployeeSalary(Employee employee)
        {
            double total = 0;
            foreach (WorkRecord record in employee.PerformedWorks)
            {
                double cost = GenericMath.Multiply(record.WorkType.Rate, record.Quantity);
                total = GenericMath.Add(total, cost);
            }
            return total;
        }

        public double CalculateTotalPayroll()
        {
            double total = 0;
            foreach (Employee emp in Employees)
            {
                total = GenericMath.Add(total, CalculateEmployeeSalary(emp));
            }
            return total;
        }

        protected virtual void OnListChanged(PayrollList e)
        {
            ListChanged?.Invoke(this, e);
        }

        protected virtual void OnWorkPerformed(WorkPerformed e)
        {
            WorkPerformed?.Invoke(this, e);
        }
    }
}