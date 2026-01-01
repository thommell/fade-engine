using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Event.Types;
using fade_project.Core.Services.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class FBoxCollider : FCollider {
    private Rectangle _hitbox;
    private FSpriteRenderer _renderer;
    
    public Rectangle Hitbox => _hitbox;
    public override void Initialize() {
        _renderer = GetComponent<FSpriteRenderer>();
        base.Initialize();
    }

    protected override void UpdateCollider(MoveEvent moveEvent) {
        _hitbox = new Rectangle((int)moveEvent.NewPosition.X - (int)Transform.Origin.X,
            (int)moveEvent.NewPosition.Y - (int)Transform.Origin.Y,
            _renderer.Texture.Width,
            _renderer.Texture.Height);
    }

    public override bool Intersects(FCollider other) {
        return other.IntersectsBox(this);
    }

    public override bool IntersectsCircle(FCircleCollider circle) {
        return false;
    }

    public override bool IntersectsBox(FBoxCollider box) {
        return _hitbox.Intersects(box.Hitbox);
    }
}