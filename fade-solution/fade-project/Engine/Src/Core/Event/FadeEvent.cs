using System;
using System.Collections.Generic;
using System.Linq;

namespace fade_project.Core.Event;

public sealed class FadeEvent {
    private Queue<Action> _eventQueue = [];
    private bool _isInvoking = false;
    
    public Queue<Action> EventQueue => _eventQueue;
    public bool IsInvoking => _isInvoking;

    public void Invoke() {
        if (_eventQueue is not { Count: > 0 }) {
            Console.WriteLine("Event has no attached callbacks or is null.");
            return;
        }

        ChangeStatus(true);
        foreach (Action action in _eventQueue) {
            action.Invoke();
        }
        ChangeStatus(false);
    }

    public void AttachListener(Action callback) {
        _eventQueue.Enqueue(callback);
    }
    public void Detach(Action callback) {
        _eventQueue.ToList().Remove(callback);
    }

    public void Destroy() {
        _eventQueue.Clear();
        _eventQueue = null;
    }
    private void ChangeStatus(bool invokingStatus) =>  _isInvoking = invokingStatus;
}