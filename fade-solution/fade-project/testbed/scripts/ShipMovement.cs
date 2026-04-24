using fade_project.Core;
using fade_project.Core.Components.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Event.Types;
using fade_project.systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace fade_project.testbed.Scripts;

public class ShipMovement : FadeBehaviour, IFixedUpdatableComponent, IUpdateableComponent {
    private FSpriteRenderer _renderer;
    private FRigidBody _rb;
    private float _speed = 15f;

    public override void Initialize() {
        _renderer = GetComponent<FSpriteRenderer>();
        _rb = GetComponent<FRigidBody>();
        base.Initialize();
    }

    protected override void OnCollisionEnter(CollisionEnterEvent data) {
        _renderer.SetColor(Color.Black);
        base.OnCollisionEnter(data);
    }

    protected override void OnCollisionExit(CollisionExitEvent data) {
        _renderer.SetColor(Color.White);
        base.OnCollisionExit(data);
    }
    
    private void Move() {
        Vector2 newPosition = Vector2.Zero;
        
        if (Input.IsKeyDown(Keys.W)) newPosition.Y -= 1;
        if (Input.IsKeyDown(Keys.A)) newPosition.X -= 1;
        if (Input.IsKeyDown(Keys.S)) newPosition.Y += 1;
        if (Input.IsKeyDown(Keys.D)) newPosition.X += 1;
        if (newPosition == Vector2.Zero) return;
        
        newPosition.Normalize();
        _rb.AddForce(newPosition * _speed);
    }
    public void FixedUpdate(float fixedDeltaTime) {
        Move();
    }
    private void Boost() {
        if (Input.IsKeyPressed(Keys.Space)) 
            _rb.AddForce(_rb.GetNormalizedVelocity() * 10, ForceTypes.Explosive);
    }
    public void Update(float deltaTime) {
        Boost();
    }
}