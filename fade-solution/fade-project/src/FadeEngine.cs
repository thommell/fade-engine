using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project;

public sealed class FadeEngine {
    // MonoGame
    private SpriteBatch _spriteBatch;
    private ContentManager _content;
    public void Initialize() {}
    public void Load(SpriteBatch spriteBatch, ContentManager content) {
        _spriteBatch = spriteBatch;
        _content = content;
    }
    public void Unload() {}
    public void Update(GameTime gameTime) {}
    public void Draw(SpriteBatch spriteBatch) {
    }
}