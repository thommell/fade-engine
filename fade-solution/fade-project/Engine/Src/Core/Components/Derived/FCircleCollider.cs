using System.Numerics;
using fade_project.Core.Components.BaseAbstract.BaseAbstract;
using fade_project.Core.Event.Types;

namespace fade_project.Core.Components.BaseAbstract;

public sealed class FCircleCollider : FCollider {
    private int _radius;
    private Vector2 _center;
    
    public int Radius => _radius;
    public Vector2 Center => _center;

    public FCircleCollider(int radius = 3) {
        _radius = radius;
    }
    
    protected override void UpdateCollider(MoveEvent e) {
    }

    public override bool Intersects(FCollider other) =>
        other.IntersectsCircle(this);

    public override bool IntersectsCircle(FCircleCollider circle) {
        return false;
    }
        
    public override bool IntersectsBox(FBoxCollider box) {
        return true;
    }
}