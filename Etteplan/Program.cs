using Etteplan;
using System.Xml;

int attrId = 42007;

string filePath = @"C:\Users\gerda\source\repos\Etteplan\Etteplan\Data\sma_gentext.xml";


XmlDocument xmlDoc = new XmlDocument();
xmlDoc.Load(filePath);

var node = xmlDoc.SelectSingleNode($"//*[@id='{attrId}']");



UnitEntity unit = new UnitEntity
{
    Id = attrId,
    Source = node.FirstChild.InnerText,
    Target = node.LastChild.InnerText,
};

Console.WriteLine(unit.Target);