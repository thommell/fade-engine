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

public class PlayerMovement : Component, IUpdateableComponent {
    private InputService _input;
    private SpriteRenderer _renderer;
    private EventService _eventService;
    private int _count = 0;
    public override void Initialize() {
        _input = GetService<InputService>();
        _eventService = GetService<EventService>();
        _renderer = GetComponent<SpriteRenderer>();
        base.Initialize();
    }

    public override void Load() {
        _eventService.CreateEvent(FadeEventType.PlayerMoving);
        _eventService.Listen(FadeEventType.PlayerMoving, EventTest);
        base.Load();
    }

    public void Update(GameTime gameTime) {
        Move();
    }

    private void EventTest() {
        _count++;
        Console.WriteLine(_count);
    }

    private void EventTest2() {
        Console.WriteLine("Wowie!");
    }

    private void Move() {
        // debug movement: DELETE RENDERER SHENANIGANS
        Vector2 newPosition = Vector2.Zero;
        if (_input.IsKeyDown(Keys.W)) newPosition.Y -= 1;
        if (_input.IsKeyDown(Keys.A)) newPosition.X -= 1;
        if (_input.IsKeyDown(Keys.S)) newPosition.Y += 1;
        if (_input.IsKeyDown(Keys.D)) newPosition.X += 1;
        if (newPosition == Vector2.Zero) return;
        newPosition.Normalize();
        Owner.Transform.Translate(newPosition);
        _eventService.InvokeEvent(FadeEventType.PlayerMoving);
    }
}