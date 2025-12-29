using System;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using fade_project.Core.Services.Enums;
using Microsoft.Xna.Framework;

namespace fade_project.Core.Components.BaseAbstract;

public enum ForceTypes {
    NORMAL,
    EXPLOSIVE
}
public sealed class FRigidBody : FComponent, IFixedUpdatableComponent {
    private const float DRAG = 10f;
    private const float MASS = 1f;
    private Vector2  _velocity = Vector2.Zero;
    private Vector2 _force = Vector2.Zero;
    
    public void AddForce(Vector2 addedForce, ForceTypes forceType = ForceTypes.NORMAL) {
        if (addedForce == Vector2.Zero || double.IsNaN(addedForce.X) || double.IsNaN(addedForce.Y)) {
            this.Log(LogType.WARN, "Tried to add zero force.");
            return;
        }

        addedForce *= forceType switch {
            ForceTypes.EXPLOSIVE => 25,
            ForceTypes.NORMAL => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(forceType), forceType, null)
        };
        _force += addedForce;
    }

    public void FixedUpdate(float fixedDeltaTime) {
        ApplyForce(fixedDeltaTime);
    }

    public Vector2 GetNormalizedVelocity() {
        return Vector2.Normalize(_velocity);
    }
    private void ApplyForce(float fixedDeltaTime) {
        Vector2 accel = _force;
        _velocity += accel * fixedDeltaTime;
        _velocity -= _velocity * DRAG * fixedDeltaTime;
        if (_velocity.LengthSquared() < 0.0125f) {
            _velocity = Vector2.Zero;
            return;
        }
        MathHelper.Clamp(_velocity.X, 0, 12.5f);
        MathHelper.Clamp(_velocity.Y, 0, 12.5f);
        Transform.Translate(_velocity);
        _force = Vector2.Zero;
    }
}