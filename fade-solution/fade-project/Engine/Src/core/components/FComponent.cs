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
}