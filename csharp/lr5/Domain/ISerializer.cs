namespace VoronovLR5.Domain
{
    public interface ISerializer
    {
        IEnumerable<Firm> DeSerializeByLINQ(string fileName);
        IEnumerable<Firm> DeSerializeXML(string fileName);
        IEnumerable<Firm> DeSerializeJSON(string fileName);
        void SerializeByLINQ(IEnumerable<Firm> xxx, string fileName);
        void SerializeXML(IEnumerable<Firm> xxx, string fileName);
        void SerializeJSON(IEnumerable<Firm> xxx, string fileName);
    }
}
