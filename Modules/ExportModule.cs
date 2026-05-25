using Microsoft.Extensions.DependencyInjection;
using Task2_Modules.Core;
using Task2_Modules.Services;

namespace Task2_Modules.Modules;

public class ExportModule : IModule
{
    public string Name => "Export";
    public IReadOnlyList<string> Dependencies => new[] { "Reporting" };

    public void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IFileExporter, CsvFileExporter>();
        Console.WriteLine($"[{Name}] Зарегистрирован IFileExporter");
    }

    public Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var reportGen = serviceProvider.GetRequiredService<IReportGenerator>();
        var exporter = serviceProvider.GetRequiredService<IFileExporter>();
        var report = reportGen.GenerateReport();
        exporter.Export(report, "export_report.txt");
        Console.WriteLine($"[{Name}] Экспорт выполнен в файл export_report.txt");
        return Task.CompletedTask;
    }
}