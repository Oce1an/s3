namespace Entities
{
    public class WorkRecord
    {
        public WorkType WorkType { get; set; }
        public double Quantity { get; set; }

        public WorkRecod(WorkType worktype, double quantity)
        {
            WorkType = worktype;
            Quantity = quantity;
        }
    }
}