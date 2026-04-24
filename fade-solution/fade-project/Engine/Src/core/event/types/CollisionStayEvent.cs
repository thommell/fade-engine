namespace fade_project.Core.Event.Types;

public readonly struct CollisionStayEvent : IFadeEvent {
    public GameObject Self { get; }
    public GameObject Other { get; }
    
    public CollisionStayEvent(GameObject self, GameObject other) {
        Self = self;
        Other = other;
    }
}