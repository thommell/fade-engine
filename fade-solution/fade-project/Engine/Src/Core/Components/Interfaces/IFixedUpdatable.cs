namespace fade_project.Core.Components.BaseAbstract.Interfaces;

/// <summary>
/// The interface for any physics-based update-loop. 
/// </summary>
public interface IFixedUpdatable {
    public void FixedUpdate(float fixedDeltaTime);
}