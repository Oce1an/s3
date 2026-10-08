namespace Entities
{
    public class WorkType
    {
        public string Name { get; set; }
        public double Rate { get; set; }

        public WorkType(string name, double rate)
        {
            Name = name;
            Rate = rate;
        }

        public override string ToString() => Name;
    }
}