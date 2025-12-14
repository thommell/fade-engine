using System;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace fade_project;

public sealed class FadeEngine {
    // MonoGame
    private SpriteBatch _spriteBatch;
    private ContentManager _content;
    
    // Sub-systems
    private readonly InputSystem _input = new();
    private readonly AssetSystem _assets = new();
    private readonly SceneManager _sceneManager = new();
    
    public void Initialize() {}
    public void Load(SpriteBatch spriteBatch, ContentManager content) {
        _spriteBatch = spriteBatch;
        _content = content;
    }
    public void Unload() {}

    public void Update(GameTime gameTime) {
        _input.Update(gameTime);
        _sceneManager.Update(gameTime);
    }
    public void Draw(SpriteBatch spriteBatch) {
    }
}