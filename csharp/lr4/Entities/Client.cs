namespace Lab4.Entities
{
    public class Client
    {
        public string Name { get; set; } = string.Empty; // Исправление CS8618
        public int Age { get; set; }
        public int Discount { get; set; }
        public bool IsCorporate { get; set; }

        public override string ToString() =>
            $"{Name,-15} | Возраст: {Age,2} | Скидка: {Discount,2}% | Юр. лицо: {IsCorporate}";
    }
}