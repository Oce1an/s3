using Microsoft.Extensions.Configuration;
using SerializerLib;
using VoronovLR5.Domain;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string baseFileName = configuration["FileSettings:BaseFileName"] ?? "VoronovLR5";

string linqFile = $"{baseFileName}_linq.xml";
string xmlFile = $"{baseFileName}_xml.xml";
string jsonFile = $"{baseFileName}.json";

List<Firm> firms = new()
{
    new Firm("БургерКинг", "пр. Независимости, 56", new HRDepartment("Воронов1", 1)),
    new Firm("Макдональдс", "пр. Независимости, 23", new HRDepartment("Воронов2", 2)),
    new Firm("КФС", "пр. Победителей, 1", new HRDepartment("Воронов3", 3))
};

ISerializer serializer = new Serializer();

serializer.SerializeByLINQ(firms, linqFile);
serializer.SerializeXML(firms, xmlFile);
serializer.SerializeJSON(firms, jsonFile);

var fromLinq = serializer.DeSerializeByLINQ(linqFile).ToList();
var fromXml = serializer.DeSerializeXML(xmlFile).ToList();
var fromJson = serializer.DeSerializeJSON(jsonFile).ToList();

Console.WriteLine($"LINQ: {firms.SequenceEqual(fromLinq)}");
Console.WriteLine($"XML: {firms.SequenceEqual(fromXml)}");
Console.WriteLine($"JSON: {firms.SequenceEqual(fromJson)}");

foreach (var firm in fromJson)
{
    Console.WriteLine(firm);
}
