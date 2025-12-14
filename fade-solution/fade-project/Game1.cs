using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project;

public class Game1 : Game {
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private readonly FadeEngine _engine;

    public Game1() {
        _engine = new FadeEngine();
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        _engine.Initialize();
        base.Initialize();
    }

    protected override void LoadContent() {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _engine.Load(_spriteBatch, Content);
    }

    protected override void Update(GameTime gameTime) {
        _engine.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        _engine.Draw(_spriteBatch);
        base.Draw(gameTime);
    }
}