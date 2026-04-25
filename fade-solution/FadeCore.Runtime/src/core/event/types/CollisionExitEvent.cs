namespace FadeCore.Runtime;

public readonly struct CollisionExitEvent : IFadeEvent {
    public GameObject Self { get; }
    public GameObject Other { get; }
    
    public CollisionExitEvent(GameObject self, GameObject other) {
        Self = self;
        Other = other;
    }
}