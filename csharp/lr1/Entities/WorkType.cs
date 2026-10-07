namespace Entities
{
    public class WorkType
    {
        public string Name { get; set; }
        public double Rate { get; set; }

        public WorkType(string Name, double Rate)
        {
            Name = name;
            Rate = Rate;
        }

        public override string ToString() => Name;
    }
}