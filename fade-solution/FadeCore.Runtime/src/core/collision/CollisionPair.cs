namespace FadeCore.Runtime;

public record struct CollisionPair {
    public readonly FCollider ColliderA;
    public readonly FCollider ColliderB;

    public CollisionPair(FCollider colliderA, FCollider colliderB) {
        ColliderA = colliderA;
        ColliderB = colliderB;
    }
}