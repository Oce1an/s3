using System.Text.Encodings.Web;
using System.Text.Json;
using System.Xml.Linq;
using System.Xml.Serialization;
using VoronovLR5.Domain;

namespace SerializerLib
{
    public class Serializer : ISerializer
    {
        public void SerializeByLINQ(IEnumerable<Firm> xxx, string fileName)
        {
            XDocument doc = new XDocument(
                new XElement("Firms",
                    xxx.Select(f => new XElement("Firm",
                        new XAttribute("Name", f.Name),
                        new XElement("Address", f.Address),
                        new XElement("HRDepartment",
                            new XElement("HeadName", f.HR.HeadName),
                            new XElement("StaffCount", f.HR.StaffCount)
                        )
                    ))
                )
            );
            doc.Save(fileName);
        }

        public IEnumerable<Firm> DeSerializeByLINQ(string fileName)
        {
            XDocument doc = XDocument.Load(fileName);
            var firms = doc.Root?
                .Elements("Firm")
                .Select(x => new Firm
                {
                    Name = (string?)x.Attribute("Name") ?? string.Empty,
                    Address = (string?)x.Element("Address") ?? string.Empty,
                    HR = new HRDepartment
                    {
                        HeadName = (string?)x.Element("HRDepartment")?.Element("HeadName") ?? string.Empty,
                        StaffCount = (int?)x.Element("HRDepartment")?.Element("StaffCount") ?? 0
                    }
                })
                .ToList();

            return firms ?? Enumerable.Empty<Firm>();
        }

        public void SerializeXML(IEnumerable<Firm> xxx, string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Firm>));
            using FileStream fs = new FileStream(fileName, FileMode.Create);
            serializer.Serialize(fs, xxx.ToList());
        }

        public IEnumerable<Firm> DeSerializeXML(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Firm>));
            using FileStream fs = new FileStream(fileName, FileMode.Open);
            return (List<Firm>?)serializer.Deserialize(fs) ?? Enumerable.Empty<Firm>();
        }

        public void SerializeJSON(IEnumerable<Firm> xxx, string fileName)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            string json = JsonSerializer.Serialize(xxx, options);
            File.WriteAllText(fileName, json);
        }

        public IEnumerable<Firm> DeSerializeJSON(string fileName)
        {
            string json = File.ReadAllText(fileName);
            return JsonSerializer.Deserialize<List<Firm>>(json) ?? Enumerable.Empty<Firm>();
        }
    }
}
