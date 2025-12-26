using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using fade_project.Core.Components.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Entities.Abstract;

public class GameObject {
    private bool _isEnabled;

    private Dictionary<Type, Component> _components = [];
    private Dictionary<Type, List<Component>> _compInheritTree = [];
    private List<IDrawableComponent> _drawableComponents = [];
    private List<IUpdateableComponent> _updateableComponents = [];
    private Transform _transform;
    private readonly FadeEventCache _fadeEventCache = new();
    public Transform Transform => _transform;
    public FadeEventCache Events => _fadeEventCache;

    public GameObject(Transform transform = null, bool isEnabled = true, params Component[] components) {
        transform ??= new Transform();
        _transform = transform;
        _isEnabled = isEnabled;
        Initialize(components);
    }

    public void Load() {
        // Initialize first (Unity awake-like) -> "get" all components, then Load (Unity's Start)
        foreach (KeyValuePair<Type, Component> component in _components) {
            if (!_isEnabled) break;
            component.Value.Initialize();
        }

        foreach (KeyValuePair<Type, Component> component in _components) {
            if (!_isEnabled) break;
            component.Value.Load();
        }
    }

    public void Draw(SpriteBatch spriteBatch) {
        for (int i = 0; i < _drawableComponents.Count; i++) {
            if (!_isEnabled) continue;
            _drawableComponents[i].Draw(spriteBatch);
        }
    }

    public void Update(GameTime gameTime) {
        for (int i = 0; i < _updateableComponents.Count; i++) {
            if (!_isEnabled) continue;
            _updateableComponents[i].Update(gameTime);
        }
    }

    // Returns matching T value given by user from the Component map.
    public T GetComponent<T>() where T : Component {
        Type component = typeof(T);
        
        if (_components.TryGetValue(component, out var match)) {
            return (T)match;
        }

        return null;
    }

    /// <summary>
    /// Searches and returns different Component types.
    /// </summary>
    /// <typeparam name="T">Can only be Component or FadeComponent</typeparam>
    public List<T> GetComponents<T>() where T : Component
    {
        if (!_compInheritTree.TryGetValue(typeof(T), out var list))
            return [];

        var result = new List<T>(list.Count);
        foreach (var c in list)
            result.Add((T)c);

        return result;
    }



    // NOT the Component.Initialize call, this makes sure all early
    // added components are handled properly.
    private void Initialize(Component[] components) {
        AddComponent(_transform);

        for (int i = 0; i < components.Length; i++) {
            AddComponent(components[i]);
        }

        SetupInterfaces(components);
        SetupOwnership();
    }

    private void SetupInterfaces(Component[] components) {
        foreach (Component component in components) {
            if (component is IDrawableComponent drawableComponent)
                _drawableComponents.Add(drawableComponent);
            if (component is IUpdateableComponent updateableComponent)
                _updateableComponents.Add(updateableComponent);
        }
    }

    private void SetupOwnership() {
        foreach (KeyValuePair<Type, Component> component in _components) {
            component.Value.SetOwner(this);
        }
    }

    private void AddComponent(Component t = null) {
        if (t == null) return; // Logger?
        Type type = t.GetType();
        // Hardcoded to check type object as Component shouldn't have a BaseClass other than object.
        Type baseType = type.BaseType == typeof(object) ? typeof(Component) : t.GetType().BaseType;
        
        if (!_compInheritTree.TryGetValue(baseType!, out List<Component> list)) {
            list = [];
            _compInheritTree[baseType] = list;
        }        
        
        list.Add(t);
        _components.Add(type, t);
    }
}