using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Event;
using fade_project.Core.Event.Types;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class BoxCollider : Component, IDrawableComponent{
    private Rectangle _hitbox;
    private bool _isColliding;
    private SpriteRenderer _renderer;
    
    public Rectangle Hitbox => _hitbox;
    public bool IsColliding => _isColliding;
    public override void Initialize() {
        Owner.Events.Listen<MoveEvent>(UpdateCollider);
        _renderer = GetComponent<SpriteRenderer>();
        base.Initialize();
    }

    public override void Load() {
        Owner.Events.Invoke(new MoveEvent(Owner, Owner.Transform.Position));
        base.Load();
    }

    public void SetCollidingStatus(bool status) => _isColliding = status;

    public void Draw(SpriteBatch spriteBatch) {
        #if DEBUG
        if (IsColliding)
            spriteBatch.Draw(_renderer.Texture, _hitbox, Color.Red);
        #endif
    }
    private void UpdateCollider(MoveEvent moveEvent) {
        _hitbox = new Rectangle((int)moveEvent.NewPosition.X,
            (int)moveEvent.NewPosition.Y,
            _renderer.Texture.Width,
            _renderer.Texture.Height);
    }
}