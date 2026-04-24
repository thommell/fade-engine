using fade_project.Core.Services;

namespace fade_project.Core.Components.BaseAbstract.BaseAbstract;

public abstract class FComponent {
    private GameObject owner;
    
    protected FTransform Transform => owner.Transform;
    public GameObject Owner => owner;
    
    public virtual void Initialize() {}
    public virtual void Load() {}
    public virtual void LateLoad() {}
    public void SetOwner(GameObject owner) => this.owner = owner;

    /// <summary>
    /// Provider for the components within the GameObject.
    /// </summary>
    /// <returns>The given instance of value T</returns>
    /// <typeparam name="T">The generic value of the specified Component</typeparam>
    /// <returns></returns>
    protected T GetComponent<T>() where T : FComponent => Owner.GetComponent<T>();

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