
using System;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Components.BaseAbstract;

// TODO: IMPORTANT!
//
// REFACTOR THIS SCRIPT TO USE THE SERVICE CALLED ASSETSYSTEM TO GET TEXTURES,
// NOT THE GLOBAL CONTENT CLASS.
//
public sealed class SpriteRenderer : Component, IDrawableComponent {
    private Texture2D _texture;
    private Transform _transform;
    private Color _color = Color.White;

    public Color Color => _color;
    public SpriteRenderer(string textureName) {
        _texture = GetTexture(textureName);
    }
    
    public override void Initialize() {
        _transform = GetComponent<Transform>();
        base.Initialize();
    }
    public override void Load() {
        if (_transform != null && _texture != null) {
            _transform.SetOrigin(new Vector2(_texture.Width * 0.5f, _texture.Height * 0.5f));
        }
        base.Load();
    }


    public void Draw(SpriteBatch spriteBatch) {
        if (_texture == null) return;
        spriteBatch.Draw(_texture, _transform.Position, _color);
    }

    public void SetColor(Color color) => _color = color;

    private Texture2D GetTexture(string textureName) => GlobalHelper.Content.Load<Texture2D>(textureName);
}