using Etteplan;
using System.Xml;

internal class Program
{
    private static void Main(string[] args)
    {
        int attrId = 42007;

        string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            @"..\..\..\Data\sma_gentext.xml"));

        var targetUnit = GetTansUnitById(filePath, attrId);

        if (targetUnit == null)
            throw new Exception("ID not found!");
        else
            WriteTargetToFile(targetUnit);
    }

    static TransUnit? GetTansUnitById(string filePath, int id)
    {

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(filePath);

        var node = xmlDoc.SelectSingleNode($"//*[@id='{id}']");

        if (node == null)
        {
            return null;
        }

        TransUnit unit = new TransUnit
        {
            Id = id,
            Source = node.FirstChild.InnerText,
            Target = node.LastChild.InnerText,
        };

        return unit;
    }

    static void WriteTargetToFile(TransUnit unit)
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
