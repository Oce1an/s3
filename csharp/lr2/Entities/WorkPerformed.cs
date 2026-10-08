using System;

namespace Entities
{
    public class WorkPerformed : EventArgs
    {
        public string EmployeeName { get; set; }
        public string WorkTypeName { get; set; }
        public double Quantity { get; set; }

        public WorkPerformed(string employeeName, string workTypeName, double quantity)
        {
            EmployeeName = employeeName;
            WorkTypeName = workTypeName;
            Quantity = quantity;
        }

        public override string ToString() =>
            $"Работник '{EmployeeName}' выполнял работу '{WorkTypeName}' " +
            $"на протяжении {Quantity} часов в день.";
    }
}