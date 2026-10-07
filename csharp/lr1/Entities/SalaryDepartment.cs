using Collections;
using Contracts;

namespace Entities
{
    public class SalaryDepartment : IPayrollSystem
    {
        public MyCustomCollection<WorkType> WorkTypes { get; set; }
        public MyCustomCollection<Employee> Employees { get; set; }

        public SalaryDepartment()
        {
            WorkTypes = new MyCustomCollection<WorkType>();
            Employees = new MyCustomCollection<Employee>();
        }

        public void AddWorkType(WorkType workType) => WorkTypes.Add(workType);
        public void AddEmployee(Employee employee) => Employees.Add(employee);

        public void AssignWork(Employee employee, WorkType workType, double quantity)
        {
            employee.PerformedWorks.Add(new WorkRecord(workType, quantity));
        }

        public MyCustomCollection<Employee> GetEmployeesByWorkType(WorkType workType)
        {
            MyCustomCollection<Employee> result = new MyCustomCollection<Employee>();
            for (int i = 0; i < Employees.Count; i++)
            {
                Employee emp = Employees[i];
                for (int j = 0; j < emp.PerformedWorks.Count; j++)
                {
                    if (emp.PerformedWorks[j].WorkType.Name == workType.Name)
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
            for (int i = 0; i < employee.PerformedWorks.Count; i++)
            {
                var record = employee.PerformedWorks[i];
                double cost = GenericMathHelper.Multiply(record.WorkType.Rate, record.Quantity);
                total = GenericMathHelper.Add(total, cost);
            }
            return total;
        }

        public double CalculateTotalPayroll()
        {
            double total = 0;
            for (int i = 0; i < Employees.Count; i++)
            {
                total = GenericMathHelper.Add(total, CalculateEmployeeSalary(Employees[i]));
            }
            return total;
        }
    }
}