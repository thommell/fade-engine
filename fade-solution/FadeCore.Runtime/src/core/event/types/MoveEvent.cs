using Microsoft.Xna.Framework;

namespace FadeCore.Runtime;

public struct MoveEvent : IFadeEvent {
    public GameObject Object { get; }
    public Vector2 NewPosition { get; }
    public MoveEvent(GameObject obj, Vector2 newPosition) {
        Object = obj;
        NewPosition = newPosition;
    }
}