using System;
using fade_project.containers;
using fade_project.Core.Components.BaseAbstract;
using fade_project.Core.Entities.Abstract;
using fade_project.testbed.Scripts;
using Microsoft.Xna.Framework;

namespace fade_project.testbed.scenes;

public class TestScene : Scene {
    public override void OnEnter() {
        AddObject(new GameObject(new Transform(scale: new Vector2(1.5f, 1.5f)),
            components: [
                new SpriteRenderer("Christmas tree"),
                new PlayerMovement()
            ]));
        base.OnEnter();
    }
    public override void OnExit() {
        Console.WriteLine("LEAVING");
        base.OnExit();
    }
}