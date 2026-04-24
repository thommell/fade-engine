using fade_project.Core;
using fade_project.Core.Services;
using fade_project.Core.Services.Derived;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project;

public sealed class FadeEngine {
    
    
    public void Initialize(ContentManager content) {
        Logger.Log(this, "Engine has initialized successfully.", LogType.Info);
        SceneManager.Initialize(content);
    }
    
    public void Load(ContentManager content) {
        Assets.Load(content);
        SceneManager.Load();
        Logger.Log(this,  "Engine has loaded successfully.", LogType.Info);
    }
    
    public void Update(GameTime gameTime) {
        Time.Update(gameTime);
        Input.Update();
        SceneManager.Update(gameTime);
    }

    public void FixedUpdate(float fixedDelta) {
        CollisionManager.FixedUpdate(fixedDelta);
    }
    
    public void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Begin(samplerState: SamplerState.PointClamp); 
        SceneManager.Draw(spriteBatch);
        spriteBatch.End();
    }
}
