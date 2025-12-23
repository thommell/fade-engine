using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Event;

public enum FadeEventType {
    KeyboardInput = 0,
    MouseInput
}
public static class FadeEvent {
    private static Dictionary<FadeEventType, Queue<Action>> _activeEvents = [];

    public static void Invoke(FadeEventType type) {
        _activeEvents.TryGetValue(type, out var queue);
        if (queue is not { Count: > 0 }) {
            throw new InvalidOperationException(
                "There are no callbacks within this queue type (or queue is null)");
        }

        foreach (Action action in queue) {
            action.Invoke();
        }
    }
    
    public static void Attach(FadeEventType type, Action callback) {
        _activeEvents.TryGetValue(type, out var queue);
        if (queue == null) {
            CreateEvent(type, callback);
        }
        else {
            EnqueueEvent(type, callback);
        }
    }
    
    public static void Detach(FadeEventType type) {
        if (!_activeEvents.TryGetValue(type, out var queue)) return;
        _activeEvents[type] = null;
    }

    private static void CreateEvent(FadeEventType eventType, Action callback) {
        _activeEvents[eventType] = new Queue<Action>();
        EnqueueEvent(eventType, callback);
    }
    private static void EnqueueEvent(FadeEventType eventType, Action callback) {
        _activeEvents[eventType].Enqueue(callback);
    }
}