using Microsoft.Extensions.DependencyInjection;
using Task2_Modules.Core;
using Task2_Modules.Services;

namespace Task2_Modules.Modules;

public class ReportingModule : IModule
{
    public string Name => "Reporting";
    public IReadOnlyList<string> Dependencies => Array.Empty<string>();

    public void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IReportGenerator, SimpleReportGenerator>();
        Console.WriteLine($"[{Name}] Зарегистрирован IReportGenerator");
    }

    public Task InitializeAsync(IServiceProvider serviceProvider)
    {
        Console.WriteLine($"[{Name}] Инициализация завершена");
        return Task.CompletedTask;
    }
}