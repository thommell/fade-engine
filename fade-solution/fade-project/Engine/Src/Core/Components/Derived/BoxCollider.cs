using System;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class BoxCollider : Component, IDrawableComponent{
    private Rectangle _hitbox;
    private SpriteRenderer _renderer;
    private EventService _eventService;
    
    public Rectangle Hitbox => _hitbox;
    public override void Initialize() {
        _renderer = GetComponent<SpriteRenderer>();
        _eventService = GetService<EventService>();
        base.Initialize();
    }

    public override void Load() {
        _eventService.Listen(FadeEventType.PlayerMoving, UpdateCollider);
        UpdateCollider();
        base.Load();
    }

    private void UpdateCollider() {
        _hitbox = new Rectangle((int)Transform.Position.X,
            (int)Transform.Position.Y,
            _renderer.Texture.Width,
            _renderer.Texture.Height);
    }

    public void Draw(SpriteBatch spriteBatch) {
        #if DEBUG
        spriteBatch.Draw(_renderer.Texture, _hitbox, Color.Red);
        #endif
    }
}