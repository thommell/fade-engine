using fade_project.Core.Components.BaseAbstract;

namespace fade_project.Engine.Core.Services.Derived.Collision;

public record struct ActiveCollisionPair {
    public readonly BoxCollider ColliderA;
    public readonly BoxCollider ColliderB;

    public ActiveCollisionPair(BoxCollider colliderA, BoxCollider colliderB) {
        ColliderA = colliderA;
        ColliderB = colliderB;
    }
}