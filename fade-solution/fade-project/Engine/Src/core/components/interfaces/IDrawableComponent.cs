using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Components.BaseAbstract.Interfaces;

/// <summary>
/// The interface for components that need to be drawn.
/// </summary>
public interface IDrawableComponent {
    public void Draw(SpriteBatch spriteBatch);
}