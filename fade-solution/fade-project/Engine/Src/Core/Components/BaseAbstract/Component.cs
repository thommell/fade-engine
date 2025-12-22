using System;
using fade_project.Core.Entities.Abstract;
using fade_project.Core.Services;

namespace fade_project.Core.Components.BaseAbstract.BaseAbstract;

public abstract class Component {
    private GameObject _owner;
    
    public GameObject Owner => _owner;
    
    public virtual void Initialize() {}

    public virtual void Load() {}
    
    public void SetOwner(GameObject owner) => _owner = owner;

    // Wrapper
    protected T GetComponent<T>() where T : Component {
        return _owner.GetComponent<T>();
    }
    protected T GetService<T>() where T : Service {
        return ServiceManager.Instance.GetService<T>();
    }
}