using System;
using fade_project.containers;
using fade_project.Core.Components.BaseAbstract;
using fade_project.Core.Entities.Abstract;
using fade_project.Core.Services.Derived;
using fade_project.testbed.Scripts;
using Microsoft.Xna.Framework;

namespace fade_project.testbed.scenes;

public class ColliderTest : Scene {
    public override void OnEnter() {
        AddObject(new GameObject(components: new CollisionManager()));
        AddObject(new GameObject(new FTransform(),
            components: [
                new FSpriteRenderer("Blue"),
                new ShipMovement(),
                new FRigidBody(),
                new FBoxCollider()
            ]));
        AddObject(new GameObject(new FTransform(position: new Vector2(80)), 
            components: [
                new FSpriteRenderer("Gray1"),
                new FBoxCollider(),
            ]));
        base.OnEnter();
    }
    public override void OnExit() {
        Console.WriteLine("LEAVING");
        base.OnExit();
    }
}