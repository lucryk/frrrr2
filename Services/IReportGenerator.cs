namespace Task2_Modules.Services;

public interface IReportGenerator
{
    string GenerateReport();
}

public class SimpleReportGenerator : IReportGenerator
{
    public string GenerateReport() => "Отчёт: все данные в порядке (пример)";
}