using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fade_project.Core;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.containers;

public abstract class Scene {
    private List<GameObject> objectsInScene = [];
    private List<GameObject> objectsToAdd = [];
    private List<GameObject> objectsToRemove = [];

    private Dictionary<GameObject, FCollider> colliders = [];

    //TODO:
    // It is dangerous to blindly remove objects during iteration,
    // We want to do this after iteration so that it is stable.

    private bool isLoaded;
    
    private bool IsAddingObjects => objectsInScene.Count > 0;

    public virtual void OnEnter() {
        AddObjectsToScene();
        for (int i = 0; i < objectsInScene.Count; i++) {
            objectsInScene[i].Load();
        }

        isLoaded = true;
    }
    
    public virtual void Draw(SpriteBatch spriteBatch) {
        for (int i = 0; i < objectsInScene.Count; i++) {
            objectsInScene[i].Draw(spriteBatch);
        }
    }

    public virtual void Update(float deltaTime) {
        // Check if there are new objects waiting to be added
        if (IsAddingObjects) {
            AddObjectsToScene();
        }
        
        for (int i = 0; i < objectsInScene.Count; i++) {
            objectsInScene[i].Update(deltaTime);
        }
    }

    public void FixedUpdate(float fixedDeltaTime) {
        for (int i = 0; i < objectsInScene.Count; i++) {
            objectsInScene[i].FixedUpdate(fixedDeltaTime);
        }
    }
    
    public virtual void OnExit() {}

    public List<T> GetObjectsOfType<T>() where T : FComponent {
        ConcurrentBag<T> objects = [];
        Parallel.ForEach(objectsInScene, obj => {
            List<T> t = obj.GetComponents<T>();
            if (t.Count <= 0) return;
            foreach (var comp in t) {
                objects.Add(comp);
            }
        });
        if (objects.IsEmpty) 
            Logger.Log(this, $"No object in {this.GetType().Name} has a single component of {typeof(T).Name}", LogType.Warn); 
            
        return objects.ToList();
    }
    
    protected void AddObject(GameObject obj) {
        if (obj == null) return;
        if (isLoaded) {
            //TODO:
            // Dynamic initialize/load object during runtime, right now it's only
            // doing this before a scene is loaded causing objects added
            // after to fail
            return;
        }

        objectsToAdd.Add(obj);
    }

    private void AddObjectsToScene() {
        foreach (GameObject obj in objectsToAdd) {
            obj.SetActiveScene(this);
        }
        
        objectsInScene.AddRange(objectsToAdd);
        objectsToAdd.Clear();
    }
}