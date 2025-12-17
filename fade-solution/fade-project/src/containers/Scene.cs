using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.containers;

public abstract class Scene {
    public virtual void Draw(SpriteBatch spriteBatch) {}
    public virtual void Update(GameTime gameTime) {}
    public virtual void OnEnter() {}
    public virtual void OnExit() {}
}