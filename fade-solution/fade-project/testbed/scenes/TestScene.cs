using System;
using fade_project.containers;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace fade_project.testbed.scenes;

public class TestScene : Scene {
    public override void OnEnter() {
        Console.WriteLine("Test!");
        base.OnEnter();
    }

    public override void Update(GameTime gameTime) {
    }
}