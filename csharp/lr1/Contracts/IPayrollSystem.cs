using Collections;
using Entities;

namespace Contracts
{
    public interface IPayrollSystem
    {
        void AddWorkType(WorkType workType);
        void AddEmployee(Employee employee);
        void AssightWork(Employee employee, WorkType workType, double quantity);
        MyCustomCollection<Employee> GetEmployeesByWorkType(WorkType workType);
        double CalculateEmployeeSalary(Employee employee);
        double CalculateTotalPayroll();
    }
}