using System;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Services.Enums;
using Microsoft.Xna.Framework;

namespace fade_project.Core.Components.BaseAbstract;

public enum ForceTypes {
    Normal,
    Explosive
}
public sealed class FRigidBody : FComponent, IFixedUpdatable {
    private const float Drag = 6.5f;
    private const float Mass = 1f;
    private Vector2  _velocity = Vector2.Zero;
    private Vector2 _force = Vector2.Zero;
    
    public void AddForce(Vector2 force, ForceTypes forceType = ForceTypes.Normal) {
        if (force == Vector2.Zero || double.IsNaN(force.X) || double.IsNaN(force.Y)) {
            this.Log(LogType.WARN, "Tried to add zero force.");
            return;
        }
        switch (forceType) {
            case ForceTypes.Normal:
                _force += force;
                break;
            case ForceTypes.Explosive:
                _force += force *= 25;
                break;
        }
        _force += force;
    }
    public void FixedUpdate(float fixedDeltaTime) {
        ApplyForce(fixedDeltaTime);
    }
    private void ApplyForce(float fixedDeltaTime) {
        this.Log(LogType.DEBUG, _velocity.ToString());
        Vector2 accel = _force;
        _velocity += accel * fixedDeltaTime;
        _velocity -= _velocity * Drag * fixedDeltaTime;
        if (_velocity.LengthSquared() < 0.0125f) {
            _velocity = Vector2.Zero;
            return;
        }
        MathHelper.Clamp(_velocity.X, 0, 12.5f);
        MathHelper.Clamp(_velocity.Y, 0, 12.5f);
        Transform.Translate(_velocity);
        _force = Vector2.Zero;
    }

    public Vector2 GetDirection() {
        return Vector2.Normalize(_velocity);
    }
}