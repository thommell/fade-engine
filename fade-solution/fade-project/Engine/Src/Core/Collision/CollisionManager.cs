using System;
using System.Collections.Generic;
using fade_project.containers;
using fade_project.Core.Components.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Entities.Abstract;
using fade_project.Core.Event.Types;
using fade_project.Core.Services.Enums;
using fade_project.Engine.Core.Services.Derived.Collision;
using fade_project.systems;

namespace fade_project.Core.Services.Derived;
    
public sealed class CollisionManager : FComponent, IFixedUpdatableComponent {
    private Scene _activeScene;
    private List<FCollider> _colliders = [];
    private readonly HashSet<CollisionPair> _activeCollisions = [];
    public override void LateLoad() {
        _activeScene = ServiceManager.Instance.GetService<SceneService>().GetActiveScene();
        GetAllColliders();
    }

    public void FixedUpdate(float fixedDeltaTime) {
        UpdateCollisions();
    }

    private void UpdateCollisions() {
        if (_colliders.Count < 2) return;
        
        HashSet<CollisionPair> checkedPairs = [];

        for (int i = 0; i < _colliders.Count; i++) {
            FCollider a = _colliders[i];
            for (int j = i + 1; j < _colliders.Count; j++) {
                FCollider b = _colliders[j];
                var pair = new CollisionPair(a, b);
                bool isColliding = AreObjectsColliding(pair);
                bool wasColliding = _activeCollisions.Contains(pair);

                if (isColliding) {
                    checkedPairs.Add(pair);
                    if (wasColliding) continue;
                    
                    a.IsColliding = true;
                    b.IsColliding = true;
                    OnCollisionEnter(a.Owner, b.Owner);
                }
                else if (wasColliding) {
                    a.IsColliding = false;
                    b.IsColliding = false;
                    OnCollisionExit(a.Owner, b.Owner);
                }
            }
        }
        _activeCollisions.Clear();
        foreach (var pair in checkedPairs) {
            _activeCollisions.Add(pair);
        }
    }

    private bool AreObjectsColliding(CollisionPair pair) {
        return pair.ColliderA.Intersects(pair.ColliderB);
    }

    private void OnCollisionEnter(GameObject other, GameObject itself) {
        itself.Events.Invoke(new CollisionEnterEvent(self: itself, other: other));
        other.Events.Invoke(new CollisionEnterEvent(self: other, other: itself));
        this.Log(LogType.INFO, $"{itself.GetType().Name} and {other.GetType().Name} have started colliding.");
    }

    private void OnCollisionExit(GameObject other, GameObject self) {
        self.Events.Invoke(new CollisionExitEvent(self: self, other: other));
        other.Events.Invoke(new CollisionExitEvent(self: other, other: self));
        this.Log(LogType.INFO, $"{self.GetType().Name} and {other.GetType().Name} have stopped colliding.");
    }

    private void GetAllColliders() {
        _colliders = _activeScene.GetObjectsOfType<FCollider>();
    }
}