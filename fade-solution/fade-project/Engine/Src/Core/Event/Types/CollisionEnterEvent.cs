using fade_project.Core.Entities.Abstract;

namespace fade_project.Core.Event.Types;

public readonly struct CollisionEnterEvent : IFadeEvent {
    public GameObject Self { get; }
    public GameObject Other { get; }
    
    public CollisionEnterEvent(GameObject self, GameObject other) {
        Self = self;
        Other = other;
    }
}