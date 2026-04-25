using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FadeCore.Runtime;

public sealed class Engine {
    
    
    public void Initialize(ContentManager content) {
        Logger.Log(this, "Engine has initialized successfully.", LogType.Info);
    }
    
    public void Load(ContentManager content) {
        Assets.Load(content);
        SceneManager.Load();
        CollisionManager.Load();
        Logger.Log(this,  "Engine has loaded successfully.", LogType.Info);
    }
    
    public void Update(GameTime gameTime) {
        Time.Update(gameTime);
        Input.Update();
        SceneManager.Update(gameTime);
    }
    
    public void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Begin(samplerState: SamplerState.PointClamp); 
        SceneManager.Draw(spriteBatch);
        spriteBatch.End();
    }
}
