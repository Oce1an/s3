using System.Collections.Generic;
using Entities;

namespace Contracts
{
    public interface IPayrollSystem
    {
        void AddWorkType(WorkType workType);
        void AddEmployee(Employee employee);
        void AssignWork(Employee employee, WorkType workType, double quantity);
        List<string> GetSortedWorkTypeNames();
        double GetTotalPayroll();
        double GetEmployeeSalaryByName(string name);
        string GetMaxEarnerName();
        int GetCountOfEmployeesEarningMoreThan(double threshold);
        Dictionary<string, double> GetEmployeeEarningsByWorkType(Employee employee);
    }
}
