using System.Collections.Generic;

namespace Entities
{
    public class Employee
    {
        public string Name { get; set; }
        public List<WorkRecord> PerformedWorks { get; set; }

        public Employee(string name)
        {
            Name = name;
            PerformedWorks = new List<WorkRecord>();
        }

        public override string ToString() => Name;
    }
}
