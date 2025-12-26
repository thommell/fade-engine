
using System.Collections.Specialized;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Event.Types;
using Microsoft.Xna.Framework;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class Transform : Component {
    private Vector2 _position;
    private Vector2 _scale = Vector2.One;
    private Vector2 _origin;
    private float _rotation;
    
    public Vector2 Position =>  _position;
    public Vector2 Scale => _scale;
    public Vector2 Origin => _origin;
    public float Rotation => _rotation;

    // Not allowed to set the origin (for now, should this change later?).
    public Transform(Vector2 position = default, Vector2 scale = default, float rotation = 0) {
        _position = position;
        _scale = scale == Vector2.Zero ? Vector2.One : scale;
        _rotation = rotation;
    }

    public void Translate(Vector2 translation) {
        _position += translation;
        Owner.Events.Invoke(new MoveEvent(Owner, _position));
    }

    public void SetPosition(Vector2 newPosition) => _position = newPosition;
    public void SetOrigin(Vector2 newOrigin) => _origin = newOrigin;
}