using Microsoft.Extensions.DependencyInjection;
using Task2_Modules.Core;
using Task2_Modules.Services;

namespace Task2_Modules.Modules;

public class ValidationModule : IModule
{
    public string Name => "Validation";
    public IReadOnlyList<string> Dependencies => Array.Empty<string>();

    public void RegisterServices(IServiceCollection services)
    {
        services.AddSingleton<IValidationRule, NonEmptyNameRule>();
        Console.WriteLine($"[{Name}] Зарегистрирован IValidationRule");
    }

    public Task InitializeAsync(IServiceProvider serviceProvider)
    {
        Console.WriteLine($"[{Name}] Инициализация завершена");
        return Task.CompletedTask;
    }
}