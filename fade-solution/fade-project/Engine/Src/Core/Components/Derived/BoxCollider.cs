using System;
using System.Collections.Generic;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Entities.Abstract;
using fade_project.Core.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class BoxCollider : Component, IDrawableComponent{
    private Rectangle _hitbox;
    private bool _isColliding;
    private SpriteRenderer _renderer;
    private EventService _eventService;
    
    public Rectangle Hitbox => _hitbox;
    public bool IsColliding => _isColliding;
    public override void Initialize() {
        _renderer = GetComponent<SpriteRenderer>();
        _eventService = GetService<EventService>();
        base.Initialize();
    }

    public override void Load() {
        _eventService.Listen(FadeEventType.PlayerMoved, UpdateCollider);
        UpdateCollider();
        base.Load();
    }

    public void OnCollisionEnter(GameObject other) {
        List<FadeComponent> componentsToCall = Owner.GetComponents<FadeComponent>();
        if (componentsToCall.Count == 0) {
            return;
        }
        foreach (FadeComponent component in componentsToCall) {
            component.OnCollisionEnter(other);
        }
    }

    public void OnCollisionExit() {
        List<FadeComponent> componentsToCall = Owner.GetComponents<FadeComponent>();
        if (componentsToCall.Count == 0) {
            return;
        }
        foreach (FadeComponent component in componentsToCall) {
            component.OnCollisionExit();
        }
    }
    public void SetCollidingStatus(bool status) => _isColliding = status;

    public void Draw(SpriteBatch spriteBatch) {
        #if DEBUG
        if (IsColliding)
            spriteBatch.Draw(_renderer.Texture, _hitbox, Color.Red);
        #endif
    }
    private void UpdateCollider() {
        _hitbox = new Rectangle((int)Transform.Position.X,
            (int)Transform.Position.Y,
            _renderer.Texture.Width,
            _renderer.Texture.Height);
    }
}