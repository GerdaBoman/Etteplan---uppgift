using Etteplan;
using System.Xml;

internal class Program
{
    private static void Main(string[] args)
    {
        int attrId = 42007;

        string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            @"..\..\..\Data\sma_gentext.xml"));

        var targetUnit = GetTargetById(filePath, attrId);

        WriteToFile(targetUnit);
    }

    static UnitEntity GetTargetById(string filePath, int id)
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);

        var node = xmlDoc.SelectSingleNode($"//*[@id='{id}']");


        UnitEntity unit = new UnitEntity
        {
            Id = id,
            Source = node.FirstChild.InnerText,
            Target = node.LastChild.InnerText,
        };

        return unit;
    }

    static void WriteToFile(UnitEntity unit)
    {
        var filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var resultPath = Path.Combine(filePath, "Result.txt");

        if (!File.Exists(resultPath))
        {
            File.Create(resultPath);

        }

        using (StreamWriter writer = new(resultPath))
        {
            writer.WriteLine(unit.Target);
            writer.Close();
        };

        Console.Write("Completed : Result.txt file is saved on your local desktop");
    }

}
