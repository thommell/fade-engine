using System.Collections.Generic;
using fade_project.containers;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project;

public sealed class FadeEngine {
    // MonoGame
    private SpriteBatch _spriteBatch;
    private ContentManager _content;
    
    private readonly List<SubSystem> _systems = [];

    public void Initialize() {
        AddSystem(new InputSystem());
        AddSystem(new SceneManager());
        for (int i = 0; i < _systems.Count; i++) {
            _systems[i].Initialize();
        }
    }
    
    public void Load(SpriteBatch spriteBatch, ContentManager content) {
        _spriteBatch = spriteBatch;
        _content = content;
        for (int i = 0; i < _systems.Count; i++) {
            _systems[i].Load();
        }
    }
    
    public void Update(GameTime gameTime) {
        for (int i = 0; i < _systems.Count; i++) {
            _systems[i].Update(gameTime);
        }
    }
    public void Draw(SpriteBatch spriteBatch) {
        for (int  i = 0; i < _systems.Count; i++) {
            _systems[i].Draw(spriteBatch);
        }
    }
    private void AddSystem(SubSystem system) {
        _systems.Add(system);
    }
}