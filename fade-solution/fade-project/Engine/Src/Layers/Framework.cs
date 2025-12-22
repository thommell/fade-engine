using fade_project.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Layers;

public class Framework {
    public void Initialize() {}

    public void Load(SpriteBatch spriteBatch, ContentManager content) {
        GlobalHelper.Content = content;
        content.Load<Texture2D>("Soldier-Idle");
    }
    public void Update(GameTime gameTime) {}
}