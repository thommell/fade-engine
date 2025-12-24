using System.Collections.Generic;
using fade_project.containers;
using fade_project.Core.Services;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.systems;

public sealed class AssetService : Service {
    private Dictionary<string, Texture2D> _textures = [];
    private ContentManager _content;
    public override void Load(SpriteBatch spriteBatch, ContentManager content) {
        _content = content;
        AddAllTextures();
        base.Load(spriteBatch, content);
    }
    private void AddTexture(string name) {
        if (_textures.TryGetValue(name, out Texture2D value)) return;
        Texture2D texture = _content.Load<Texture2D>(name);
        _textures[name] = texture; 
    }
    public Texture2D GetTexture(string name) {
        _textures.TryGetValue(name, out Texture2D value);
        return value;
    }

    // Debug
    private void AddAllTextures() {
        AddTexture("Christmas tree");
    }
}