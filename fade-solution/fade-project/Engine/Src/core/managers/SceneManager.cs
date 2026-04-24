using System.Collections.Generic;
using System.Linq;
using fade_project.containers;
using fade_project.Core;
using fade_project.testbed.scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core;

public static class SceneManager {
    private static Dictionary<string, Scene> scenes = new();
    private static Scene activeScene;
    private static bool isInitialized;
    private static bool isLoaded;
    public static Scene ActiveScene => activeScene;
    public static void Initialize(ContentManager content) {
        if (isInitialized) return;
        
        CreateScenes();
    }

    public static void Load() {
        if (isLoaded) return;
        ChangeScene(scenes.FirstOrDefault().Value);
        isLoaded = true;
    }
    
    public static void Update(GameTime gameTime) =>
        activeScene?.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
    public static void FixedUpdate(float fixedDeltaTime) =>
        activeScene?.FixedUpdate(fixedDeltaTime);
    public static void Draw(SpriteBatch spriteBatch) =>
        activeScene?.Draw(spriteBatch);
    
    public static void RequestSceneChange(string sceneName) {
        if (!scenes.TryGetValue(sceneName, out Scene newScene) || newScene == activeScene) {
            Logger.Log(typeof(SceneManager), "Scene is null or already active.", LogType.Warn);
            return;
        }
        
        ChangeScene(newScene);
    }
    
    private static void ChangeScene(Scene newScene) {
        if (newScene == null) return;
        activeScene?.OnExit();
        activeScene = newScene;
        activeScene.OnEnter();
        Logger.Log(typeof(SceneManager), $"Changed scene to {newScene.GetType().Name}", LogType.Info);
    }

    // temp to create scenes as there are none right now
    private static void CreateScenes() {
        scenes.TryAdd("test", new ColliderTest());
        isInitialized = true;
    }
}