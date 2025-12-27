using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Event.Types;
using fade_project.Core.Services.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class FBoxCollider : FComponent {
    private Rectangle _hitbox;
    private FSpriteRenderer _renderer;
    
    public Rectangle Hitbox => _hitbox;
    public bool IsColliding { get; set; }
    public override void Initialize() {
        Owner.Events.Listen<MoveEvent>(UpdateCollider);
        _renderer = GetComponent<FSpriteRenderer>();
        base.Initialize();
    }

    public override void Load() {
        Owner.Events.Invoke(new MoveEvent(Owner, Owner.Transform.Position));
        base.Load();
    }

    private void UpdateCollider(MoveEvent moveEvent) {
        _hitbox = new Rectangle((int)moveEvent.NewPosition.X - (int)Transform.Origin.X,
            (int)moveEvent.NewPosition.Y - (int)Transform.Origin.Y,
            _renderer.Texture.Width,
            _renderer.Texture.Height);
    }
}