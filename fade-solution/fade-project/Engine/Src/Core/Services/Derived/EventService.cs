using System;
using System.Collections.Generic;
using fade_project.Core.Services;

namespace fade_project.Core.Event;

/// <summary>
/// Service for managing all the events in the engine.
/// </summary>
public sealed class EventService : Service {
    private Dictionary<FadeEventType, FadeEvent> _events = [];

    public void Listen(FadeEventType type, Action callback) {
        if (_events.TryGetValue(type, out FadeEvent value)) {
            AddListener(value, callback);
        }
        else {
            Console.Error.WriteLine("This EventType has no attached FadeEvents or is null.");
        }
    }

    public void Remove(FadeEventType type, Action callback = null) {
        if (_events.TryGetValue(type, out FadeEvent value)) {
            value.Detach(callback);
        }
    }
    
    public void InvokeEvent(FadeEventType type) {
        if (!_events.TryGetValue(type, out FadeEvent value) || value.IsInvoking) return;
        value.Invoke();
    }

    public void CreateEvent(FadeEventType type, Action callback = null) {
        if (_events.TryGetValue(type, out FadeEvent value)) {
            return;
        }
        value = new FadeEvent();
        _events[type] = value;
        if (callback != null) {
            AddListener(value, callback);
        }
    }

    private void AddListener(FadeEvent fadeEvent, Action callback) {
        fadeEvent.AttachListener(callback);
    }
}