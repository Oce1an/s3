namespace VoronovLR5.Domain
{
    public class Firm : IEquatable<Firm>
    {
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public HRDepartment HR { get; set; } = new();

        public Firm() { }

        public Firm(string name, string address, HRDepartment hr)
        {
            Name = name;
            Address = address;
            HR = hr;
        }

        public bool Equals(Firm? other)
        {
            if (other is null) return false;
            return Name == other.Name &&
                   Address == other.Address &&
                   Equals(HR, other.HR);
        }

        public override bool Equals(object? obj) => Equals(obj as Firm);

        public override int GetHashCode() => HashCode.Combine(Name, Address, HR);

        public override string ToString() => $"\"{Name}\" ({Address}) | {HR}";
    }
}
