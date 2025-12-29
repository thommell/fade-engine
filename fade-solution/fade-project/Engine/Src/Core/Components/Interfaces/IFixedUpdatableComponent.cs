namespace fade_project.Core.Components.BaseAbstract.Interfaces;

/// <summary>
/// The interface for any physics-based update-loop.
/// <para>
/// This should not be used for UI and/or Input
/// as this only get's called every X amount of frames.
/// </para>
/// </summary>
public interface IFixedUpdatableComponent {
    public void FixedUpdate(float fixedDeltaTime);
}