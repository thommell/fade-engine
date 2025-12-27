using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class FSpriteRenderer : FComponent, IDrawableComponent {
    private Texture2D _texture;
    private string _textureName;
    private Color _color = Color.White;
    
    private AssetService _assetService;
    
    public Color Color => _color;
    public Texture2D Texture => _texture;
    public FSpriteRenderer(string textureName) {
        _textureName = textureName;
    }
    
    public override void Initialize() {
        _assetService = GetService<AssetService>();
        base.Initialize();
    }
    public override void Load() {
        _texture = SetTexture(_textureName);
        if (Transform != null && _texture != null) {
            Transform.SetOrigin(new Vector2(_texture.Width * 0.5f, _texture.Height * 0.5f));
        }
        base.Load();
    }

    public void Draw(SpriteBatch spriteBatch) {
        if (_texture == null) return;
        spriteBatch.Draw(
            _texture,
            Transform.Position,
            null,
            _color,
            MathHelper.ToDegrees(Transform.Rotation),
            Transform.Origin,
            Transform.Scale,
            SpriteEffects.None,
            1
            );
    }

    public void SetColor(Color color) => _color = color;

    private Texture2D SetTexture(string textureName) => _assetService.GetTexture(textureName);
}