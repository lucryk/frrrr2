namespace Task2_Modules.Services;

public interface IFileExporter
{
    void Export(string content, string filePath);
}

public class CsvFileExporter : IFileExporter
{
    public void Export(string content, string filePath)
        => File.WriteAllText(filePath, content);
}