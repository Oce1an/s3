using GGGGGG_NNN_Lab2.Entities;

namespace GGGGGG_NNN_Lab2.Contracts
{
    public interface IPayrollSystem
    {
        void AddWorkType(WorkType workType);
        void AddEmployee(Employee employee);
        void RegisterWork(WorkRecord record);
        decimal CalculateEmployeeSalary(string surname);
        decimal CalculateTotalPayouts();

        // Events
        event EventHandler<PayrollEventArgs> TariffsOrEmployeesChanged;
        event EventHandler<PayrollEventArgs> WorkPerformed;
    }
}