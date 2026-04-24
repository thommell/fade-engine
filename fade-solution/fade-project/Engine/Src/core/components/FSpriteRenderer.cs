using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class FSpriteRenderer : FComponent, IDrawableComponent {
    private Texture2D texture;
    private string textureName;
    private Color color = Color.White;
    
    public Color Color => color;
    public Texture2D Texture => texture;
    public FSpriteRenderer(string textureName) {
        this.textureName = textureName;
    }
    
    public override void Load() {
        texture = SetTexture(textureName);
        if (Transform != null && texture != null) {
            Transform.SetOrigin(new Vector2(texture.Width * 0.5f, texture.Height * 0.5f));
        }
        base.Load();
    }

    public void Draw(SpriteBatch spriteBatch) {
        if (texture == null) return;
        
        Vector2 drawPos = Vector2.Round(Transform.Position);
        spriteBatch.Draw(
            texture,
            drawPos,
            null,
            color,
            MathHelper.ToDegrees(Transform.Rotation),
            Transform.Origin,
            Transform.Scale,
            SpriteEffects.None,
            1
            );
    }

    public void SetColor(Color color) => this.color = color;
    private Texture2D SetTexture(string textureName) => Assets.GetTexture(textureName);
}