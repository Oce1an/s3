namespace GGGGGG_NNN_Lab2.Entities
{
    public class WorkType
    {
        public string Name { get; set; }
        public decimal RatePerHour { get; set; }

        public WorkType(string name, decimal rate)
        {
            Name = name;
            RatePerHour = rate;
        }

        public override string ToString()
        {
            return $"{Name} (Rate: {RatePerHour})";
        }
    }
}