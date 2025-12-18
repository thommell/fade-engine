using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.containers;

public abstract class SubSystem {
    public virtual void Initialize() {}
    public virtual void Load() {}
    public virtual void Draw(SpriteBatch batch) {}
    public virtual void Update(GameTime gameTime) {}
}