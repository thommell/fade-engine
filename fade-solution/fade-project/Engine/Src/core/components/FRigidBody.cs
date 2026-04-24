using System;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.Interfaces;
using Microsoft.Xna.Framework;

namespace fade_project.Core.Components.BaseAbstract;

public enum ForceTypes {
    Normal,
    Explosive
}

public sealed class FRigidBody : FComponent, IFixedUpdatableComponent {
    private const float Drag = 10f;
    private Vector2  velocity = Vector2.Zero;
    private Vector2 force = Vector2.Zero;

    public void FixedUpdate(float fixedDeltaTime) {
        ApplyForce(fixedDeltaTime);
    }
    
    public void AddForce(Vector2 addedForce, ForceTypes forceType = ForceTypes.Normal) {
        if (addedForce == Vector2.Zero || double.IsNaN(addedForce.X) || double.IsNaN(addedForce.Y)) {
            Logger.Log(this, "Tried to add zero force.", LogType.Warn);
            return;
        }

        addedForce *= forceType switch {
            ForceTypes.Explosive => 25,
            ForceTypes.Normal => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(forceType), forceType, null)
        };
        force += addedForce;
    }

    public Vector2 GetNormalizedVelocity() =>
        Vector2.Normalize(velocity);
    
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