using System;
using System.Drawing;
using fade_project.Core.Components.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Event;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;

namespace fade_project.testbed.Scripts;

public class PlayerMovement : FadeComponent, IUpdateableComponent {
    private SpriteRenderer _renderer;
    private InputService _inputService;
    private EventService _eventService;
    public override void Initialize() {
        _inputService = GetService<InputService>();
        _eventService = GetService<EventService>();
        _renderer = GetComponent<SpriteRenderer>();
        base.Initialize();
    }

    public override void Load() {
        _eventService.CreateEvent(FadeEventType.PlayerMoved);
        base.Load();
    }

    public void Update(GameTime gameTime) {
        Move();
    }


    private void Move() {
        // debug movement: DELETE RENDERER SHENANIGANS
        Vector2 newPosition = Vector2.Zero;
        if (_inputService.IsKeyDown(Keys.W)) newPosition.Y -= 1;
        if (_inputService.IsKeyDown(Keys.A)) newPosition.X -= 1;
        if (_inputService.IsKeyDown(Keys.S)) newPosition.Y += 1;
        if (_inputService.IsKeyDown(Keys.D)) newPosition.X += 1;
        if (newPosition == Vector2.Zero) return;
        newPosition.Normalize();
        Owner.Transform.Translate(newPosition);
        _eventService.InvokeEvent(FadeEventType.PlayerMoved);
    }
}