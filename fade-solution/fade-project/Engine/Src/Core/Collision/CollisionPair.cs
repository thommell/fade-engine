using fade_project.Core.Components.BaseAbstract;

namespace fade_project.Engine.Core.Services.Derived.Collision;

public record struct CollisionPair {
    public readonly FBoxCollider ColliderA;
    public readonly FBoxCollider ColliderB;

    public CollisionPair(FBoxCollider colliderA, FBoxCollider colliderB) {
        ColliderA = colliderA;
        ColliderB = colliderB;
    }
}