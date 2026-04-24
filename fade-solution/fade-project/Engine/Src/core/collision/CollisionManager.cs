using System.Collections.Generic;
using fade_project.containers;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Event.Types;
using fade_project.Engine.Core.Services.Derived.Collision;
using fade_project.systems;

namespace fade_project.Core.Services.Derived;
    
public sealed class CollisionManager : FComponent, IFixedUpdatableComponent {
    private Scene activeScene;
    private List<FCollider> colliders = [];
    private readonly HashSet<CollisionPair> activeCollisions = [];
    public override void LateLoad() {
        activeScene = ServiceManager.Instance.GetService<SceneService>().GetActiveScene();
        CacheAllColliders();
    }

    public void FixedUpdate(float fixedDeltaTime) {
        UpdateCollisions();
    }

    // This will get reworked to a more cpu-friendly design later, currently it's quite heavy
    private void UpdateCollisions() {
        if (colliders.Count < 2) return;
        
        HashSet<CollisionPair> checkedPairs = [];

        for (int i = 0; i < colliders.Count; i++) {
            FCollider a = colliders[i];
            for (int j = i + 1; j < colliders.Count; j++) {
                FCollider b = colliders[j];
                var pair = new CollisionPair(a, b);
                bool isColliding = AreObjectsColliding(pair);
                bool wasColliding = activeCollisions.Contains(pair);

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
        activeCollisions.Clear();
        foreach (var pair in checkedPairs) {
            activeCollisions.Add(pair);
        }
    }

    private bool AreObjectsColliding(CollisionPair pair) {
        return pair.ColliderA.Intersects(pair.ColliderB);
    }

    private void OnCollisionEnter(GameObject other, GameObject itself) {
        itself.Events.Invoke(new CollisionEnterEvent(self: itself, other: other));
        other.Events.Invoke(new CollisionEnterEvent(self: other, other: itself));
        this.Log(LogType.Info, $"{itself.GetType().Name} and {other.GetType().Name} have started colliding.");
    }

    private void OnCollisionExit(GameObject other, GameObject self) {
        self.Events.Invoke(new CollisionExitEvent(self: self, other: other));
        other.Events.Invoke(new CollisionExitEvent(self: other, other: self));
        this.Log(LogType.Info, $"{self.GetType().Name} and {other.GetType().Name} have stopped colliding.");
    }

    private void CacheAllColliders() =>
        colliders = activeScene.GetObjectsOfType<FCollider>();
}