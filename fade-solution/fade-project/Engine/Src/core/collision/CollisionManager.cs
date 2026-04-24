using System.Collections.Generic;
using fade_project.containers;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Event.Types;
using fade_project.Engine.Core.Services.Derived.Collision;
using fade_project.systems;

namespace fade_project.Core.Services.Derived;
    
public static class CollisionManager {
    private static Scene activeScene;
    private static List<FCollider> colliders = [];
    private static readonly HashSet<CollisionPair> activeCollisions = [];
    public static void Load() {
        activeScene = SceneManager.ActiveScene;
        CacheAllColliders();
    }

    public static void FixedUpdate(float fixedDeltaTime) {
        UpdateCollisions();
    }

    public static void AddCollider(FCollider newCollider) {
        
    }

    public static void RemoveCollider(FCollider oldCollider) {
        
    }

    // This will get reworked to a more cpu-friendly design later, currently it's quite heavy
    private static void UpdateCollisions() {
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
                    if (wasColliding) {
                        OnCollisionStay(a.Owner, b.Owner);
                        continue;
                    };
                    
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

    private static bool AreObjectsColliding(CollisionPair pair) {
        return pair.ColliderA.Intersects(pair.ColliderB);
    }

    private static void OnCollisionEnter(GameObject other, GameObject self) {
        self.Events.Invoke(new CollisionEnterEvent(self: self, other: other));
        other.Events.Invoke(new CollisionEnterEvent(self: other, other: self));
        Logger.Log(typeof(CollisionManager), $"{self.GetType().Name} and {other.GetType().Name} have started colliding.", LogType.Debug);
    }
    
    private static void OnCollisionStay(GameObject other, GameObject self) {
        self.Events.Invoke(new CollisionStayEvent(self: self, other: other));
        other.Events.Invoke(new CollisionStayEvent(self: other, other: self));
    }

    private static void OnCollisionExit(GameObject other, GameObject self) {
        self.Events.Invoke(new CollisionExitEvent(self: self, other: other));
        other.Events.Invoke(new CollisionExitEvent(self: other, other: self));
        Logger.Log(typeof(CollisionManager), $"{self.GetType().Name} and {other.GetType().Name} have stopped colliding.", LogType.Debug);
    }

    private static void CacheAllColliders() =>
        colliders = activeScene.GetObjectsOfType<FCollider>();
}