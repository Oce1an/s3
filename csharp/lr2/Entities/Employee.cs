using GGGGGG_NNN_Lab2.Collections;

namespace GGGGGG_NNN_Lab2.Entities
{
    public class Employee
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        // Using custom collection for work records as per Lab 1 requirement
        public MyCustomCollection<WorkRecord> WorkRecords { get; set; }

        public Employee(string surname, string firstName)
        {
            Surname = surname;
            FirstName = firstName;
            WorkRecords = new MyCustomCollection<WorkRecord>();
        }

        public override string ToString()
        {
            return $"{Surname} {FirstName}";
        }
    }
}