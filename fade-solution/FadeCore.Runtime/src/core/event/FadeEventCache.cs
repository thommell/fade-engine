using System;
using System.Collections.Generic;

namespace FadeCore.Runtime;

public sealed class FadeEventCache {
    private readonly Dictionary<Type, List<Delegate>> eventsCache = [];
    public void Listen<T>(Action<T> listener) where T : IFadeEvent {
        var type = typeof(T);
        if (!eventsCache.TryGetValue(type, out var list)) {
            eventsCache[type] = list = [];
        }
        list.Add(listener);
    }
    public void Invoke<T>(T obj) where T : IFadeEvent {
        if (!eventsCache.TryGetValue(typeof(T), out var list)) return;
        foreach (var del in list) {
            ((Action<T>)del)(obj);
        }
    }
}