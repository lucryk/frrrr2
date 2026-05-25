using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Task2_Modules.Core;

public class ModuleLoader
{
    private readonly IConfiguration _config;
    private readonly Dictionary<string, IModule> _availableModules;

    public ModuleLoader(IConfiguration config, IEnumerable<IModule> builtInModules)
    {
        _config = config;
        _availableModules = builtInModules.ToDictionary(m => m.Name);
    }

    public List<IModule> LoadFromConfig()
    {
        var moduleNames = _config.GetSection("Modules").Get<List<string>>();
        if (moduleNames == null || moduleNames.Count == 0)
            throw new InvalidOperationException("Список модулей в конфигурации пуст");

        var modules = new List<IModule>();
        foreach (var name in moduleNames)
        {
            if (!_availableModules.TryGetValue(name, out var module))
                throw new InvalidOperationException($"Модуль '{name}' не найден среди доступных модулей");
            modules.Add(module);
        }
        return modules;
    }
}