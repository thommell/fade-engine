using System;
using System.Collections.Generic;
using fade_project.Core.Globals;
using fade_project.Core.Services;
using fade_project.Layers;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project;

public sealed class FadeEngine {
    // Add TimeService
    // Add InputService
    // Add SceneManager
    
    private readonly SceneManager _sceneManager = new();
    private readonly Framework _framework = new();

    public void Initialize() {
        ServiceManager.Instance.AddService(new SceneManager());
        ServiceManager.Instance.AddService(new InputService());
        ServiceManager.Instance.Initialize();
        _framework.Initialize();
    }
    
    public void Load(SpriteBatch spriteBatch, ContentManager content) {
        _framework.Load(spriteBatch, content);
        ServiceManager.Instance.Load(spriteBatch);
    }
    
    public void Update(GameTime gameTime) {
        ServiceManager.Instance.Update(gameTime);
        _framework.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Begin();
        ServiceManager.Instance.Draw(spriteBatch);
        spriteBatch.End();
    }
}
