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
    private const float Drag = 10f;
    private Vector2  velocity = Vector2.Zero;
    private Vector2 force = Vector2.Zero;
    
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
        force += addedForce;
    }

    public void FixedUpdate(float fixedDeltaTime) {
        ApplyForce(fixedDeltaTime);
    }

    public Vector2 GetNormalizedVelocity() {
        return Vector2.Normalize(velocity);
    }
    private void ApplyForce(float fixedDeltaTime) {
        Vector2 accel = force;
        velocity += accel * fixedDeltaTime;
        velocity -= velocity * Drag * fixedDeltaTime;
        if (velocity.LengthSquared() < 0.0125f) {
            velocity = Vector2.Zero;
            return;
        }
        MathHelper.Clamp(velocity.X, 0, 12.5f);
        MathHelper.Clamp(velocity.Y, 0, 12.5f);
        Transform.Translate(velocity);
        force = Vector2.Zero;
    }
}