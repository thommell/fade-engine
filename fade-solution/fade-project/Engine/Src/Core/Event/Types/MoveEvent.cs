using fade_project.Core.Entities.Abstract;
using Microsoft.Xna.Framework;

namespace fade_project.Core.Event.Types;

public struct MoveEvent : IFadeEvent {
    public GameObject Object { get; }
    public Vector2 NewPosition { get; }
    public MoveEvent(GameObject obj, Vector2 newPosition) {
        Object = obj;
        NewPosition = newPosition;
    }
}