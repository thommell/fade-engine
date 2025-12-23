using System;
using System.Collections.Generic;
using fade_project.containers;
using fade_project.Core.Components.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Entities.Abstract;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Services.Derived;
    
public sealed class CollisionService : Service {
    private Scene _activeScene;
    private List<BoxCollider> _colliders = [];
    public override void LateLoad(SpriteBatch spriteBatch, ContentManager content) {
        _activeScene = ServiceManager.Instance.GetService<SceneService>().GetActiveScene();
        GetAllColliders();
    }

    public override void Update(GameTime gameTime) {
        CollisionCheck();
        base.Update(gameTime);
    }

    private void CollisionCheck() {
        if (_colliders.Count <= 1) return;
        for (int i = 0; i < _colliders.Count; i++) {
            for (int j = i + 1; j < _colliders.Count; j++) {
                bool result = AreObjectsColliding(_colliders[i], _colliders[j]);
                Console.WriteLine(result);
            }
        }
    }
    private bool AreObjectsColliding(BoxCollider coll1, BoxCollider coll2) {
        return coll1.Hitbox.Intersects(coll2.Hitbox);
    }

    private void GetAllColliders() => _colliders = _activeScene.GetObjectsOfType<BoxCollider>();
}