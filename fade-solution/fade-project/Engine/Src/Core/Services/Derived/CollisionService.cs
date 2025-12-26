using System.Buffers;
using System.Collections.Generic;
using fade_project.containers;
using fade_project.Core.Components.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Entities.Abstract;
using fade_project.Core.Event.Types;
using fade_project.Core.Services.Enums;
using fade_project.Engine.Core.Services.Derived.Collision;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Services.Derived;
    
public sealed class CollisionService : Service {
    private Scene _activeScene;
    private List<BoxCollider> _colliders = [];
    private readonly HashSet<ActiveCollisionPair> _activeCollisions = [];
    public override void LateLoad(SpriteBatch spriteBatch, ContentManager content) {
        _activeScene = ServiceManager.Instance.GetService<SceneService>().GetActiveScene();
        GetAllColliders();
    }

    public override void Update(GameTime gameTime) {
        UpdateCollisions();
        base.Update(gameTime);
    }

    private void UpdateCollisions() {
        if (_colliders.Count < 2) return;
        
        HashSet<ActiveCollisionPair> checkedPairs = new HashSet<ActiveCollisionPair>();

        for (int i = 0; i < _colliders.Count; i++) {
            BoxCollider a = _colliders[i];
            for (int j = i + 1; j < _colliders.Count; j++) {
                BoxCollider b = _colliders[j];
                var pair = new ActiveCollisionPair(a, b);
                bool isColliding = AreObjectsColliding(a, b);
                bool wasColliding = _activeCollisions.Contains(pair);

                if (isColliding) {
                    checkedPairs.Add(pair);
                    if (!wasColliding) {
                        OnCollisionEnter(a.Owner, b.Owner);
                    }
                }
                else if (wasColliding) {
                    OnCollisionExit(a.Owner, b.Owner);
                }
            }
        }
        _activeCollisions.Clear();
        foreach (var pair in checkedPairs) {
            _activeCollisions.Add(pair);
        }
    }

    private bool AreObjectsColliding(BoxCollider coll1, BoxCollider coll2) {
        return coll1.Hitbox.Intersects(coll2.Hitbox);
    }

    private void OnCollisionEnter(GameObject other, GameObject itself) {
        itself.Events.Invoke(new CollisionEnterEvent(self: itself, other: other));
        other.Events.Invoke(new CollisionEnterEvent(self: itself, other: other));
        Logger.Log(LogType.INFO, $"{itself.GetType().Name} and {other.GetType().Name} have started colliding.");
    }

    private void OnCollisionExit(GameObject other, GameObject self) {
        self.Events.Invoke(new CollisionExitEvent(self: self, other: other));
        other.Events.Invoke(new CollisionExitEvent(self: self, other: other));
        Logger.Log(LogType.INFO, $"{self.GetType().Name} and {other.GetType().Name} have stopped colliding.");
    }

    private void GetAllColliders() => _colliders = _activeScene.GetObjectsOfType<BoxCollider>();
}