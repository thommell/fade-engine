using System;
using fade_project.Core.Entities.Abstract;
using fade_project.Core.Services;

namespace fade_project.Core.Components.BaseAbstract.BaseAbstract;

public abstract class Component {
    private GameObject _owner;
    
    protected Transform Transform => _owner.Transform;
    protected GameObject Owner => _owner;
    
    public virtual void Initialize() {}
    public virtual void Load() {}
    public void SetOwner(GameObject owner) => _owner = owner;

    /// <summary>
    /// Provider for the components within the GameObject.
    /// </summary>
    /// <returns>The given instance of value T</returns>
    /// <typeparam name="T">The generic value of the specified Component</typeparam>
    /// <returns></returns>
    protected T GetComponent<T>() where T : Component {
        return Owner.GetComponent<T>();
    }

    /// <summary>
    /// Provider for the main services within this Engine.
    /// <code>
    /// instance.GetService&lt;InputService&gt;();
    /// </code>
    /// <returns>The given instance of value T</returns>
    /// </summary>
    /// <typeparam name="T">The generic value of the specified Service.</typeparam>
    /// <returns></returns>
    protected T GetService<T>() where T : Service {
        return ServiceManager.Instance.GetService<T>();
    }
}