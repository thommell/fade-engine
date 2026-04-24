using System.Collections.Generic;
using System.Linq;
using fade_project.containers;
using fade_project.Core;
using fade_project.Core.Services;
using fade_project.Core.Services.Enums;
using fade_project.testbed.scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.systems;

public sealed class SceneService : Service {
    private readonly Dictionary<string, Scene> _scenes = new();
    private Scene _activeScene;
    private bool _isInitialized;
    private bool _isLoaded;
    private readonly float _fixedDelta = 1f / 50f;
    private float _fixedAccumulator = 0f;
    private const int MAX_FIXED_STEPS = 5;
    public Scene GetActiveScene() => _activeScene ??= new ColliderTest();
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
        // DEBUG CODE, THIS SHOULD BE PLACED MORE INTO A SEPARATE TIMESERVICE SCRIPT
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        _fixedAccumulator += dt;

        int fixedSteps = 0;

        while (_fixedAccumulator >= _fixedDelta) {
            _activeScene?.FixedUpdate(_fixedDelta);
            _fixedAccumulator -= _fixedDelta;
            fixedSteps++;
        }

        if (fixedSteps == MAX_FIXED_STEPS) {
            _fixedAccumulator = 0;
        }
        
        _activeScene?.Update(dt);
    }

    public override void Draw(SpriteBatch spriteBatch) {
        _activeScene?.Draw(spriteBatch);
    }
    public void RequestSceneChange(string sceneName) {
        //TODO: Add log info that scene doesnt exist
        if (!_scenes.TryGetValue(sceneName, out Scene newScene) || newScene == _activeScene) {
            this.Log(LogType.WARN, "Scene is null or already active.");
            return;
        }
        ChangeScene(newScene);
    }
    
    private void ChangeScene(Scene newScene) {
        if (newScene == null) return;
        _activeScene?.OnExit();
        _activeScene = newScene;
        _activeScene.OnEnter();
        this.Log(LogType.INFO, $"Changed scene to {newScene.GetType().Name}");
    }

    private void CreateScenes() {
        _scenes.TryAdd("test", new ColliderTest());
        
        _isInitialized = true;
    }
}