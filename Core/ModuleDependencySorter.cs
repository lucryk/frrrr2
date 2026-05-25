using System;
using System.Collections.Generic;
using System.Linq;

namespace Task2_Modules.Core;

public class ModuleDependencySorter
{
    public List<string> Sort(IEnumerable<IModule> modules)
    {
        var moduleDict = modules.ToDictionary(m => m.Name);
        var graph = new Dictionary<string, List<string>>();
        var inDegree = new Dictionary<string, int>();

        // Инициализация
        foreach (var mod in modules)
        {
            graph[mod.Name] = new List<string>();
            inDegree[mod.Name] = 0;
        }

        // Построение графа и подсчёт входящих степеней
        foreach (var mod in modules)
        {
            foreach (var dep in mod.Dependencies)
            {
                if (!moduleDict.ContainsKey(dep))
                    throw new InvalidOperationException($"Модуль '{mod.Name}' требует отсутствующий модуль '{dep}'");
                graph[dep].Add(mod.Name);
                inDegree[mod.Name]++;
            }
        }

        // Очередь для модулей без зависимостей (входящая степень 0)
        var queue = new Queue<string>(inDegree.Where(kv => kv.Value == 0).Select(kv => kv.Key));
        var result = new List<string>();

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            result.Add(current);

            foreach (var next in graph[current])
            {
                inDegree[next]--;
                if (inDegree[next] == 0)
                    queue.Enqueue(next);
            }
        }

        if (result.Count != modules.Count())
            throw new InvalidOperationException("Обнаружена циклическая зависимость между модулями");

        return result;
    }
}