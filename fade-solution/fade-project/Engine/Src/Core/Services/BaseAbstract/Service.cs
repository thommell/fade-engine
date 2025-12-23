using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Services;

public abstract class Service {
    internal ServiceManager manager;
    public virtual void Initialize() {}
    public virtual void Load(SpriteBatch spriteBatch, ContentManager content) {}
    public virtual void LateLoad(SpriteBatch spriteBatch, ContentManager content) {}
    public virtual void Update(GameTime gameTime) {}
    public virtual void Draw(SpriteBatch spriteBatch) {}
}