using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using Microsoft.Xna.Framework;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class BoxCollider : Component, IUpdateableComponent {
    private Rectangle _hitbox;
    private SpriteRenderer _renderer;
    public override void Initialize() {
        _renderer = GetComponent<SpriteRenderer>();
        base.Initialize();
    }

    public void Update(GameTime gameTime) => UpdateCollider();

    private void UpdateCollider() {
        
    }
}