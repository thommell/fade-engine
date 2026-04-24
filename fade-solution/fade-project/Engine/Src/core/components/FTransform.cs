using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Event.Types;
using Microsoft.Xna.Framework;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class FTransform : FComponent {
    private Vector2 position;
    private Vector2 scale;
    private Vector2 origin;
    private float rotation;
    
    public Vector2 Position =>  position;
    public Vector2 Scale => scale;
    public Vector2 Origin => origin;
    public float Rotation => rotation;

    // Not allowed to set the origin (for now, should this change later?).
    public FTransform(Vector2 position = default, Vector2 scale = default, float rotation = 0) {
        this.position = position;
        this.scale = scale == Vector2.Zero ? Vector2.One : scale;
        this.rotation = rotation;
    }

    public void Translate(Vector2 translation) {
        position += translation;
        Owner.Events.Invoke(new MoveEvent(Owner, position));
    }
    
    public void SetPosition(Vector2 newPosition) {
        position = newPosition;
        Owner.Events.Invoke(new MoveEvent(Owner, position));
    }
    
    public void AddRotation(float newRotation) => rotation += newRotation;
    public void SetOrigin(Vector2 newOrigin) => origin = newOrigin;
}