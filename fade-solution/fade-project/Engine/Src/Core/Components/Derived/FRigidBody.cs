using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Services.Derived;
using fade_project.Core.Services.Enums;
using Microsoft.Xna.Framework;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class FRigidBody : FComponent, IUpdateableComponent{
    private const float GRAVITY = 9.81f;
    private Vector2  _velocity = Vector2.Zero;

    public void Update(float deltaTime) {
        if (_velocity.X <= 0f || _velocity.Y <= 0f) {
            _velocity = Vector2.Zero;
            return;
        }
        LoseVelocity();
    }
    public void AddForce(Vector2 force) {
        _velocity += force;
    }
    private void LoseVelocity() {
        this.Log(LogType.DEBUG, _velocity.ToString());
        _velocity.X -= GRAVITY;
        _velocity.Y -= GRAVITY;
        Transform.Translate(_velocity);
    }
}