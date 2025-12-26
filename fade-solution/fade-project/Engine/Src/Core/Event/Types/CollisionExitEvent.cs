using fade_project.Core.Entities.Abstract;

namespace fade_project.Core.Event.Types;

public readonly struct CollisionExitEvent : IFadeEvent {
    public GameObject Self { get; }
    public GameObject Other { get; }
    
    public CollisionExitEvent(GameObject self, GameObject other) {
        Self = self;
        Other = other;
    }
}