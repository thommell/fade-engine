using System;
using fade_project.containers;
using Microsoft.Xna.Framework;

namespace fade_project.testbed.scenes;

public class TestScene : Scene {
    public override void OnEnter() {
        Console.WriteLine("Test!");
        base.OnEnter();
    }

    public override void OnExit() {
        Console.WriteLine("LEAVING");
        base.OnExit();
    }
}