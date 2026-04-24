using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project;

public class Game1 : Game {
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    
    private readonly FadeEngine engine;

    public Game1() {
        engine = new FadeEngine();
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize() {
        IsFixedTimeStep = false;
        engine.Initialize(Content);
        base.Initialize();
    }

    protected override void LoadContent() {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        engine.Load(Content);
    }

    protected override void Update(GameTime gameTime) {
        engine.Update(gameTime);
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime) {
        GraphicsDevice.Clear(Color.CornflowerBlue);
        engine.Draw(spriteBatch);
        base.Draw(gameTime);
    }
}