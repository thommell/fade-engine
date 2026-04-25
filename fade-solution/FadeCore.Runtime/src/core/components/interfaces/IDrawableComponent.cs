using Microsoft.Xna.Framework.Graphics;

namespace FadeCore.Runtime;

/// <summary>
/// The interface for components that need to be drawn.
/// </summary>
public interface IDrawableComponent {
    public void Draw(SpriteBatch spriteBatch);
}