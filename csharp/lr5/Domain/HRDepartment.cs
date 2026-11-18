namespace VoronovLR5.Domain
{
    public class HRDepartment : IEquatable<HRDepartment>
    {
        public string HeadName { get; set; } = string.Empty;
        public int StaffCount { get; set; }

        public HRDepartment() { }

        public HRDepartment(string headName, int staffCount)
        {
            HeadName = headName;
            StaffCount = staffCount;
        }

        public bool Equals(HRDepartment? other)
        {
            if (other is null) return false;
            return HeadName == other.HeadName && StaffCount == other.StaffCount;
        }

        public override bool Equals(object? obj) => Equals(obj as HRDepartment);

        public override int GetHashCode() => HashCode.Combine(HeadName, StaffCount);

        public override string ToString() => $"Начальник: {HeadName}, Сотрудников: {StaffCount}";
    }
}
