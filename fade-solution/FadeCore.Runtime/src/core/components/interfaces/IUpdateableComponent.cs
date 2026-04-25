using Microsoft.Xna.Framework;

namespace FadeCore.Runtime;

/// <summary>
/// The interface for any Input-based update-loop.
/// <para>
/// This should NOT be used for anything physics
/// as this is not FPS-capped.
/// </para>
/// </summary>
public interface IUpdateableComponent {
    public void Update(float deltaTime);
}