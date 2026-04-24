using System;
using System.Collections.Generic;
using fade_project.containers;
using fade_project.Core.Components.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Event;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core;

public class GameObject {
    private bool isEnabled;
    private readonly Dictionary<Type, FComponent> components = [];
    private Dictionary<Type, List<FComponent>> compInheritTree = [];
    private List<IDrawableComponent> drawableComponents = [];
    private List<IUpdateableComponent> updateableComponents = [];
    private List<IFixedUpdatableComponent> fixedUpdatableComponents = [];
    private Scene activeScene;
    
    public FTransform Transform { get; }
    public FadeEventCache Events { get; } = new();
    public Scene ActiveScene {
        get => activeScene;
    }

    public GameObject(FTransform transform = null, bool isEnabled = true, params FComponent[] components) {
        transform ??= new FTransform();
        this.Transform = transform;
        this.isEnabled = isEnabled;
        Initialize(components);
    }

    public void Load() {
        // Initialize first (Unity awake-like) -> "get" all components.
        foreach (KeyValuePair<Type, FComponent> component in components) {
            if (!isEnabled) break;
            component.Value.Initialize();
        }

        // Then "Load" all components (Start-like)
        foreach (KeyValuePair<Type, FComponent> component in components) {
            if (!isEnabled) break;
            component.Value.Load();
            component.Value.LateLoad();
        }
    }

    public void Draw(SpriteBatch spriteBatch) {
        for (int i = 0; i < drawableComponents.Count; i++) {
            if (!isEnabled) continue;
            drawableComponents[i].Draw(spriteBatch);
        }
    }

    public void Update(float deltaTime) {
        for (int i = 0; i < updateableComponents.Count; i++) {
            if (!isEnabled) continue;
            updateableComponents[i].Update(deltaTime);
        }
    }

    public void FixedUpdate(float fixedDeltaTime) {
        for (int i = 0; i < fixedUpdatableComponents.Count; i++) {
            if (!isEnabled) continue;
            fixedUpdatableComponents[i].FixedUpdate(fixedDeltaTime);
        }
    }

    // Returns matching T value given by user from the Component map.
    public T GetComponent<T>() where T : FComponent {
        Type component = typeof(T);

        if (components.TryGetValue(component, out var match)) {
            return (T)match;
        }

        return null;
    }

    /// <summary>
    /// Searches and returns different Component types.
    /// </summary>
    /// <typeparam name="T">Can only be FComponent or FadeComponent</typeparam>
    public List<T> GetComponents<T>() where T : FComponent
    {
        if (!compInheritTree.TryGetValue(typeof(T), out var list))
            return [];

        var result = new List<T>(list.Count);
        foreach (var c in list)
            result.Add((T)c);

        return result;
    }
    
    public void SetActiveScene(Scene scene) => activeScene = scene;

    // NOT the Component.Initialize call, this makes sure all early
    // added components are handled properly.
    private void Initialize(FComponent[] components) {
        AddComponent(Transform);

        for (int i = 0; i < components.Length; i++) {
            AddComponent(components[i]);
        }

        SetupInterfaces(components);
        SetupOwnership();
    }

    private void SetupInterfaces(FComponent[] components) {
        foreach (FComponent component in components) {
            if (component is IDrawableComponent drawableComponent)
                drawableComponents.Add(drawableComponent);
            if (component is IUpdateableComponent updateableComponent)
                updateableComponents.Add(updateableComponent);
            if (component is IFixedUpdatableComponent fixedUpdatableComponent)
                fixedUpdatableComponents.Add(fixedUpdatableComponent);
        }
    }

    private void SetupOwnership() {
        foreach (KeyValuePair<Type, FComponent> component in components) {
            component.Value.SetOwner(this);
        }
    }

    private void AddComponent(FComponent t = null) {
        if (t == null) return; // Logger?
        Type type = t.GetType();
        // Hardcoded to check type object as Component shouldn't have a BaseClass other than object.
        Type baseType = type.BaseType == typeof(object) ? typeof(FComponent) : t.GetType().BaseType;
        
        if (!compInheritTree.TryGetValue(baseType!, out List<FComponent> list)) {
            list = [];
            compInheritTree[baseType] = list;
        }        
        
        list.Add(t);
        components.Add(type, t);
    }
}