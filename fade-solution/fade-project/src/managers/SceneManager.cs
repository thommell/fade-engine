using System;
using fade_project.containers;
using fade_project.testbed.scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.systems;

public sealed class SceneManager {
    private Scene _activeScene;
    public Scene GetActiveScene() => _activeScene ??= new TestScene();
    public void Update(GameTime gameTime) {
        _activeScene?.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch) {
        _activeScene?.Draw(spriteBatch);
    }
    public void ChangeScene(Scene newScene) {
        if (newScene == null) return;
        _activeScene.OnExit();
        _activeScene = newScene;
        _activeScene.OnEnter();
        Console.WriteLine($"Changed scene to {newScene.GetType().Name}!");
    }
}