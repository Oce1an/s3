using Collections;

namespace Entities
{
    public class Employee
    {
        public string Name { get; set; }
        public MyCustomCollection<WorkRecord> PerformedWorks { get; set; }

        public Employee(string name)
        {
            Name = name;
            PerformedWorks = new MyCustomCollection<WorkRecord>();
        }

        public override string ToString() => Name;
    }
}