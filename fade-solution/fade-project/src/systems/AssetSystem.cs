using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.systems;

public sealed class AssetSystem {
    private Dictionary<string, Texture2D> _textures = [];
    public void AddTexture(string name, Texture2D texture) {
        if (_textures.TryGetValue(name, out Texture2D val)) return;
        _textures[name] = texture; 
    }
    public Texture2D GetTexture(string name) {
        _textures.TryGetValue(name, out Texture2D value);
        return value;
    }
}