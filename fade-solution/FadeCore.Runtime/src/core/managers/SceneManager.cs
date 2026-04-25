using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace FadeCore.Runtime;

public static class SceneManager {
    private static Dictionary<string, Scene> scenes = new();
    private static bool isInitialized;
    private static bool isLoaded;
    public static Scene ActiveScene { get; private set; }

    public static void Load() {
        if (isLoaded) return;
        ChangeScene(scenes.FirstOrDefault().Value);
        isLoaded = true;
    }
    
    public static void Update(GameTime gameTime) =>
        ActiveScene?.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
    public static void FixedUpdate(float fixedDeltaTime) =>
        ActiveScene?.FixedUpdate(fixedDeltaTime);
    public static void Draw(SpriteBatch spriteBatch) =>
        ActiveScene?.Draw(spriteBatch);
    
    public static void RequestSceneChange(string sceneName) {
        if (!scenes.TryGetValue(sceneName, out Scene newScene) || newScene == ActiveScene) {
            Logger.Log(typeof(SceneManager), "Scene is null or already active.", LogType.Warn);
            return;
        }
        ChangeScene(newScene);
    }
    
    private static void ChangeScene(Scene newScene) {
        if (newScene == null) return;
        ActiveScene?.OnExit();
        ActiveScene = newScene;
        ActiveScene.OnEnter();
        Logger.Log(typeof(SceneManager), $"Changed scene to {newScene.GetType().Name}", LogType.Info);
    }
}