using System.Collections.Generic;
using System.Runtime.CompilerServices;
using fade_project.Core.Services;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.systems;

public static class Assets {
    private static Dictionary<string, Texture2D> textures = [];
    private static ContentManager content;
    public static void Load(ContentManager content) {
        Assets.content = content;
        AddAllTextures();
    }
    private static void AddTexture(string name) {
        if (textures.TryGetValue(name, out _)) return;
        
        Texture2D texture = content.Load<Texture2D>(name);
        textures[name] = texture; 
    }
    public static Texture2D GetTexture(string name) {
        textures.TryGetValue(name, out Texture2D value);
        return value;
    }
    
    private static void AddAllTextures() {
        AddTexture("Christmas tree");
        AddTexture("Blue");
        AddTexture("Gray1");
    }
}