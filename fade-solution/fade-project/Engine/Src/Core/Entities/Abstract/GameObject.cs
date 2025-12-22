using System;
using System.Collections.Generic;
using System.Linq;
using fade_project.Core.Components.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Entities.Abstract;

public class GameObject {
    private bool _isEnabled;

    private Dictionary<Type, Component> _components = [];
    private List<IDrawableComponent> _drawableComponents = [];
    private List<IUpdateableComponent> _updateableComponents = [];
    private Transform _transform;

    public Transform Transform => _transform;

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
        if (_components.TryGetValue(component, out Component match)) {
            return (T)match;
        }

        return null;
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
        _components.Add(t.GetType(), t);
    }
}