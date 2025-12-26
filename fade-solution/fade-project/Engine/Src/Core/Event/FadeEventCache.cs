using System;
using System.Collections.Generic;

namespace fade_project.Core.Event;

public sealed class FadeEventCache {
    private readonly Dictionary<Type, List<Delegate>> _eventsCache = [];
    public Dictionary<Type, List<Delegate>> EventsCache => _eventsCache;
    public void Listen<T>(Action<T> listener) where T : IFadeEvent {
        var type = typeof(T);
        if (!_eventsCache.TryGetValue(type, out var list)) {
            _eventsCache[type] = list = [];
        }
        list.Add(listener);
    }
    public void Invoke<T>(T parameters) where T : IFadeEvent {
        if (_eventsCache.TryGetValue(typeof(T), out var list)) {
            foreach (var del in list) {
                ((Action<T>)del)(parameters);
            }
        }
    }
}