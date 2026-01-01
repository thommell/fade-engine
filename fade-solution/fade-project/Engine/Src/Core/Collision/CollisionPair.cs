using fade_project.Core.Components.BaseAbstract;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;

namespace fade_project.Engine.Core.Services.Derived.Collision;

public record struct CollisionPair {
    public readonly FCollider ColliderA;
    public readonly FCollider ColliderB;

    public CollisionPair(FCollider colliderA, FCollider colliderB) {
        ColliderA = colliderA;
        ColliderB = colliderB;
    }
}