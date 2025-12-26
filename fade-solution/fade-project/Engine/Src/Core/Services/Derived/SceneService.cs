using System;
using System.Collections.Generic;
using System.Linq;
using fade_project.containers;
using fade_project.Core.Services;
using fade_project.Core.Services.Derived;
using fade_project.Core.Services.Enums;
using fade_project.testbed.scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace fade_project.systems;

public sealed class SceneService : Service {
    private readonly Dictionary<string, Scene> _scenes = new();
    private Scene _activeScene;
    private bool _isInitialized;
    private bool _isLoaded;
    public Scene GetActiveScene() => _activeScene ??= new TestScene();
    public override void Initialize(ContentManager content) {
        if (_isInitialized) return;
        CreateScenes();
    }

    public override void LateLoad(SpriteBatch spriteBatch, ContentManager content) {
        if (_isLoaded) return;
        ChangeScene(_scenes.FirstOrDefault().Value);
        _isLoaded = true;
    }
    
    public override void Update(GameTime gameTime) {
        _activeScene?.Update(gameTime);
    }
    public override void Draw(SpriteBatch spriteBatch) {
        _activeScene?.Draw(spriteBatch);
    }
    public void RequestSceneChange(string sceneName) {
        //TODO: Add log info that scene doesnt exist
        if (!_scenes.TryGetValue(sceneName, out Scene newScene) || newScene == _activeScene) {
            Logger.Log(LogType.WARN, "Scene is null or already active.");
            return;
        }
        ChangeScene(newScene);
    }
    
    private void ChangeScene(Scene newScene) {
        if (newScene == null) return;
        _activeScene?.OnExit();
        _activeScene = newScene;
        _activeScene.OnEnter();
        Logger.Log(LogType.INFO, $"Changed scene to {newScene.GetType().Name}");
    }

    private void CreateScenes() {
        _scenes.TryAdd("test", new TestScene());
        _scenes.TryAdd("test2", new TestScene2());
        
        _isInitialized = true;
    }
}