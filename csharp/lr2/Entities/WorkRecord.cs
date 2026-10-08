using System;

namespace GGGGGG_NNN_Lab2.Entities
{
    public class WorkRecord
    {
        public string WorkTypeName { get; set; }
        public double HoursWorked { get; set; }
        public DateTime Date { get; set; }

        public WorkRecord(string workTypeName, double hours, DateTime date)
        {
            WorkTypeName = workTypeName;
            HoursWorked = hours;
            Date = date;
        }
    }
}