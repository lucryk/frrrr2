using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Task2_Modules.Core;
using Task2_Modules.Modules;

var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false);
var configuration = builder.Build();

var availableModules = new IModule[]
{
    new ReportingModule(),
    new ExportModule(),
    new ValidationModule()
};

var loader = new ModuleLoader(configuration, availableModules);
List<IModule> modulesToLoad;
try
{
    modulesToLoad = loader.LoadFromConfig();
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка загрузки модулей: {ex.Message}");
    return;
}

var sorter = new ModuleDependencySorter();
List<string> orderedNames;
try
{
    orderedNames = sorter.Sort(modulesToLoad);
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка сортировки зависимостей: {ex.Message}");
    return;
}

var orderedModules = orderedNames.Select(name => modulesToLoad.First(m => m.Name == name)).ToList();

var services = new ServiceCollection();
foreach (var module in orderedModules)
{
    module.RegisterServices(services);
}
var serviceProvider = services.BuildServiceProvider();

foreach (var module in orderedModules)
{
    await module.InitializeAsync(serviceProvider);
}

Console.WriteLine("Приложение запущено. Нажмите любую клавишу...");
Console.ReadKey();