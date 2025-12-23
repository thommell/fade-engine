using System;
using System.Collections.Generic;
using fade_project.Core.Entities.Abstract;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.containers;

public abstract class Scene {
    private List<GameObject> _objectsInScene = [];
    private List<GameObject> _objectsToAdd = [];
    private List<GameObject> _objectsToRemove = []; 
    //TODO:
    // It is dangerous to blindly remove objects during iteration,
    // We want to do this after iteration so that it is stable.

    protected bool IsLoaded;
    
    private bool IsAddingObjects => _objectsInScene.Count > 0;

    public virtual void OnEnter() {
        AddObjectsToScene();
        for (int i = 0; i < _objectsInScene.Count; i++) {
            _objectsInScene[i].Load();
        }

        IsLoaded = true;
    }
    
    public virtual void Draw(SpriteBatch spriteBatch) {
        for (int i = 0; i < _objectsInScene.Count; i++) {
            _objectsInScene[i].Draw(spriteBatch);
        }
    }

    public virtual void Update(GameTime gameTime) {
        // Check if there are new objects waiting to be added
        if (IsAddingObjects) {
            AddObjectsToScene();
        }
        
        for (int i = 0; i < _objectsInScene.Count; i++) {
            _objectsInScene[i].Update(gameTime);
        }
    }
    
    // Destroy later important systems here
    public virtual void OnExit() {}
    
    protected void AddObject(GameObject obj) {
        if (obj == null) return;
        if (IsLoaded) {
            //TODO:
            // Dynamic initialize/load object during runtime, right now it's only
            // doing this before a scene is loaded causing objects added
            // after to fail
            return;
        }
        _objectsToAdd.Add(obj);
    }

    private void AddObjectsToScene() {
        _objectsInScene.AddRange(_objectsToAdd);
        _objectsToAdd.Clear();
    }
}