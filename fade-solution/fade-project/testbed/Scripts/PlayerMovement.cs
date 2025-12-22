using System;
using System.Drawing;
using fade_project.Core.Components.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;

namespace fade_project.testbed.Scripts;

public class PlayerMovement : Component, IUpdateableComponent {
    private InputService _input;
    private SpriteRenderer _renderer;
    public override void Initialize() {
        _input = GetService<InputService>();
        _renderer = GetComponent<SpriteRenderer>();
        base.Initialize();
    }

    public void Update(GameTime gameTime) {
        Move();
    }

    private void Move() {
        // debug movement: DELETE RENDERER SHENANIGANS
        Vector2 newPosition = Vector2.Zero;
        if (_renderer.Color != Color.White)
            _renderer.SetColor(Color.White);
        if (_input.IsKeyDown(Keys.W)) newPosition.Y -= 1;
        if (_input.IsKeyDown(Keys.A)) newPosition.X -= 1;
        if (_input.IsKeyDown(Keys.S)) newPosition.Y += 1;
        if (_input.IsKeyDown(Keys.D)) newPosition.X += 1;
        if (newPosition == Vector2.Zero) return;
        if (_renderer.Color == Color.White) 
            _renderer.SetColor(Color.Black);
        newPosition.Normalize();
        Owner.Transform.Translate(newPosition);
    }
}