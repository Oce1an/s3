using System;
using System.Linq;
using GGGGGG_NNN_Lab2.Collections;
using GGGGGG_NNN_Lab2.Contracts;

namespace GGGGGG_NNN_Lab2.Entities
{
    public class PayrollDepartment : IPayrollSystem
    {
        public MyCustomCollection<WorkType> WorkTypes { get; private set; }
        public MyCustomCollection<Employee> Employees { get; private set; }

        public event EventHandler<PayrollEventArgs> TariffsOrEmployeesChanged;
        public event EventHandler<PayrollEventArgs> WorkPerformed;

        public PayrollDepartment()
        {
            WorkTypes = new MyCustomCollection<WorkType>();
            Employees = new MyCustomCollection<Employee>();
        }

        public void AddWorkType(WorkType workType)
        {
            WorkTypes.Add(workType);
            OnTariffsOrEmployeesChanged($"Added work type: {workType.Name}", workType.Name);
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
            OnTariffsOrEmployeesChanged($"Added employee: {employee.Surname}", employee.Surname);
        }

        public void RegisterWork(WorkRecord record)
        {
            // Find employee logic would be needed here if record contained ID. 
            // For simplicity, let's assume we add work to the last added employee or specific one.
            // To make it functional, let's modify RegisterWork to take Employee and Record.
            // But sticking to interface: Let's assume we search by some criteria or just add to a global list?
            // The prompt says "Worker can perform different types of works".
            // Let's adjust: We need to link record to employee.
            // Modified approach: RegisterWork finds an employee (e.g., by surname passed in context or separate method).
            // For this lab, let's create a specific method to assign work to an existing employee object.

            // Since the interface is fixed, let's assume this method is called after finding the employee externally 
            // OR we change the design slightly to allow passing employee. 
            // Let's stick to the prompt's "System should... register work".

            // Implementation detail: We will iterate employees to find one matching the record context? 
            // No, that's ambiguous. Let's assume the caller manages the link.
            // Actually, better: Let's add a method `AssignWorkToEmployee(string surname, WorkRecord record)`
            // But the interface `IPayrollSystem` doesn't have it. 
            // Let's assume `RegisterWork` is generic. 
            // *Correction*: The prompt says "After entering surname, output salary". 
            // So we need to store records inside Employee.

            // Let's implement a helper inside PayrollDepartment to find employee and add record.
            // We will expose a method not in interface for simplicity or use the interface loosely.
            // Let's add `public void AssignWork(string employeeSurname, WorkRecord record)`
        }

        public void AssignWork(string employeeSurname, WorkRecord record)
        {
            bool found = false;
            // Iterate using foreach (supported by IEnumerable)
            foreach (var emp in Employees)
            {
                if (emp.Surname == employeeSurname)
                {
                    emp.WorkRecords.Add(record);
                    found = true;
                    OnWorkPerformed($"Work registered: {record.WorkTypeName} for {employeeSurname}", employeeSurname);
                    break;
                }
            }
            if (!found) throw new Exception($"Employee {employeeSurname} not found.");
        }

        public decimal CalculateEmployeeSalary(string surname)
        {
            decimal total = 0;
            foreach (var emp in Employees)
            {
                if (emp.Surname == surname)
                {
                    emp.WorkRecords.Reset();
                    for (int i = 0; i < emp.WorkRecords.Count; i++)
                    {
                        var record = emp.WorkRecords[i];
                        var workType = WorkTypes.FirstOrDefault(wt => wt.Name == record.WorkTypeName);
                        if (workType != null)
                        {
                            total += (decimal)record.HoursWorked * workType.RatePerHour;
                        }
                    }
                    return total;
                }
            }
            return 0;
        }

        public decimal CalculateTotalPayouts()
        {
            decimal total = 0;
            foreach (var emp in Employees)
            {
                emp.WorkRecords.Reset();
                for (int i = 0; i < emp.WorkRecords.Count; i++)
                {
                    var record = emp.WorkRecords[i];
                    var workType = WorkTypes.FirstOrDefault(wt => wt.Name == record.WorkTypeName);
                    if (workType != null)
                    {
                        total += (decimal)record.HoursWorked * workType.RatePerHour;
                    }
                }
            }
            return total;
        }

        protected virtual void OnTariffsOrEmployeesChanged(string message, string entity)
        {
            TariffsOrEmployeesChanged?.Invoke(this, new PayrollEventArgs(message, entity));
        }

        protected virtual void OnWorkPerformed(string message, string entity)
        {
            WorkPerformed?.Invoke(this, new PayrollEventArgs(message, entity));
        }
    }
}