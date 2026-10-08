namespace Entities
{
    public class WorkRecord
    {
        public WorkType WorkType { get; set; }
        public double Quantity { get; set; }

        public WorkRecord(WorkType workType, double quantity)
        {
            WorkType = workType;
            Quantity = quantity;
        }
    }
}