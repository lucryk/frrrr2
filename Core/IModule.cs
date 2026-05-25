using Microsoft.Extensions.DependencyInjection;

namespace Task2_Modules.Core;

public interface IModule
{
    string Name { get; }
    IReadOnlyList<string> Dependencies { get; }
    
    void RegisterServices(IServiceCollection services);
    Task InitializeAsync(IServiceProvider serviceProvider);
}