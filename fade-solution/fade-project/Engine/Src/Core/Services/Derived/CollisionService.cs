using System;
using System.Collections.Generic;
using fade_project.containers;
using fade_project.Core.Components.BaseAbstract;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Services.Derived;
    
public sealed class CollisionService : Service {
    private Scene _activeScene;
    private List<BoxCollider> _colliders = [];
    private List<BoxCollider> _activeCollisions = [];
    public override void LateLoad(SpriteBatch spriteBatch, ContentManager content) {
        _activeScene = ServiceManager.Instance.GetService<SceneService>().GetActiveScene();
        GetAllColliders();
    }

    public override void Update(GameTime gameTime) {
        EnterCollisionCheck();
        ExitCollisionCheck();
        base.Update(gameTime);
    }

    private void EnterCollisionCheck() {
        if (_colliders.Count <= 1) return;
        for (int i = 0; i < _colliders.Count; i++) {
            BoxCollider coll1 = _colliders[i];
            for (int j = i + 1; j < _colliders.Count; j++) {
                BoxCollider coll2 = _colliders[j];
                
                if (coll1.IsColliding && coll2.IsColliding) {
                    continue;
                }
                
                if (AreObjectsColliding(coll1, coll2)) {
                    AddActiveCollision(coll1, coll2);
                };
            }
        }
    }

    private void ExitCollisionCheck() {
        if (_activeCollisions.Count <= 0 || _colliders.Count <= 1) return;
        for (int i = 0; i < _activeCollisions.Count; i++) {
            for (int j = i + 1; j < _activeCollisions.Count; j++) {
                BoxCollider coll1 = _colliders[i];
                BoxCollider coll2 = _colliders[j];
                if (!AreObjectsColliding(coll1, coll2)) {
                    RemoveActiveCollision(coll1, coll2);
                };
            }
        }
    }
    private bool AreObjectsColliding(BoxCollider coll1, BoxCollider coll2) {
        return coll1.Hitbox.Intersects(coll2.Hitbox);
    }

    private void AddActiveCollision(BoxCollider coll1, BoxCollider coll2) {
        _activeCollisions.Add(coll1);
        _activeCollisions.Add(coll2);
        
        coll1.SetCollidingStatus(true);
        coll2.SetCollidingStatus(true);
        
        coll1.OnCollisionEnter(coll2.Owner);
        coll2.OnCollisionEnter(coll1.Owner);
    }

    private void RemoveActiveCollision(BoxCollider coll1, BoxCollider coll2) {
        _activeCollisions.Remove(coll1);
        _activeCollisions.Remove(coll2);
        
        coll1.SetCollidingStatus(false);
        coll2.SetCollidingStatus(false);
        
        coll1.OnCollisionExit();
        coll2.OnCollisionExit();
    }

    private void GetAllColliders() => _colliders = _activeScene.GetObjectsOfType<BoxCollider>();
}