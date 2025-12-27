using System;
using System.Collections.Generic;
using fade_project.Core;
using fade_project.Core.Event;
using fade_project.Core.Services;
using fade_project.Core.Services.Derived;
using fade_project.Core.Services.Enums;
using fade_project.Layers;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project;

public sealed class FadeEngine {
    private readonly Framework _framework = new();
    public void Initialize() {
        ServiceManager.Instance.AddService(new SceneService());
        ServiceManager.Instance.AddService(new InputService());
        ServiceManager.Instance.AddService(new AssetService());
        this.Log(LogType.INFO, "Engine has initialized successfully.");
    }
    
    public void Load(SpriteBatch spriteBatch, ContentManager content) {
        ServiceManager.Instance.Initialize(content);
        _framework.Initialize();
        // Initialize HAS to be earlier than load.
        ServiceManager.Instance.Load(spriteBatch, content);
        _framework.Load(spriteBatch, content);
        this.Log(LogType.INFO, "Engine has loaded successfully.");
        ServiceManager.Instance.LateLoad(spriteBatch, content);
        this.Log(LogType.INFO, "Engine has lateloaded successfully.");
        
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
